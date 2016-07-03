using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.Types
{
    public sealed class Integer : DataType
    {
        public Integer()
        {
            Name = "Integer";
            Type = DataTypeSpecifer.Linear;
            Operator add = new Operator("plus", "+", Operator.OperatorType.Poly, this);
            Operator subtract = new Operator("minus", "-", Operator.OperatorType.Binary, this);
            Operator multiply = new Operator("multipliedby", "*", Operator.OperatorType.Poly, this);
            Operator divide = new Operator("dividedby", "/", Operator.OperatorType.Binary, this);
            Operator exponent = new Operator("raisedto", "^", Operator.OperatorType.Binary, this);
            Operator modulus = new Operator("moduluswith", "%", Operator.OperatorType.Binary, this);
            
            add.EvaluateOperator += add_EvaluateOperator;
            subtract.EvaluateOperator += subtract_EvaluateOperator;
            multiply.EvaluateOperator += multiply_EvaluateOperator;
            divide.EvaluateOperator += divide_EvaluateOperator;
            exponent.EvaluateOperator += exponent_EvaluateOperator;
            modulus.EvaluateOperator += modulus_EvaluateOperator;

            AddOperator(add);
            AddOperator(subtract);
            AddOperator(multiply);
            AddOperator(divide);
            AddOperator(exponent);
            AddOperator(modulus);

            TypeCast toreal = new TypeCast("Integer", "Real");

            toreal.CastVariable += toreal_CastVariable;
            toreal.ReverseCastvariable += toreal_ReverseCastvariable;
            AddCast(toreal);
        }

        Variable toreal_ReverseCastvariable(Variable variable)
        {
            Variable casted = null;
            try
            {
                casted = Processor.DefaultProcessor.GetVariable("Integer", (int)variable.Value);
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException("Cannot cast from Double to Integer");
            }
            return casted;
        }
        Variable toreal_CastVariable(Variable variable)
        {
            Variable casted = null;
            try
            {
                casted = Processor.DefaultProcessor.GetVariable("Double", (double)variable.Value);
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException("Cannot cast from Integer to Double");
            }
            return casted;
        }
        Variable modulus_EvaluateOperator(Operator op, params Variable[] variables)
        {
            return new Variable(this, ((int)variables[0].Value % (int)variables[1].Value));
        }
        Variable exponent_EvaluateOperator(Operator op, params Variable[] variables)
        {
            return new Variable(this, ((int)(System.Math.Pow((int)variables[0].Value, (int)variables[1].Value))));
        }
        Variable divide_EvaluateOperator(Operator op, params Variable[] variables)
        {
            return new Variable(this, ((int)variables[0].Value / (int)variables[1].Value));
        }
        Variable multiply_EvaluateOperator(Operator op, params Variable[] variables)
        {
            int va = 1;
            foreach (Variable v in variables)
            {
                va *= (int)v.Value;
            }
            return new Variable(this, va);
        }
        Variable subtract_EvaluateOperator(Operator op, params Variable[] variables)
        {
            return new Variable(this, ((int)variables[0].Value - (int)variables[1].Value));
        }
        Variable add_EvaluateOperator(Operator op, params Variable[] variables)
        {
            int va = 0;
            foreach (Variable v in variables)
            {
                va += (int)v.Value;
            }
            return new Variable(this, va);
        }

        public override Variable GetVariable(object s)
        {
            return Processor.DefaultProcessor.GetVariable("Integer", (int)s);
        }
        public override Variable Parse(string s)
        {
            return new Variable(this, int.Parse(s));
        }
    }
}
