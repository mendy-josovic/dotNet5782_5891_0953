using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace ConsoleUI
{
    [Serializable]
    public class ConsoleException: Exception
    {
        public string[] violations { get; private set; }
        public int type { get; set; }
        public ConsoleException() : base() { }
        public ConsoleException(string message) : base(message) { }
        public ConsoleException(string message, Exception e) : base(message, e) { }
        protected ConsoleException(SerializationInfo info, StreamingContext context) : base(info, context) { }


        public override string ToString()
        {
           
            return "Consol_uI"+base.ToString();
        }

    }
}
