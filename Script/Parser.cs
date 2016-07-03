using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Script.Runtime;
using Language;

namespace Script
{
    public class Parser
    {
        private Engine _engine;

        public Parser(Engine engine)
        {
            _engine = engine;
        }

        public void ParseVariableDeclaration(string str)
        {
            if (str.EndsWith(";") && str.IndexOf(';') == str.Length - 1)
            {
                Console.WriteLine("Is Valid Line");
                StringTokenizer st = new StringTokenizer(str);
                str = str.Remove(str.Length - 1, 1);
                string type = st.NextToken();
                Console.WriteLine(str + "\n" + type);
                if (type.StartsWith("/"))
                {
                    Console.WriteLine(type + " : " +
                        Processor.DefaultProcessor.IsValidType(type.Remove(0, 1)));
                    type = type.Remove(0, 1);
                    string vardec = " ";
                    while ((vardec = st.NextToken(',', ';')) != null)
                    {
                        Console.WriteLine("- > " + vardec);
                        object val = null;
                        string varname = "";
                        if(vardec.Contains("["))
                        {
                            varname = vardec.Substring(0, vardec.IndexOf("[")).Trim();
                            string len = vardec.Substring(0, vardec.IndexOf(']'));
                            len = len.Substring(vardec.IndexOf('[') + 1);
                            int l = 0;
                            if (len.Length == 0)
                            {
                                Console.WriteLine("No size specified!");
                            }
                            else
                            {
                                Console.WriteLine("Size for array : " + len);
                                l = int.Parse(len);
                            }
                            VariableArray arr = new VariableArray(Processor.GType(type), l) { Name = varname };
                            _engine.Arrays.Add(varname, arr);
                        }
                        else if (vardec.Contains("="))
                        {
                            varname = vardec.Substring(0, vardec.IndexOf("=")).Trim();
                            val = Processor.GType(type).Parse(vardec.Substring(vardec.IndexOf("=") + 1).Trim());
                            Variable var = new Variable(Processor.GType(type), val);
                            _engine.Variables.Add(varname, var);
                        }
                        else
                        {
                            varname = vardec.Trim();
                            Variable var = new Variable(Processor.GType(type), val);
                            _engine.Variables.Add(varname, var);
                        }
                    }
                }
            }
            return;
        }

        public void Execute(string s)
        {
            if (s.Contains("="))
            {
                string n = s.Substring(0, s.IndexOf("="));
                string exp = s.Substring(s.IndexOf("=") + 1);

            }
        }

        public void Evaluate(string str)
        {
            Console.WriteLine(str);
            StringTokenizer st = new StringTokenizer(str + ";");
            string s = " ";
            List<string> list = Processor.DefaultProcessor.GetTypeLister().OperatorsSupported.Keys.ToList();
            list.Add(";");
            list.Add(")");
            list.Add("(");
            Stack<string> vars = new Stack<string>();
            Stack<string> ops = new Stack<string>();
            vars = new Stack<string>(vars.Reverse());
            ops = new Stack<string>(ops.Reverse());
            while ((s = st.NextToken(list.ToArray())) != null)
            {
                if (s.Trim().Length != 0)
                    vars.Push(s);
                ops.Push(st.PreviousProcessedString);
            }
            while (ops.Count != 0)
            {
                string op = ops.Pop();
                if (op == "(")
                {
                    string subop = " ";
                    while ((subop = ops.Pop()) != ")")
                    {
                        Console.WriteLine(subop);
                    }
                }
                else if (op == ")")
                {

                }
            }
            return;
        }
    }
}
