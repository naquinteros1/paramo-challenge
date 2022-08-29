using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.PersistenseFile.Helpers
{
    public static class FileHelper
    {
        public static string[] ReadFromFile(string path)
        {
            string[] lines = new string[] { };
            try
            {
                var file = Directory.GetCurrentDirectory() + path;
                lines = File.ReadAllLines(file);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return lines;
        }

        public static string ReadAllFromFile(string path)
        {
            return File.ReadAllText(path);
            
        }

        public static async Task<bool> WriteFile(string path, string text)
        {
            bool ok = false;
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    await writer.WriteLineAsync(text);
                }
                ok = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return ok;
        }
    }
}
