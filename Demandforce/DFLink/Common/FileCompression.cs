// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileCompression.cs" company="Demandforce">
//   Copyright (c) Demandforce. All rights reserved.
// </copyright>
// <summary>
//   The file compression.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demandforce.DFLink.Common
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using ICSharpCode.SharpZipLib.Zip;

    /// <summary>
    /// The file compression.
    /// </summary>
    internal class FileCompression
    {
        #region Public Methods and Operators

        /// <summary>
        /// The compress.
        /// </summary>
        /// <param name="fileInfo">
        /// The file info.
        /// </param>
        /// <param name="zipFileName">
        /// The zip file name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="compressionLevel">
        /// The compression level. if null 6 is default
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Compress(
            FileInfo fileInfo,
            string zipFileName,
            string password,
            int? compressionLevel)
        {
            var s = new ZipOutputStream(File.Create(zipFileName));
            try
            {
                s.SetLevel(compressionLevel.HasValue ? compressionLevel.Value : 6); //// 0 - store only to 9 - means best compression
                s.Password = password;

                FileStream fs;
                try
                {
                    fs = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite);
                }
                catch
                {
                    return false;
                }

                var data = new byte[2048];
                int size = 2048;
                var entry = new ZipEntry(Path.GetFileName(fileInfo.Name));
                entry.DateTime = fileInfo.CreationTime > fileInfo.LastWriteTime ? fileInfo.LastWriteTime : fileInfo.CreationTime;
                s.PutNextEntry(entry);
                while (true)
                {
                    size = fs.Read(data, 0, size);
                    if (size <= 0)
                    {
                        break;
                    }

                    s.Write(data, 0, size);
                }

                fs.Close();
                return true;
            }
            finally
            {
                s.Finish();
                s.Close();
            }
        }

        /// <summary>
        /// The compress.
        /// </summary>
        /// <param name="fileNames">
        /// The file names.
        /// </param>
        /// <param name="zipFileName">
        /// The zip file name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="compressionLevel">
        /// The compression level.
        /// </param>
        /// <param name="sleepTimer">
        /// The sleep timer.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Compress(
            IEnumerable<FileInfo> fileNames, 
            string zipFileName,
            string password,
            int? compressionLevel, 
            int? sleepTimer)
        {
            var s = new ZipOutputStream(File.Create(zipFileName));
            try
            {
                s.SetLevel(compressionLevel.HasValue ? compressionLevel.Value : 6); //// 0 - store only to 9 - means best compression
                s.Password = password;

                foreach (FileInfo file in fileNames)
                {
                    FileStream fs;
                    try
                    {
                        fs = file.Open(FileMode.Open, FileAccess.ReadWrite);
                    }
                    catch
                    {
                        continue;
                    }

                    // 方法二，将文件分批读入缓冲区
                    var data = new byte[2048];
                    int size = 2048;
                    var entry = new ZipEntry(Path.GetFileName(file.Name));
                    entry.DateTime = file.CreationTime > file.LastWriteTime ? file.LastWriteTime : file.CreationTime;
                    s.PutNextEntry(entry);
                    while (true)
                    {
                        size = fs.Read(data, 0, size);
                        if (size <= 0)
                        {
                            break;
                        }

                        s.Write(data, 0, size);
                    }

                    fs.Close();

                    Thread.Sleep(sleepTimer.HasValue ? sleepTimer.Value : 50);
                }

                return true;
            }
            finally
            {
                s.Finish();
                s.Close();
            }
        }

        #endregion
    }
}