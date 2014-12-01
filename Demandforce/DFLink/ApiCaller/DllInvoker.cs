// -----------------------------------------------------------------------
// <copyright file="DllInvoker.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.ApiCaller
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Calls API that extracts out data from PMS(Created by Delphi)
    /// </summary>
    public class DllInvoker
    {
        /// <summary>
        /// A pointer variable related to dynamic link library
        /// </summary>
        private IntPtr dll;         
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DllInvoker"/> class.
        /// </summary>
        /// <param name="dllPath">
        ///     the path of dynamic link library
        /// </param>
        public DllInvoker(string dllPath)
        {
            this.dll = LoadLibrary(dllPath);            
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DllInvoker" /> class.
        /// </summary>
        ~DllInvoker()
        {
            FreeLibrary(this.dll);            
        }

        /// <summary>
        /// change a interface function to a delegation
        /// </summary>
        /// <param name="funcName">interface function of dynamic link library</param>
        /// <param name="t">type of the interface function</param>    
        /// <returns>
        /// A delegation for interface function
        /// </returns>
        public Delegate Invoke(string funcName, Type t)
        {
            IntPtr api = GetProcAddress(this.dll, funcName);
            return (Delegate)Marshal.GetDelegateForFunctionPointer(api, t);              
        }

        #region Win API
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadLibrary(string path);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern IntPtr GetProcAddress(IntPtr lib, string funcName);

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern bool FreeLibrary(IntPtr lib);
        #endregion
    }
}