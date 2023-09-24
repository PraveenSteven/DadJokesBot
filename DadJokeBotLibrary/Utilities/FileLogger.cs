using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJokeBotLibrary.Utilities
{
    internal static class FileLogger
    {
        // Random hardcoded path.
        // Todo: read from config. 
        private static string filePath = @"C:\\DadJokesLogger.log";
        internal static void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
    }
}
