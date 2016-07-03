using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    public class Operator
    {
        private string _operator;
        private string _name;
        private DataType[] _operands;

        public string OperatorString { get { return _operator; } }
        public string Name { get { return _name; } }
        public OperatorType Type { get; set; }
        public DataType[] Operands
        {
            get
            {
                return _operands;
            }
            set
            {
                value.CopyTo(_operands, 0);
            }
        }
        public int ParamsRequired { get; set; }

        public delegate Variable Evaluator(Operator op, params Variable[] variables);
        public event Evaluator EvaluateOperator;

        public enum OperatorType
        {
            Unary,
            Binary,
            Ternary,
            Poly
        }

        public Operator(string opstring, params DataType[] operands)
        {
            if (operands.Length == 1)
                Type = OperatorType.Unary;
            else if (operands.Length == 2)
                Type = OperatorType.Binary;
            else if (operands.Length == 3)
                Type = OperatorType.Ternary;
            else
                Type = OperatorType.Poly;
            ParamsRequired = operands.Length;
            _operator = opstring;
            _operands = new DataType[operands.Length];
            operands.CopyTo(_operands, 0);
        }
        public Operator(string opstring, OperatorType type, DataType operand, int param = 0)
        {
            Type = type;
            if (type == OperatorType.Unary) param = 1;
            else if (type == OperatorType.Binary) param = 2;
            else if (type == OperatorType.Ternary) param = 3;
            _operator = opstring;
            _operands = new DataType[param];
            for (int i = 0; i < param; i++)
            {
                _operands[i] = operand;
            }
            ParamsRequired = param;
        }
        public Operator(string name, string opstring, params DataType[] operands)
        {
            _name = name;
            if (operands.Length == 1)
                Type = OperatorType.Unary;
            else if (operands.Length == 2)
                Type = OperatorType.Binary;
            else if (operands.Length == 3)
                Type = OperatorType.Ternary;
            else
                Type = OperatorType.Poly;
            ParamsRequired = operands.Length;
            _operator = opstring;
            _operands = new DataType[operands.Length];
            operands.CopyTo(_operands, 0);
        }
        public Operator(string name, string opstring, OperatorType type, DataType operand, int param = 0)
        {
            _name = name;
            Type = type;
            if (type == OperatorType.Unary) param = 1;
            else if (type == OperatorType.Binary) param = 2;
            else if (type == OperatorType.Ternary) param = 3;
            _operator = opstring;
            _operands = new DataType[param];
            for (int i = 0; i < param; i++)
            {
                _operands[i] = operand;
            }
            ParamsRequired = param;
        }
        public Variable Evaluate(params Variable[] placeHolders)
        {
            if (!(Type == OperatorType.Poly))
            {
                if (Operands.Length == placeHolders.Length)
                {
                    for (int i = 0; i < Operands.Length; i++)
                    {
                        if (!Operands[i].Name.Equals(placeHolders[i].Type.Name))
                        {
                            throw new ArgumentException("Either the argument types do not match for the defined operator or the number of parameters is undesired.");
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Either the argument types do not match for the defined operator or the number of parameters is undesired.");
                }
            }
            return EvaluateOperator(this, placeHolders);
        }
    }
}
