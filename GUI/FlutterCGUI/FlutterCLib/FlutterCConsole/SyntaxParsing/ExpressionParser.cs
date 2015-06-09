
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;
using FlutterCConsole.Nodes;

namespace FlutterCConsole
{
    public class OperatorSign
    {
        public static string[] allSigns = { "~", "+", "*", "-", "/", "(", ")", ",", "=", "==", "!=", ">", "<", "<=", ">=", "#", "@", "u+", "u-", "$cast$", "%", "<<", ">>", "&", "|", "^", "&&", "||", "0preinc", "0postinc", "0predec", "0postdec", "+=", "-=", "*=", "/=", ">>=", "<<=", "%=", "|=", "^=", "&=", "!", "$field" };
        public string sign;

        public OperatorSign(string sign)
        {
            this.sign = sign;
        }

        public override string ToString()
        {
            return "operator " + sign;
        }

        public bool isLeftAsociative()
        {
            if (sign == "0predec" || sign == "0preinc" || sign == "+=" || sign == "-=" || sign == "*=" || sign == "/=" || sign == "%=" || sign == "|=" || sign == "&=" || sign == "<<=" || sign == ">>=" || sign == "^=")
            {
                return false;
            }

            if (sign == "~" || sign == "=" || sign == "#" || sign == "@" || sign == "u-" || sign == "u+")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool isLeftBracket()
        {
            return this.sign == "(";
        }

        public bool isRightBracket()
        {
            return this.sign == ")";
        }

        public bool isComma()
        {
            return this.sign == ",";
        }

        public Node node(List<string> instr, string func)
        {
            Node r = null;
            if (this.sign == "==")
            {
                r = new Equal();
            }

            if (this.sign == "!=")
            {
                r = new NonEqual();
            }

            if (this.sign == "<=")
            {
                r = new LessEqual();
            }

            if (this.sign == ">=")
            {
                r = new MoreEqual();
            }

            if (this.sign == ">")
            {
                r = new More();
            }

            if (this.sign == "<")
            {
                r = new Less();
            }

            if (this.sign == "+")
            {
                r = new Plus();
            }

            if (this.sign == "+=")
            {
                PrimitiveValueCommand res = new Plus();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == "-")
            {
                r = new Minus();
            }

            if (this.sign == "-=")
            {
                PrimitiveValueCommand res = new Minus();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == "*")
            {
                r = new Mult();
            }

            if (this.sign == "*=")
            {
                PrimitiveValueCommand res = new Mult();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == "/")
            {
                r = new Divide();
            }

            if (this.sign == "/=")
            {
                PrimitiveValueCommand res = new Divide();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == "=")
            {
                r = new Assignment();
            }

            if (this.sign == "~")
            {
                r = new BitwiseNot();
            }

            if (this.sign == "#")
            {
                r = new Asterix();
            }

            if (this.sign == "@")
            {
                r = new Ampersand();
            }

            if (this.sign == "$cast$")
            {
                r = new CastCommand();
            }

            if(this.sign == "$field"){
                r = new OperatorPoint();
            }

            if (this.sign == "%")
            {
                r = new Mod();
            }

            if (this.sign == "%=")
            {
                PrimitiveValueCommand res = new Mod();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == ">>")
            {
                r = new RightShift();
            }

            if (this.sign == ">>=")
            {
                PrimitiveValueCommand res = new RightShift();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == "<<")
            {
                r = new LeftShift();
            }

            if (this.sign == "<<=")
            {
                PrimitiveValueCommand res = new LeftShift();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == "|")
            {
                r = new BitwiseOr();
            }

            if (this.sign == "u-")
            {
                r = new UnaryMinus();
            }

            if (this.sign == "!")
            {
                r = new LogicalNot();
            }

            if (this.sign == "|=")
            {
                PrimitiveValueCommand res = new BitwiseOr();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == "&")
            {
                r = new BitwiseAnd();
            }

            if (this.sign == "&=")
            {
                PrimitiveValueCommand res = new BitwiseAnd();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == "^")
            {
                r = new BitwiseXor();
            }

            if (this.sign == "^=")
            {
                PrimitiveValueCommand res = new BitwiseXor();
                res.setWithAssignment();
                r = res;
            }

            if (this.sign == "&&")
            {
                r = new LogicalAnd();
            }

            if (this.sign == "||")
            {
                r = new LogicalOr();
            }

            if (this.sign == "0preinc")
            {
                r = new PreIncrement();
            }

            if (this.sign == "0postinc")
            {
                r = new PostIncrement();
            }

            if (this.sign == "0predec")
            {
                r = new PreDecrement();
            }

            if (this.sign == "0postdec")
            {
                r = new PostDecrement();
            }

            if (r != null)
            {
                r.setInstruction(instr);
                r.setFunctionName(func);
            }

            return r;
        }

        public int priority()
        {
            if (sign == "0postinc" || sign == "0postdec" || sign == "$field")
            {
                return 7;
            }

            if (sign == "u-" || sign == "u+" || sign == "#" || sign == "@" || sign == "$cast$" || sign == "0preinc" || sign == "0predec")
            {
                return 6;
            }

            if (sign == "~" || sign == "!" )
            {
                return 5;
            }

            if (sign == "*" || sign == "/" || sign == "%")
            {
                return 4;
            }

            if (sign == ">>" || sign == "<<")
            {
                return 3;
            }

            if (sign == "+" || sign == "-")
            {
                return 2;
            }

            if (sign == "<=" || sign == ">=" || sign == "<" || sign == ">")
            {
                return 1;
            }

            if (sign == "==" || sign == "!=")
            {
                return 1;
            }

            if (sign == "&")
            {
                return 0;
            }

            if (sign == "^")
            {
                return -1;
            }

            if (sign == "|")
            {
                return -2;
            }

            if (sign == "&&")
            {
                return -3;
            }

            if (sign == "||")
            {
                return -4;
            }


            if (sign == "=")
            {
                return -100;
            }

            if (sign == "+=" || sign == "-=" || sign == "*=" || sign == "/=" || sign == "%=" || sign == "|=" || sign == "&=" || sign == "<<=" || sign == ">>=" || sign == "^=")
            {
                return -100;
            }

            return -200;
        }
    }

