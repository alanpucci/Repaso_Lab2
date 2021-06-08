using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Application.Common
{
    public class Entity
    {
        private int id;

        public int Id { get {  return this.id; } set  { this.id = value; } }
    }
}
