using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    public class VariableArray : DataType
    {
        public DataType Type { get; set; }
        private Variable[] _array;
        public Variable this[int idx]
        {
            get
            {
                return _array[idx];
            }
            set
            {
                _array[idx] = value;
            }
        }
        public int Length { get { return _array.Length; } }

        public VariableArray(DataType t, params Variable[] values)
        {
            Type = t;
            Name = "Array<" + Type.Name + ">";
            _array = new Variable[values.Length];
            values.CopyTo(_array, 0);
            AddOperators();
        }
        public VariableArray(DataType t, int size)
        {
            Type = t;
            Name = "Array<" + Type.Name + ">";
            _array = new Variable[size];
            AddOperators();
        }
        public VariableArray()
        {
            Type = Processor.DefaultProcessor.GetType("Object");
            Name = "Array<" + Type.Name + ">";
        }

        private void AddOperators()
        {
            Operator length = new Operator("length", "#");
            length.EvaluateOperator += length_EvaluateOperator;
            AddOperator(length);
        }

        Variable length_EvaluateOperator(Operator op, params Variable[] variables)
        {
            return Processor.GV("Integer", _array.Length);
        }

        public Variable[] ToArray()
        {
            Variable[] arr = new Variable[_array.Length];
            _array.CopyTo(arr, 0);
            return arr;
        }

        public Variable this[string oper, params Variable[] variables]
        {
            get
            {
                if (Operators.Keys.Contains(oper))
                {
                    return Operators[oper].Evaluate(variables);
                }
                if (Type.Operators.Keys.Contains(oper) && variables.Length == 0)
                {
                    Operator op = Type.Operators[oper];
                    if (!(op.Type == Operator.OperatorType.Poly))
                        throw new InvalidOperationException("Invalid Operator Specified - " + oper);
                    return op.Evaluate(_array);
                }
                throw new InvalidOperationException("Invalid Operator Specified - " + oper);
            }
        }
    }
}
