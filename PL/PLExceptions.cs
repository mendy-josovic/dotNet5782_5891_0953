using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PL
{
    [Serializable]
    public class PLExceptions : Exception
    {
        public string[] violations { get; private set; }
        public int type { get; set; }
        public PLExceptions() : base() { }
        public PLExceptions(string message) : base(message) { }
        public PLExceptions(string message, Exception e) : base(message, e) { }
        protected PLExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override string ToString()
        {
            return "Logic_PL_Exception" + base.ToString();
        }
    }
}