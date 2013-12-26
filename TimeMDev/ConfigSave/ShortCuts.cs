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
        public static void Add(int id, Keys key)
        {
            init();
            instance.addShortCut(id, key);
        }
        private void addShortCut(int id, Keys key)
        {
            mapping.Add(id, key);
        }
        public static void Remove(int id)
        {
            init();
            instance.removeShortCut(id);
        }
        private void removeShortCut(int id)
        {
            mapping.Remove(id);
        }
        public static Keys Get(int id)
        {
            init();
            return instance.getShortCut(id);
        }
        private Keys getShortCut(int id)
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
        public static void Change(int id, Keys key)
        {
            init();
            instance.changeKey(id, key);
        }
        private void changeKey(int id, Keys key)
        {
            mapping[id] = key;
        }
        public static bool Exist(Keys key)
        {
            init();
            return instance.KeyExist(key);
        }
        private bool KeyExist(Keys key)
        {
            return mapping.ContainsValue(key);
        }
        private static void init()
        {
            if (instance == null)
            {
                instance = read();
            }
        }
        private static ShortCuts read()
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
        public static void Write()
        {
            init();
            write(instance);
        }
        private static void write(ShortCuts shortcuts)
        {
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "ShortCutsSettings.dat",FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, shortcuts);
            fs.Close();
            instance = shortcuts;//update data in memory
        }

        private static ShortCuts getShortCuts()
        {
            init();
            return instance;
        }
    }
}
