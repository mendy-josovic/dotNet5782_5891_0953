﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace ConsoleUI
{
    [Serializable]
    class BlException : Exception
    {
        public string[] violations { get; private set; }
        public int type { get; set; }
        public BlException() : base() { }
        public BlException(string message) : base(message) { }
        public BlException(string message, Exception e) : base(message, e) { }
        protected BlException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
