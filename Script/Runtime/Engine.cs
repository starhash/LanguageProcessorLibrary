using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Language;
using Language.Processing;
using Language.Types;

namespace Script.Runtime
{
    public class Engine
    {
        private TypeSpace _typespace;
        public Language.TypeSpace TypeSpace
        {
            get
            {
                return _typespace;
            }
        }

        private Dictionary<string, Variable> _variables;
        private Dictionary<string, VariableArray> _arrays;
        private Dictionary<string, VariableTuple> _tuples;
        public Dictionary<string, Variable> Variables { get { return _variables; } }
        public Dictionary<string, VariableArray> Arrays { get { return _arrays; } }
        public Dictionary<string, VariableTuple> Tuples { get { return _tuples; } }

        public Engine()
        {
            _typespace = new Language.TypeSpace();
            _variables = new Dictionary<string, Variable>();
            _arrays = new Dictionary<string, VariableArray>();
            _tuples = new Dictionary<string, VariableTuple>();
        }

        
    }
}
