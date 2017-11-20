using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace FinalNinja
{
    public class XmlManager<T>
    {
        public Type Type;

        public XmlManager()
        {
            Type=typeof(T);
        }

        public T Load(string path)//takes from xml and loads in
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }

        public void Save(string path, object obj)//saves classes back to xml
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
