using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Text
{
    public class Text<T> : IFile<T>
    {
        //TODO implementar IFile
        public bool Save(string file, T data)
        {
            try
            {
                using(StreamWriter fileWriter = new StreamWriter(file, true))
                {
                    fileWriter.WriteLine(data);
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
            data = default(T);
            return true;
        }
    }
}
