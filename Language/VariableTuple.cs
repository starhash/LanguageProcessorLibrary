using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    public class VariableTuple
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        private Dictionary<string, Variable> _values;
        
        public void AddValue(string label, Variable value)
        {
            if (!_values.Keys.Contains(label))
            {
                _values.Add(label, value);
            }
        }
        public void SetDictionary(Dictionary<string, Variable> dic)
        {
            _values = dic;
        }

        public Variable this[int idx]
        {
            get
            {
                return _values.Values.ElementAt(idx);
            }
        }
        public Variable this[string s]
        {
            get
            {
                return _values[s];
            }
            set
            {
                if (_values.Keys.Contains(s))
                    _values.Remove(s);
                _values.Add(s, value);
            }
        }
    }
}
