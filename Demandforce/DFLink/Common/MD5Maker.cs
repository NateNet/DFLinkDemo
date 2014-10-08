//------------------------------------------------------------------------------
// <copyright file="MD5Maker.cs" company="Demandforce">
// Get MD5 value for a file or a string
// </copyright>
//------------------------------------------------------------------------------
namespace Demandforce.DFLink.Common
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// It can generate a md5 value for a file/string
    /// </summary>
    public class MD5Maker
    {
        /// <summary>  
        /// Get md5 string for a String  
        /// </summary>  
        /// <param name="value">Source String</param>  
        /// <returns>md5 string</returns>  
        public static string GetMD5ForString(string value)
        {
            string md5String = string.Empty;
            if (value != string.Empty)
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                md5String = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(value))).Replace("-", string.Empty);                
            }

            return md5String;
        }

        /// <summary>  
        /// Get md5 string for a file  
        /// </summary>  
        /// <param name="fileName">Source File</param>  
        /// <returns>md5 string</returns>  
        public static string GetMD5ForFile(string fileName)
        {
            string md5String = string.Empty;
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            FileStream objFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            byte[] bufferForFileStream = new byte[objFileStream.Length];

            objFileStream.Read(bufferForFileStream, 0, bufferForFileStream.Length);
            objFileStream.Close();
            md5String = BitConverter.ToString(md5.ComputeHash(bufferForFileStream)).Replace("-", string.Empty);

            return md5String;
        }
    }
}
