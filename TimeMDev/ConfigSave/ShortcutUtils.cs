using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace TimeMDev.ShortCut
{
    class ShortcutUtils
    {
        private static String splitter = "  : ";
        private static String[] splitters = new String[1] { splitter};
        
        public static String readableShortcut(Keys key)
        {
            return key.ToString().Replace(",", " +");
        }
        public static String Entry2ListCaption(DictionaryEntry entry)
        {
            return entry.Key.ToString() + splitter + readableShortcut(ShortCuts.Get((int)entry.Value));
        }
        public static String Caption2Hashkey(String text)
        {
            String[] parts = text.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
            /*
             * //maybe check the parts
            foreach (String part in parts)
            {
            }
             */
            return parts[0];
        }
    }
}
