using System;

namespace ARIProject.controllers
{
    static class Tools
    {
        //Return the fileType of a Route String
        internal static String GetFileType(String route)
        {
            var extension = System.IO.Path.GetExtension(route);        
            return extension.TrimStart('.'); 
        }

        //Remove the extension of a Route
        internal static String RemoveFileExtension(String route)
        {
            var splitString = route.Split('.');
            return splitString[0];
        }
    }
}
