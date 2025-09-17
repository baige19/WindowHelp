using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowHelp.WindowHelper;

namespace WindowHelp
{
    public class ListItem
    {
        public ListItem(string title, WindowInfo windowInfos) 
        {
            Key = title;
            WindowInfos = windowInfos;
        }
        public string Key { get; set; } = "";
        public WindowInfo WindowInfos { get; set;} = new WindowInfo();
        public override string ToString()
        {
            return Key;
        }
    }
}
