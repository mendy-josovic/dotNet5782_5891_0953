using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace IDAL.DO
{
        [Serializable]
        public class DalExceptions : Exception
        {
            public  string[] violations { get;private set; }
            public int type { get; set; }
            public DalExceptions() : base() { }
            public DalExceptions(string message) : base(message) { }
            public DalExceptions(string message, Exception e) : base(message, e) { }
            protected DalExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }
            public override string ToString()
            {
                return "ERROR: " +this.violations[type];
            }
        }
}