using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARIProject.controllers
{
    static class Tools
    {
        //Return the fileType of a Route String
        internal static String GetFileType(String route)
        {
            var splitString = route.Split('.');
            return splitString.Length > 0 ? splitString[1] : "Cannot find the file type";
        }

        //Remove the extension of a Route
        internal static String RemoveFileExtension(String route)
        {
            var splitString = route.Split('.');
            return splitString[0];
        }
    }
}
