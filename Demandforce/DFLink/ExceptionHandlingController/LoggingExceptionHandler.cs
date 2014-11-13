// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingExceptionHandler.cs" company="Demandforce">
// Copyright (c) Demandforce. All rights reserved. 
// </copyright>
// <summary>
//   Represents an <see cref="IExceptionHandler" /> that formats
//   the exception into a log message and sends it to
//   the Enterprise Library Logging Application Block.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Demandforce.DFLink.ExceptionHandling.Logging
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;

    using Demandforce.DFLink.Logger;

    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;

    /// <summary>
    /// Represents an <see cref="IExceptionHandler"/> that formats
    /// the exception into a log message and sends it to
    /// the logger.
    /// </summary>
    [ConfigurationElementType(typeof(CustomHandlerData))]
    public class LoggingExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// The type converter.
        /// </summary>
        private static readonly AssemblyQualifiedTypeNameConverter TypeConverter
              = new AssemblyQualifiedTypeNameConverter();

        /// <summary>
        /// The formatter type.
        /// </summary>
        private readonly Type formatterType;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingExceptionHandler"/> class.
        /// </summary>
        /// <param name="configValues">
        /// The config values.
        /// </param>
        public LoggingExceptionHandler(NameValueCollection configValues)
        {
            this.formatterType = 
                (Type)TypeConverter.ConvertFrom(configValues["formatterType"]);
        }

        /// <summary>
        /// <para>
        /// Handles the specified <see cref="Exception"/> object by formatting it and writing to the configured log.
        /// </para>
        /// </summary>
        /// <param name="exception">
        /// <para>
        /// The exception to handle.
        /// </para>
        /// </param>
        /// <param name="handlingInstanceId">
        /// <para>
        /// The unique ID attached to the handling chain for this handling instance.
        /// </para>
        /// </param>
        /// <returns>
        /// <para>
        /// Modified exception to pass to the next handler in the chain.
        /// </para>
        /// </returns>
        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            this.WriteToLog(this.CreateMessage(exception, handlingInstanceId), exception.Data);
            return exception;
        }

        /// <summary>
        /// Writes the specified log message using 
        /// the Logger
        /// method.
        /// </summary>
        /// <param name="logMessage">
        /// The message to write to the log.
        /// </param>
        /// <param name="exceptionData">
        /// The exception's data.
        /// </param>
        protected virtual void WriteToLog(string logMessage, IDictionary exceptionData)
        {
            LogHelper.GetLoggerHandle().Debug(this.ToString(), 0, logMessage);
        }

        /// <summary>
        /// Creates an instance of the <see cref="StringWriter"/>
        /// class using its default constructor.
        /// </summary>
        /// <returns>A newly created <see cref="StringWriter"/></returns>
        protected virtual StringWriter CreateStringWriter()
        {
            return new StringWriter(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Creates an <see cref="ExceptionFormatter"/>
        /// object based on the configured ExceptionFormatter
        /// type name.
        /// </summary>
        /// <param name="writer">
        /// The stream to write to.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to pass into the formatter.
        /// </param>
        /// <param name="handlingInstanceId">
        /// The id of the handling chain.
        /// </param>
        /// <returns>
        /// A newly created <see cref="ExceptionFormatter"/>
        /// </returns>
        protected virtual ExceptionFormatter CreateFormatter(
            StringWriter writer, 
            Exception exception, 
            Guid handlingInstanceId)
        {
            ConstructorInfo constructor = this.GetFormatterConstructor();
            return (ExceptionFormatter)constructor.Invoke(
                new object[] { writer, exception, handlingInstanceId });
        }

        /// <summary>
        /// The get formatter constructor.
        /// </summary>
        /// <returns>
        /// The <see cref="ConstructorInfo"/>.
        /// </returns>
        /// <exception cref="ExceptionHandlingException">
        /// There is a internal exception of formatter type
        /// </exception>
        private ConstructorInfo GetFormatterConstructor()
        {
            var types = new[] { typeof(TextWriter), typeof(Exception), typeof(Guid) };
            ConstructorInfo constructor = this.formatterType.GetConstructor(types);
            if (constructor == null)
            {
                const string ExceptionFormat =
                    "The configured exception formatter '{0}' must expose a "
                    + "public constructor that takes a TextWriter object, an "
                    + "Exception object and a GUID instance as parameters.";
                throw new ExceptionHandlingException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        ExceptionFormat,
                    this.formatterType.AssemblyQualifiedName));
            }

            return constructor;
        }

        /// <summary>
        /// The create message.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="handlingInstanceID">
        /// The handling instance id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateMessage(Exception exception, Guid handlingInstanceID)
        {
            StringWriter writer = null;
            StringBuilder stringBuilder = null;
            try
            {
                writer = this.CreateStringWriter();
                ExceptionFormatter formatter = 
                    this.CreateFormatter(writer, exception, handlingInstanceID);
                formatter.Format();
                stringBuilder = writer.GetStringBuilder();
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }

            return stringBuilder.ToString();
        }
    }
}
