// -----------------------------------------------------------------------
// <copyright file="ApiAssistor.cs" company="Demandforce">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Demandforce.DFLink.ApiCaller
{
    using System;
    using System.Text;

    /// <summary>
    /// debug level 
    /// </summary>
    public enum DebugLevel : byte
    {
        /// <summary> Does not debug </summary>
        Dlunuse = 0,

        /// <summary> Simple debug, no SQL output </summary>        
        Dlinfo = 1,

        /// <summary> normal debug, has SQL </summary>        
        Dldebug = 2,

        /// <summary> detail debug, contain SQL and record detail </summary>
        Dlverbose = 3
    }

    /// <summary>
    /// provider some assist to call API 
    /// </summary>
    public static class ApiAssistor
    {
        /// <summary>
        /// Convert string type to enumeration type for debug level
        /// </summary>
        /// <param name="level">debug level string</param>
        /// <returns>return debug level with enumeration type</returns>
        public static DebugLevel StrToDebugLevel(string level)
        {            
            string strLevel = level;
            if (strLevel.ToLower() != "info" && strLevel.ToLower() != "debug" && strLevel.ToLower() != "verbose")
            {
                strLevel = "unuse";
            }            

            StringBuilder sb = new StringBuilder();
            sb.Append("Dl");
            sb.Append(strLevel.ToLower());
            
            return (DebugLevel)Enum.Parse(typeof(DebugLevel), sb.ToString());
        }

        /// <summary>
        /// Indicates the program working on debug mode or not
        /// </summary>
        /// <param name="level">debug level string</param>
        /// <returns>True-working on debug mode, False-not</returns>
        public static bool IsDebugMode(DebugLevel level)
        {
            return level == DebugLevel.Dlinfo || level == DebugLevel.Dldebug || level == DebugLevel.Dlverbose;
        }        
    }
}