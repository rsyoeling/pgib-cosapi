using Serilog;
using System;
using System.IO;

namespace Common.FileDir
{
    public class FileUtil
    {
        public bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
        {
            try
            {
                using (FileStream fs = File.Create(
                    Path.Combine(
                        dirPath,
                        Path.GetRandomFileName()
                    ),
                    1,
                    FileOptions.DeleteOnClose)
                )
                { }
                return true;
            }
            catch(Exception ex)
            {
                Log.Error("disk cannot be accesed", ex);
                if (throwIfFails)
                    throw;
                else
                    return false;
            }
        }

        public long GetAvailableFreeSpaceInBytes(string path, bool throwIfFails = false)
        {
            try
            { 
                return new DriveInfo(path).AvailableFreeSpace;
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "disk space cannot be accesed");
                if (throwIfFails)
                    throw;
                else
                    return -1;
            }
        }
    }
}
