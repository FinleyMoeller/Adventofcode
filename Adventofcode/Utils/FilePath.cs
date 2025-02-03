using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Adventofcode.Utils
{
    internal class FilePath
    {
        private static readonly string path = "./Source/";
        public static string GetFilePath(string fileName)
        {
            return FilePath.path + fileName;
        }
       
    }
}
