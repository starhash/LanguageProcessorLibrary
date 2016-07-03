using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Language.Types;

namespace Language.Processing
{
    public class DataTypeLister
    {
        private Dictionary<string, DataType> Types;
        public Dictionary<string, string> OperatorsSupported;

        public DataTypeLister() 
        {
            Types = new Dictionary<string, DataType>();
            Integer integer = new Integer();
            Types.Add(integer.Name, integer);
            Real real = new Real();
            Types.Add(real.Name, real);
            Bool boo = new Bool();
            Types.Add(boo.Name, boo);
            //Bit bit = new Bit();
            //Types.Add(bit.Name, bit);
            //Character character = new Character();
            //Types.Add(character.Name, character);
            //CharacterString str = new CharacterString();
            //Types.Add(str.Name, str);
            DataType Object = new DataType() { Name = "Object" };
            Types.Add(Object.Name, Object);
            OperatorsSupported = new Dictionary<string, string>();
            Update();
            Console.WriteLine();
        }

        public void Update()
        {
            foreach (DataType d in Types.Values)
            {
                foreach (Operator o in d.Operators.Values)
                {
                    if (!OperatorsSupported.Keys.Contains(o.OperatorString))
                    {
                        OperatorsSupported.Add(o.OperatorString, d.Name);
                        continue;
                    }
                    string s = OperatorsSupported[o.OperatorString];
                    if (!s.Contains(d.Name))
                        s = s + ", " + d.Name;
                    OperatorsSupported.Remove(o.OperatorString);
                    OperatorsSupported.Add(o.OperatorString, s);
                }
            }
        }

        public DataType this[string s]
        {
            get
            {
                return Types[s];
            }
        }

        public bool HasType(string s)
        {
            return Types.Keys.Contains(s);
        }

        public void AddType(DataType newtype)
        {
            Types.Add(newtype.Name, newtype);
        }

        public void AddOperator(Operator op, DataType d)
        {
            d = Types[d.Name];
            Types.Remove(d.Name);
            d.AddOperator(op);
            Types.Add(d.Name, d);
            Update();
        }
    }
}
