using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFileWeb.Shared.Constants
{
    //public enum FileType
    //{
    //    CSV = 1,
    //    XML = 2
    //}
    public static class FileType {
        public static string[] permittedExtensions = { ".CSV", ".XML"};
    }
}
