using SamSeifert.Utilities.FileParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook
{
    public class ManifestFileNavigation : ManifestFile
    {
        public int _NavigationPointCloses = 0; // 1 = close 1 chapter, 2 = close chapter and higher (part)
        public enum NavigationType { None, Default, Custom }
        public NavigationType _NavigationType = NavigationType.None;
        public String _NavigationNameDefault = null;
        public String _NavigationName = null;


        public ManifestFileNavigation(TagFile node, string base_path) : base(node, base_path)
        {
        }
    }
}
