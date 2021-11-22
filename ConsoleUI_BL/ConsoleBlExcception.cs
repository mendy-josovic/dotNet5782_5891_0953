using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace ConsoleUI_BL
{
    [Serializable]
    public  class ConsoleBlException : Exception
    {
        public string[] violations { get; private set; }
        public int type { get; set; }
        public ConsoleBlException() : base() { }
        public ConsoleBlException(string message) : base(message) { }
        public ConsoleBlException(string message, Exception e) : base(message, e) { }
        protected ConsoleBlException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Console_BL_Excception"+ base.ToString();
        }

    }
}