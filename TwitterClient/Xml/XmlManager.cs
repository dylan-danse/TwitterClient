using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TwitterClient.Xml
{
    public class XmlManager<T>
    {

        public T Deserialize(Stream stream)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(stream);
        }

        public void Serialize(StreamWriter sw, T content)
        {
            if (content != null)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(content.GetType());
                xmlSerializer.Serialize(sw, content);
            }
        }
    }
}
