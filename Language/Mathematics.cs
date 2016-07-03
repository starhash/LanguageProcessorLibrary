using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    public class Mathematics
    {
        private static Dictionary<string, Operator> _operators = new Dictionary<string, Operator>();

        static Mathematics()
        {
            Operator abs = new Operator("AbsoluteValueOf", "Abs", P.GType("Real"));
            abs.EvaluateOperator += abs_EvaluateOperator;
            _operators.Add(abs.OperatorString, abs);
            Operator sqrt = new Operator("SquareRootOf", "Sqrt", P.GType("Real"));
            sqrt.EvaluateOperator += sqrt_EvaluateOperator;
            _operators.Add(abs.OperatorString, abs);
        }

        static Variable sqrt_EvaluateOperator(Operator op, params Variable[] variables)
        {
            double d = (double)variables[0].Value;
            return Variable.GetVariable("Real", Math.Sqrt(d));
        }

        static Variable abs_EvaluateOperator(Operator op, params Variable[] variables)
        {
            double d = (double)variables[0].Value;
            return Variable.GetVariable("Real", Math.Abs(d));
        }

        public static Variable _(string oper, params Variable[] variables)
        {
            List<Variable> list = variables.ToList();
            variables = list.ToArray();
            Operator op = null;
            foreach (Operator o in _operators.Values)
            {
                bool all = true;
                if (o.Operands.Length != variables.Length)
                    continue;
                for (int i = 0; i < o.Operands.Length; i++)
                {
                    if (!variables[i].Type.Name.Equals(o.Operands[i].Name))
                    {
                        all = false;
                        break;
                    }
                }
                if (all && o.OperatorString.Equals(oper))
                {
                    op = o;
                    break;
                }
            }
            if (op == null)
            {
                return null;
            }
            return op.Evaluate(variables);
        }
    }
}
