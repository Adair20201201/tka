using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace TKA.Helper
{
    public class XMLHelper
    {
        public static bool Write(string path, object data, Type type)
        {
            bool result = false;

            FileStream fs = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(type);
                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                xs.Serialize(fs, data);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return result;
        }

        public static object Read(string path, Type type)
        {
            object result = null;

            FileStream fs = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(type);
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                XmlTextReader reader = new XmlTextReader(fs);
                result = xs.Deserialize(reader);
            }
            catch
            {
                result = null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return result;
        }
    }
}
