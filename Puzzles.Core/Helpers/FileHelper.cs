using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Puzzles.Core.Helpers
{
    public static class FileHelper
    {
        public static string GetFileContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public static List<string> GetFileLines(string[] pathElements)
        {
            var filePath = Path.Join(pathElements);

            var fileContents = File.ReadAllLines(filePath);

            return fileContents.ToList();
        }

        /// <summary>
        /// Get the content from an embedded resource (must have the Build Action on Properties set to "Embedded Resource")
        /// - if any issues finding the file, check the known resources using <see cref="GetListOfKnownResources"/>
        /// A resource name is fully qualified i.e. it is the full default namespace for the folder it resides in
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        public static string GetEmbeddedResourceContent(string resourcePath)
        {
            var assembly = Assembly.GetCallingAssembly();
            string result;

            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null) return string.Empty;

                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

        public static IEnumerable<string> GetListOfKnownResources()
        {
            var assembly = Assembly.GetCallingAssembly();
            var knownResources = assembly.GetManifestResourceNames();
            foreach (var knownResource in knownResources)
            {
                Console.WriteLine("Known: {0}", knownResource);
            }
            return knownResources;
        }
    }
}
