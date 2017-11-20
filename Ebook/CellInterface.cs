using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook
{
    public interface CellInterface
    {

        bool CheckboxEnabled
        {
            set;
        }

        bool CheckboxChecked
        {
            set;
        }

        string DisplayNumber
        {
            set;
        }

    }
}
