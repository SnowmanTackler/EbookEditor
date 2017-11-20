using SamSeifert.Utilities.FileParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook
{
    public class ManifestFileNavigation : IComparable<ManifestFileNavigation>
    {
        /// <summary>
        /// non null
        /// </summary>
        public readonly ManifestFile _MF;

        public int _NavigationPointCloses = 0; // 1 = close 1 chapter, 2 = close chapter and higher (part)
        public enum NavigationType { None, Default, Custom }
        public NavigationType _NavigationType = NavigationType.None;
        public String _NavigationNameDefault = null;
        public String _NavigationName = null;

        public ManifestFileNavigation(ManifestFile mf)
        {
            this._MF = mf;
        }

        public int CompareTo(ManifestFileNavigation other)
        {
            return this._MF.CompareTo(other._MF);
        }
    }
}