    public class ExpressionParser
    {

        public static string[] rawTypes = { "int", "float", "double", "long", "char" };

        public static bool isRaw(List<string> list, int x, int y)
        {
            /*
            bool isRawType = false;
            for (int i = 0; i < rawTypes.Length; i++)
            {
                if (list[x+1] == rawTypes[i])
                {
                    isRawType = true;
                    break;
                }
            }

            for (int i = x+2; i < y - 1; i++)
            {
                if (list[i] != "*")
                {
                    isRawType = false;
                }
            }
            return isRawType;
             * */

            List<string> q = new List<string>();
            for (int i = x; i <= y; i++)
            {
                q.Add(list[i]);
            }
            return TypeParser.isValidType(q);
        }

        public static CastInfo makeCastInfo(ref List<string> accumulator)
        {
            if (accumulator.Count > 2 && accumulator[0] == "?" && accumulator[accumulator.Count - 1] == "?")
            {
                bool isRawType = isRaw(accumulator, 1, accumulator.Count - 2);

                if (isRawType)
                {
                    string res = "";
                    for (int i = 1; i < accumulator.Count - 1; i++)
                    {
                        res += accumulator[i];
                    }
                    return new CastInfo(res);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static List<Node> parseExpression(List<string> s, Node host, List<string> inst, string functionName)
        {
            if (inst[inst.Count - 1] == "=")
            {
                inst.RemoveAt(inst.Count - 1);
            }
            List<Node> res = new List<Node>();
            List<Object> tokens = tokenize(s, host, functionName, inst);
            List<Object> RPN = shuntingYard(tokens, s);

            for (int i = 0; i < RPN.Count; i++)
            {
                if (RPN[i] is Variable)
                {
                    res.Add(new FlutterCConsole.PushVariable((Variable)RPN[i]));
                }

                if (RPN[i] is CastInfo)
                {
                    res.Add(new Push((CastInfo)RPN[i]));
                    continue;
                }

                if (RPN[i] is FieldName)
                {
                    res.Add(new Push((FieldName)RPN[i]));
                    continue;
                }

                if (RPN[i] is Value)
                {
                    if (((Value)RPN[i]).isPointer())
                    {
                        res.Add(new Push((Value)RPN[i]));
                    }
                    else
                    {
                        res.Add(new Push((Value)RPN[i]));
                    }
                }

                if (RPN[i] is FunctionBody)
                {
                    res.Add(new Call((FunctionBody)(RPN[i])));
                }

                if (RPN[i] is OperatorSign)
                {
                    res.Add(((OperatorSign)RPN[i]).node(inst, functionName));
                }
            }

            return res;
        }

        public static void handleAccumulator(ref List<string> accumulator, List<Object> res, List<string> source, string funcname, List<string> original)
        {
            if (accumulator.Count > 0)
            {
                string constant = Utils.merge(accumulator);
                Value con = ConstantParser.parseConstant(constant, source, funcname, original);
                res.Add(con);
                accumulator = new List<string>();
            }
        }

        public static List<Object> tokenize(List<string> list, Node host, string funcname, List<string> original)
        {
            List<Object> res = new List<Object>();

            Dictionary<string, Variable> availableVaribles = host.getVariables();
            Dictionary<string, FunctionBody> availableFunctions = Memory.getInstance().getFunctionsMap();

            foreach(var i in availableFunctions){
                string ggg = "";
                if(i.Key.Contains("$")){
                    ggg = i.Key.Substring(0, i.Key.IndexOf("$"));
                }
                else
                {
                    ggg = i.Key;
                }
                if (availableVaribles.ContainsKey(ggg))
                {
                    throw new NameConflictException(i.Key, funcname);
                }
            }

            List<string> accumulator = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                if (accumulator.Count > 0 && accumulator[0] == "?")
                {
                    accumulator.Add(list[i]);
                    CastInfo cast = makeCastInfo(ref accumulator);

                    if (cast != null)
                    {
                        res.Add(cast);
                        res.Add(new OperatorSign("$cast$"));
                        accumulator.Clear();
                    }
                    continue;
                }

                if (availableFunctions.ContainsKey(list[i]))
                {
                    handleAccumulator(ref accumulator, res, list, funcname, original);
                    res.Add(availableFunctions[list[i]]);
                    continue;
                }

                if (availableVaribles.ContainsKey(list[i]))
                {
                    handleAccumulator(ref accumulator, res, list, funcname, original);
                    res.Add(availableVaribles[list[i]]);
                    continue;
                }

                if (OperatorSign.allSigns.Contains<string>(list[i]))
                {
                    handleAccumulator(ref accumulator, res, list, funcname, original);
                    res.Add(new OperatorSign(list[i]));
                    continue;
                }

                accumulator.Add(list[i]);
            }

            CastInfo cst = makeCastInfo(ref accumulator);
            if (cst != null)
            {
                res.Add(cst);
                res.Add(new OperatorSign("$cast$"));
                accumulator.Clear();
            }

            handleAccumulator(ref accumulator, res, list, funcname, original);
            return res;
        }



        public static List<Object> shuntingYard(List<Object> input, List<string> source)
        {
            Stack<Object> stack = new Stack<object>();
            List<Object> res = new List<object>();
            for (int i = 0; i < input.Count; i++)
            {
                Object token = input[i];
                if (token is Value || token is Variable)
                {
                    res.Add(token);
                }

                if (token is FunctionBody)
                {
                    stack.Push(token);
                }

                if (token is OperatorSign)
                {
                    OperatorSign op1 = ((OperatorSign)token);

                    if (op1.isLeftBracket())
                    {
                        stack.Push(op1);
                        continue;
                    }

                    if (op1.isRightBracket())
                    {
                        while (true)
                        {
                            Object peek = null;
                            if (stack.Count > 0)
                            {
                                peek = stack.Peek();
                            }
                            else
                            {
                                throw new BracketBalanceException("(", 0, source);
                            }

                            if (peek is FunctionBody)
                            {
                                res.Add(peek);
                                stack.Pop();
                                continue;
                            }

                            if (peek is OperatorSign && ((OperatorSign)peek).isLeftBracket())
                            {
                                break;
                            }
                            else
                            {
                                res.Add(peek);
                                if (stack.Count > 0)
                                {
                                    stack.Pop();
                                }
                                else
                                {
                                    throw new BracketBalanceException("(", 0, source);
                                }
                            }
                        }
                        stack.Pop();

                        if (stack.Count > 0 && stack.Peek() is FunctionBody)
                        {
                            res.Add(stack.Peek());
                            stack.Pop();
                        }

                        continue;
                    }

                    if (op1.isComma())
                    {
                        while (true)
                        {
                            if (stack.Count > 0)
                            {
                                Object peek = null;
                                peek = stack.Peek();

                                if (peek is OperatorSign && ((OperatorSign)peek).isLeftBracket())
                                {
                                    break;
                                }
                                else
                                {
                                    res.Add(peek);
                                    stack.Pop();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            Object peek = null;
                            if (stack.Count > 0)
                            {
                                peek = stack.Peek();
                            }
                            else
                            {
                                break;
                            }

                            if (peek is OperatorSign)
                            {
                                OperatorSign op2 = ((OperatorSign)peek);
                                if (op1.isLeftAsociative())
                                {
                                    if (op1.priority() <= op2.priority())
                                    {
                                        res.Add(op2);
                                        stack.Pop();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    if (op1.priority() < op2.priority())
                                    {
                                        res.Add(op2);
                                        stack.Pop();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                        stack.Push(op1);
                    }
                }
            }




            while (stack.Count > 0)
            {
                if (stack.Peek() is OperatorSign)
                {
                    OperatorSign peek = (OperatorSign)stack.Peek();
                    if (peek.isLeftBracket() || peek.isRightBracket())
                    {
                        throw new BracketBalanceException("(", 0, source);
                    }
                    else
                    {
                        res.Add(peek);
                        stack.Pop();
                    }
                }
                else
                {
                    res.Add(stack.Peek());
                    stack.Pop();
                }
            }

            return res;
        }
    }



}