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
            public DalExceptions(int type,string message) : base(message)
            {
                this.violations = new string[2] { "elemnt alredy exsit", "access violation" };
                this.type = type;
            }
            /// <summary>
            /// there r 2 basic violations in the level.
            /// 1 trying the access somthing that dos not exsite 
            /// 2 trying to add elemnt that alredy exsits
            /// so the string here has both and we will prin each time acording to the right one
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "ERROR: " +this.violations[type];
            }
        }
}