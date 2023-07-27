
namespace NPOI.Util
{
    using System;
    using System.IO;
    using System.Threading;

    public class TempFile
    {

        private static string dir;
        /**
         * Creates a temporary file.  Files are collected into one directory and by default are
         * deleted on exit from the VM.  Files can be kept by defining the system property
         * <c>poi.keep.tmp.files</c>.
         * 
         * Dont forget to close all files or it might not be possible to delete them.
         */
        public static FileInfo CreateTempFile(String prefix, String suffix)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "poifiles");

            if (dir == null)
                dir = Directory.CreateDirectory(tempDir).FullName;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Generate a unique new filename 
            string file = Path.Combine(dir, prefix + Guid.NewGuid().ToString() + suffix);
            while (File.Exists(file))
            {
                file = Path.Combine(dir, prefix + Guid.NewGuid().ToString() + suffix);
                Thread.Sleep(1);
            }
            FileStream newFile = new FileStream(file, FileMode.CreateNew, FileAccess.ReadWrite);
            newFile.Close();

            return new FileInfo(file);
        }

        public static string GetTempFilePath(String prefix, String suffix)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "poifiles");

            if (dir == null)
                dir = Directory.CreateDirectory(tempDir).FullName;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            Random rnd = new Random(DateTime.Now.Millisecond);
            Thread.Sleep(10);
            //return prefix + rnd.Next() + suffix;
            return Path.Combine(dir, prefix + rnd.Next() + suffix);
        }
    }
}
