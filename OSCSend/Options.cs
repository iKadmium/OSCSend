using CommandLineParser.Arguments;
using CommandLineParser.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCSend
{
    [ArgumentGroupCertification("s,f", EArgumentGroupCondition.ExactlyOneUsed)]
    class Options
    {
        [ValueArgument(typeof(string), 'h', "host", Description = "Set the host", DefaultValue = "localhost")]
        public string Host { get; set; }

        [ValueArgument(typeof(int), 'p', "port", Description = "Set the port", DefaultValue = 8000)]
        public int Port { get; set; }

        [ValueArgument(typeof(int?), 'l', "localport", Description = "Set the local port to listen on for a reply", DefaultValue = null)]
        public int? LocalPort { get; set; }

        [ValueArgument(typeof(string), 'a', "address", Description = "Set the address", Optional = false)]
        public string Address { get; set; }

        [ValueArgument(typeof(string), 's', "stringValue", Description = "Set the string value")]
        public string StringValue { get; set; }

        [ValueArgument(typeof(float), 'f', "floatValue", Description = "Set the float value")]
        public float FloatValue { get; set; }
        
    }
}
