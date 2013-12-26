using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TimeMDev.ShortCut
{
    [Serializable]
    public class ShortCuts
    {

        private static ShortCuts instance;

        public Hashtable mapping{get;set;}

        private ShortCuts()
        {
            mapping = new Hashtable();
        }
        public void addShortCut(int id, Keys key)
        {
            mapping.Add(id, key);
        }
        public void removeShortCut(int id)
        {
            mapping.Remove(id);
        }
        public Keys getShortCut(int id)
        {
            if (mapping.Contains(id))
            {
                return (Keys)mapping[id];
            }
            else
            {
                return (Keys)0;
            }
        }
        public void changeKey(int id, Keys key)
        {
            mapping[id] = key;
        }
        public static ShortCuts read()
        {
            FileStream fs;
            try
            {
                fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "ShortCutsSettings.dat", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                ShortCuts result = bf.Deserialize(fs) as ShortCuts;
                fs.Close();
                return result;
            }
            catch (Exception e)
            {
                return new ShortCuts();
            }
        }
        public static void write(ShortCuts shortcuts)
        {
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "ShortCutsSettings.dat",FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, shortcuts);
            fs.Close();
            instance = shortcuts;//update data in memory
        }

        public static ShortCuts getShortCuts()
        {
            if (instance == null)
            {
                instance = read();
            }
            return instance;
        }
    }
}
