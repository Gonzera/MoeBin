using System.IO;
using System;
using System.Text;

namespace MoeBinAPI.Utils
{
    public static class FileHandler
    {
        public static async Task WriteFile(string fn, byte[] content)
        {
            using(var fs = File.Create("public/" + fn))
            {
                await fs.WriteAsync(content);
            }
        }

        public static byte[]? ReadFile(string fn)
        {
            try
            {
                return File.ReadAllBytes("public/" + fn);
            }catch
            {
                return null;
            }
        }
        public static bool FileExists(string fn)
        {
            return File.Exists("public/" + fn);
        }


    }
}