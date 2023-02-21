//using section
using System;
using System.IO;
using System.Diagnostics;

namespace TECsite
{

    public static class DotEnv
    {
        /// <summary>
        /// Used to load environment variables from a .env file
        /// </summary>
        /// <param name="filePath">The complete file path of the file to load from</param>
        public static void Load(string filePath)
        {
            //ask if the file exists
            Debug.WriteLine(".env exists?");

            //check if it exists, and dont continue if it doesnt
            if (!File.Exists(filePath))
            {
                Debug.WriteLine("error, no .env");
                return;
            }
            
            //if exists ig
            Debug.WriteLine(".env exists");

            //go through the file and set environment variables
            foreach (var line in File.ReadAllLines(filePath))
            {
                Debug.WriteLine(line);
                var parts = line.Split(
                    " = ",
                    StringSplitOptions.RemoveEmptyEntries);
                Debug.WriteLine(parts.Length);
                if (parts.Length != 2)
                    continue;
                Debug.WriteLine("'" + parts[0] + "'" + "'" + parts[1] + "'");
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }

        /// <summary>
        /// Used to write a variable to the .env file
        /// </summary>
        /// <param name="filePath">The complete file path of the file to write to</param>
        /// <param name="variables">the variable to write, in format: NAME = VALUE</param>
        public static void Write(string filePath, string variables)
        {
            string[] filecont = File.ReadAllLines(filePath);
            filecont = filecont.SkipLast(1).ToArray();
            filecont = filecont.Append(variables).ToArray();
            File.WriteAllLines(filePath, filecont);
        }
    }
}