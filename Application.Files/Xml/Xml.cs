using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Application.Files.Xml
{
    public class Xml<T> : IFile<T>
    {
       //TODO implementar IFile
       public bool Save(string file, T data)
       {
            try
            {
                using (XmlTextWriter fileWriter = new XmlTextWriter(file, Encoding.UTF8) )
                {
                    XmlSerializer xmlWriter = new XmlSerializer(typeof(T));
                    xmlWriter.Serialize(fileWriter, data);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
       }

        public bool Read(string file, out T data)
        {
            try
            {
                if (!File.Exists(file))
                {
                    throw new Exception("Ruta inválida.");
                }
                using (XmlTextReader fileReader = new XmlTextReader(file))
                    {
                        XmlSerializer xmlReader = new XmlSerializer(typeof(T));
                        data = (T)xmlReader.Deserialize(fileReader);
                        return true;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
