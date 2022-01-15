using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DalXml
{
   public class XmlException : Exception
    {
        private string FileName { set; get; }
        public string[] violations { get; private set; }
        public int type { get; set; }
        public XmlException() : base() { }
        public XmlException(string message) : base(message) { }
        public XmlException(string message, Exception e) : base(message, e) { }
        public XmlException(string filename,string message,Exception e): base(message,e) { FileName = filename; }
        protected XmlException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return  "Xml_Exception_In_File" + FileName+ this.violations[type];
        }
    }
}