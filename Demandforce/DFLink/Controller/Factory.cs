// -----------------------------------------------------------------------
// <copyright file="Factory.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Encapsulate the process to define and execute Dynamic Method which can 
    /// create instance with constructor without parameter, as a delegate type to 
    /// execute a dynamic method and this is a factory, so use the generic dictionary 
    /// to manager different constructor.
    /// </summary>
    /// <typeparam name="TKey">The key type for dictionary look up
    /// </typeparam>
    /// <typeparam name="TBaseType">The base type for instance in the factory,
    /// it can be Object
    /// </typeparam>
    public static class Factory<TKey, TBaseType>
        where TBaseType : class
    {
        /// <summary>
        /// The Dictionary that caches the delegates
        /// </summary>
        private static Dictionary<TKey, ConstructorInvoker> delegates =
            new Dictionary<TKey, ConstructorInvoker>();

        /// <summary>
        /// The delegate to represent dynamic method that constructs instance
        /// </summary>
        /// <returns>The instance of base type</returns>
        private delegate TBaseType ConstructorInvoker();

        /// <summary>
        /// Adds a new type to the internal dictionary
        /// </summary>
        /// <param name="key">The value of Key type </param>
        /// <param name="type">The type</param>
        public static void Add(TKey key, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            // Check if object is not a class
            if (type.IsClass == false)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0} is not a reference type.",
                    type.FullName), 
                    "type");
            }

            // Check if object is abstract
            if (type.IsAbstract == true)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0} is an abstract class, which can not be created.",
                        type.FullName), 
                        "type");
            }

            // Extra check if delegate not already added.
            if (delegates.ContainsKey(key) == false)
            {
                try
                {
                    // Create the delegate for the type
                    ConstructorInvoker invoke = CreateInvoker(type);

                    // Try to invoke function (extra error check, 
                    // so the delegate is not added on error)
                    invoke();

                    // The invoker executed correctly (no exceptions)
                    // so let's add it to the dictionary
                    delegates.Add(key, invoke);
                }
                catch (InvalidCastException)
                {
                    throw new InvalidCastException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "{0} couldn't be casted to {1}.",
                            type.FullName,
                            typeof(TBaseType).FullName));
                }
            }
        }

        /// <summary>
        ///  Create a new object for the type linked to the given key
        /// </summary>
        /// <param name="key">The value of Key type</param>
        /// <returns>The instance of type</returns>
        public static TBaseType CreateInstance(TKey key)
        {
            ConstructorInvoker invoke = null;
            delegates.TryGetValue(key, out invoke);
            return invoke != null ? invoke() : null;
        }

        /// <summary>
        /// Create a new delegate that returns a new object of Type t
        /// </summary>
        /// <param name="t">The Type</param>
        /// <returns>The delegate to represent dynamic method</returns>
        private static ConstructorInvoker CreateInvoker(Type t)
        {
            // Get the Default constructor.
            ConstructorInfo ctor = t.GetConstructor(new Type[0]);

            // Check if the constructor exists.
            if (ctor == null)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0} doesn't have a public default constructor.",
                        t.FullName));
            }

            // return a constructor method
            var dm = new DynamicMethod(
                t.Name + "Ctor", 
                t, 
                new Type[0], 
                typeof(TBaseType).Module, 
                true);

            // Generate the intermediate language.
            ILGenerator lgen = dm.GetILGenerator();

            // Allocates an uninitialized object and calls the constructor method ctor
            lgen.Emit(OpCodes.Newobj, ctor);

            // Returns from method
            lgen.Emit(OpCodes.Ret);

            // Create a delegate that represents the dynamic method.
            return (ConstructorInvoker)dm.CreateDelegate(
                typeof(ConstructorInvoker));
        }
    }
}
