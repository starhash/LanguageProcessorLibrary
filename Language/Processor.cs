using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Language.Processing;

namespace Language
{
    public class Processor
    {
        private static Processor _defproc = new Processor();
        public static Processor DefaultProcessor
        {
            get { return _defproc; }
            set { _defproc = value; }
        }
        protected DataTypeLister TypeLister;

        public Processor()
        {
            TypeLister = new DataTypeLister();
        }

        public static DataType GType(string s)
        {
            return Processor.DefaultProcessor.GetType(s);
        }
        public static Variable GV(string s, object v)
        {
            return DefaultProcessor.GetVariable(s, v);
        }
        public static VariableArray GVA(string d, params object[] variables)
        {
            return Processor.DefaultProcessor.GetVariableArray(d, variables);
        }
        public static VariableTuple GT(string n, params Variable[] variables)
        {
            VariableTuple t = new VariableTuple() { Name = n };
            for (int i = 0; i < variables.Length; i++)
            {
                t.AddValue(i + "", variables[i]);
            }
            return t;
        }
        public static VariableTuple GT(string n, Dictionary<string, Variable> dic)
        {
            VariableTuple t = new VariableTuple() { Name = n };
            t.SetDictionary(dic);
            return t;
        }

        public DataTypeLister GetTypeLister()
        {
            return TypeLister;
        }
        public DataType GetType(string s)
        {
            return TypeLister[s];
        }
        public Variable GetVariable(DataType d, object o)
        {
            return new Variable(d, o);
        }
        public Variable GetVariable(string d, object o)
        {
            return new Variable(GetType(d), o);
        }
        public VariableArray GetVariableArray(DataType d, params Variable[] variables)
        {
            return new VariableArray(d, variables);
        }
        public VariableArray GetVariableArray(string d, params Variable[] variables)
        {
            return new VariableArray(GetType(d), variables);
        }
        public VariableArray GetVariableArray(string d, params object[] variables)
        {
            DataType dt = GetType(d);
            Variable[] vars = new Variable[variables.Length];
            for (int i = 0; i < vars.Length; i++)
            {
                vars[i] = GetVariable(dt, variables[i]);
            }
            return new VariableArray(dt, vars);
        }

        public bool IsValidType(string d)
        {
            return TypeLister.HasType(d);
        }
    }
}
