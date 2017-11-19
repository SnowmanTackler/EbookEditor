using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ebook
{
    public class FontFaceSerializer
    {
        public static Dictionary<String, String> getDict(String input)
        {
            var dict = new Dictionary<String, String>();
            Boolean error = false;

            foreach (var arg in input.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var pair = arg.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length == 2) dict[pair[0].Trim()] = pair[1].Trim();
                else error = true;
            }

            if (error) Console.WriteLine("Error parsing font dict: {", input, "}");

            return dict;
        }

        public static String getFontPath(String input)
        {
            var dict = FontFaceSerializer.getDict(input);

            String src = null;
            if (dict.TryGetValue("src", out src))
            {
                int end = src.LastIndexOf(")");
                int start = src.IndexOf("url(");

                if ((start == 0) && (end == src.Length - 1))
                    return src.Substring(4, src.Length - 5);
                else return null;
            }
            else return null;
        }
    }
}
