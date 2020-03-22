using System;
using System.IO;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.Services
{
    internal static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string AppFolderPath =
            Path.Combine(AppDataPath, "LW4Polishchuk");

        internal static readonly string StorageFilePath =
            Path.Combine(AppFolderPath, "Users.cskma");

        internal static bool CreateFolderAndCheckFileExistance(string filePath)
        {
            var file = new FileInfo(filePath);
            return file.CreateFolderAndCheckFileExistance();
        }

        internal static bool CreateFolderAndCheckFileExistance(this FileInfo file)
        {
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            return file.Exists;
        }
    }
}