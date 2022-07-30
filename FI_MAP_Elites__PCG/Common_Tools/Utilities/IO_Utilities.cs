using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Common_Tools
{
    public static class IO_Utilities
    {

        public static string Read_File_Text(
            string filePath
            )
        {
            return File.ReadAllText(filePath);
        }

        public static string ReadFileFromFolder(
            string fileName,
            string folderPath
            )
        {
            string filePath = folderPath + "//" + fileName;
            if (File.Exists(filePath) == false)
            {
                throw new System.Exception("file or folder does not exist");
            }
            return File.ReadAllText(filePath);
        }

        public static string[] File_Paths_In_Folder(string folder_path)
        {
            return Directory.GetFiles(folder_path);
        }

        public static string[] File_Paths_In_Folder_Alphanumeric(string folder_path)
        {
            // code borrowed from: https://stackoverflow.com/questions/5093842/alphanumeric-sorting-using-linq
            string[] filePaths = Directory.GetFiles(folder_path);

            string[] sortedFilePaths = filePaths.OrderBy(x => PadNumbers(x)).ToArray();

            return sortedFilePaths.ToArray();
        }

        public static string PadNumbers(string input)
        {
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }

        public static bool Folder_Exists(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        public static bool Is_Folder(string path)
        {
            return Directory.Exists(path);
        }

        public static bool File_Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static bool Is_File(string path)
        {
            return File.Exists(path);
        }

        public static void Create_File(string filePath, bool overwriteIfExists, bool throwErrorIfExists)
        {
            if (File_Exists(filePath) == true)
            {
                if (throwErrorIfExists == true)
                {
                    throw new System.Exception("File already exists");
                }
                else
                {
                    if (overwriteIfExists == true)
                    {
                        var file = File.Create(filePath);
                        file.Close();
                    }
                    else
                    {
                        // do nothing!!!
                    }
                }
            }
            else if (File_Exists(filePath) == false)
            {
                var file = File.Create(filePath);
                file.Close();
            }
        }

        public static void Append_To_File(string filePath, string textToAppend)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.Write(textToAppend);
            }
        }

        public static void CreateFolder(string folderPath)
        {
            // this will not overwrite a previous directory
            Directory.CreateDirectory(folderPath);
        }


    }

}
