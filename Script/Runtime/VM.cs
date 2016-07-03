using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Language.Processing;
using Language;

namespace Script.Runtime
{
    public class VM
    {
        private static Engine _defengine = new Engine();
        private static Dictionary<string, TypeSpace> _typespaces = new Dictionary<string, TypeSpace>();

        public static Engine DefaultEngine { get { return _defengine; } }
        public static Dictionary<string, TypeSpace> TypeSpaces { get { return _typespaces; } }


    }
}
