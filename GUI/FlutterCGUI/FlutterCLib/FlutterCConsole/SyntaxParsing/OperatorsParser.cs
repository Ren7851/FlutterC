using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;


namespace FlutterCConsole
{
    public class OperatorsParser
    {
        public static bool EVIL = true;
        public static void parseFunction(FunctionBody body, List<string> tokens)
        {
            if (!Utils.checkBalance("(", ")", tokens))
            {
                throw new BracketBalanceException("(", 0, tokens);
            }

            if (!Utils.checkBalance("[", "]", tokens))
            {
                throw new BracketBalanceException("[", 0, tokens);
            }

            Node res = parseBlock(tokens, body, body.name);
            body.addNode(res);
            body.addNode(new ReturnOff());
        }



        public static LinearNode parseBlock(List<string> tokens, Node host, string funcname)
        {
            LinearNode res = new LinearNode(host);
            int prev = -1;
            tokens.Add(Utils.DELIMITER);
            int i = 0;
            while (i < tokens.Count)
            {
                bool sign = tokens[i] == ";";
                if (EVIL)
                {
                    sign = sign || tokens[i] == "while" || tokens[i] == "for" || tokens[i] == "if" || tokens[i] == Utils.DELIMITER;
                }
                if (sign)
                {
                    List<string> command = new List<string>();
                    for (int j = prev + 1; j < i; j++)
                    {
                        command.Add(tokens[j]);
                    }

                    if (command.Count > 0)
                    {
                        List<Node> instructions = parseCommand(command, res, funcname);
                        for (int k = 0; k < instructions.Count; k++)
                        {
                            res.addNode(instructions[k]);
                        }
                    }

                    prev = i;
                }


                if (tokens[i] == "while" || tokens[i] == "for")
                {
                    string takitipcycla = tokens[i];
                    List<string> condition = new List<string>();
                    List<string> body = new List<string>();
                    i++;
                    if (tokens[i] != "(")
                    {
                        throw new BlockException(i, tokens, funcname);
                    }
                    else
                    {
                        condition = detectSomethingBalance(")", i, tokens, "(", funcname);
                    }
                    i += condition.Count + 1;
                    if (tokens[i] != ")")
                    {
                        throw new BlockException(i, tokens, funcname);
                    }

                    i++;
                    if (tokens[i] == "{")
                    {
                        body = detectBlock(i, tokens, funcname);
                        i += body.Count;
                        i++;
                        prev = i;
                    }
                    else
                    {
                        i--;
                        body = detectSomething(";", i, tokens, funcname);
                        i += body.Count;
                        prev = i;
                    }

                    if (takitipcycla == "while")
                    {
                        res.addNode(parseWhile(condition, body, res, funcname));
                        res.addNode(new BreakOff(SyntaxParser.peek()));
                        SyntaxParser.numbers.Pop();
                    }
                    else
                    {
                        res.addNode(parseFor(condition, body, res, funcname));
                        res.addNode(new BreakOff(SyntaxParser.peek()));
                        SyntaxParser.numbers.Pop();
                    }
                }




                if (tokens[i] == "if")
                {
                    List<string> condition = new List<string>();
                    List<string> bodyIf = new List<string>();
                    List<string> bodyElse = new List<string>();
                    i++;
                    if (tokens[i] != "(")
                    {
                        throw new BlockException(i, tokens, funcname);
                    }
                    else
                    {
                        condition = detectSomethingBalance(")", i, tokens, "(", funcname);
                    }
                    i += condition.Count + 1;
                    if (tokens[i] != ")")
                    {
                        throw new BlockException(i, tokens, funcname);
                    }

                    i++;
                    if (tokens[i] == "{")
                    {
                        bodyIf = detectBlock(i, tokens, funcname);
                        i += bodyIf.Count;
                        i++;
                        prev = i;
                    }
                    else
                    {
                        i--;
                        bodyIf = detectSomething(";", i, tokens, funcname);
                        i += bodyIf.Count;
                        //i++;
                        prev = i;
                    }

                    i++;
                    if (tokens[i] == "else")
                    {
                        i++;
                        if (tokens[i] == "{")
                        {
                            bodyElse = detectBlock(i, tokens, funcname);
                            i += bodyElse.Count;
                            i++;
                            prev = i;
                        }
                        else
                        {
                            i--;
                            bodyElse = detectSomething(";", i, tokens, funcname);
                            i += bodyElse.Count;
                            //i++;
                            prev = i;
                        }
                    }
                    else
                    {
                        i--;
                    }


                    res.addNode(parseIfElse(condition, bodyIf, bodyElse, res, funcname));
                }


                i++;
            }
            tokens.RemoveAt(tokens.Count - 1);
            return res;
        }


        public static List<string> detectSomethingBalance(string something, int i, List<string> tokens, string other, string funcname)
        {
            int balance = -1;
            int pointer = i + 1;
            List<string> res = new List<string>();
            while (true)
            {
                if (tokens[pointer] == Utils.DELIMITER)
                {
                    throw new BlockException(i, tokens, funcname);
                }

                if (tokens[pointer] == something)
                {
                    balance++;
                    if (balance == 0)
                    {
                        return res;
                    }
                }

                if (tokens[pointer] == other)
                {
                    balance--;
                }


                res.Add(tokens[pointer]);
                pointer++;
            }
        }

        public static List<string> detectSomething(string something, int i, List<string> tokens, string funcname)
        {
            int pointer = i + 1;
            bool isFor = false;
            int life = 2;
            int bracketBalance = 0;
            List<string> res = new List<string>();
            while (true)
            {
                if (tokens[pointer] == Utils.DELIMITER)
                {
                    throw new BlockException(i, tokens, funcname);
                    //return res;
                }

                if (tokens[pointer] == "{")
                {
                    bracketBalance++;
                }

                if (tokens[pointer] == "}")
                {
                    bracketBalance--;
                }

                if (tokens[pointer] == "for" && something == ";")
                {
                    isFor = true;
                    life = 3;
                }

                if (tokens[pointer] == "}")
                {
                    //Console.WriteLine(bracketBalance + " " + isFor);
                }

                if (tokens[pointer] == something)
                {
                    if (isFor)
                    {
                        life--;
                        if (life == 0)
                        {
                            isFor = false;
                        }
                    }
                }

                if ((tokens[pointer] == something && bracketBalance == 0) || (tokens[pointer] == "}" && bracketBalance == 0))
                {
                    if (!isFor)
                    {
                        res.Add(tokens[pointer]);
                        if (pointer != tokens.Count - 1 && tokens[pointer + 1] == "else" && tokens[pointer] == "}")
                        {
                            res.RemoveAt(res.Count  - 1);
                        }
                        else
                        {
                            return res;
                        }
                    }
                }

                res.Add(tokens[pointer]);

                pointer++;
            }
        }

        public static List<string> detectBlock(int i, List<string> tokens, string funcname)
        {
            int count = 1;
            List<string> body = new List<string>();
            int pointer = i + 1;
            while (true)
            {
                if (tokens[pointer] == Utils.DELIMITER)
                {
                    throw new BlockException(i, tokens, funcname);
                }

                if (tokens[pointer] == "{")
                {
                    count++;
                }

                if (tokens[pointer] == "}")
                {
                    count--;
                    if (count == 0)
                    {
                        break;
                    }
                }

                body.Add(tokens[pointer]);

                pointer++;
            }
            return body;
        }

        public static List<Node> parseCommand(List<string> tokens, Node host, string funcname)
        {
            //Node res = new LinearNode();
            //res.tag = "operation";

            if (tokens.Count == 0)
            {
                return new List<Node>();
            }

            List<Node> res = CommandParser.parseCommand(host, tokens, tokens, funcname);

            /*
            for (int i = 0; i < tokens.Count; i++ )
            {
                Console.Write(tokens[i]+" ");
            }
            Console.WriteLine();
             * */


            return res;
        }


        public static Node parseWhile(List<string> condition, List<string> body, LinearNode host, string funcname)
        {
            int num = NameInventor.numb();
            SyntaxParser.numbers.Push(num);
            While res = new While(host);
            Node cond = new LinearNode(res);
            LinearNode whilebody = new LinearNode(res);
            whilebody = (LinearNode)parseBlock(body, res, funcname);
            whilebody.addNode(new ContinueOff(num));
            cond = parseBlock(condition, res, funcname);
            whilebody.tag = "while body";
            res.setStuff(cond, whilebody);
            return res;
        }


        public static Node parseFor(List<string> condition, List<string> body, LinearNode host, string funcname)
        {
            int num = NameInventor.numb();
            SyntaxParser.numbers.Push(num);
            List<List<string>> splitCondition = Utils.split(condition, ";");
            if (splitCondition.Count != 3)
            {
                throw new ForParseException(0, condition, funcname);
            }



            LinearNode forMain = new LinearNode(host);


            List<Node> nodes = parseCommand(splitCondition[0], forMain, funcname);
            for (int i = 0; i < nodes.Count; i++)
            {
                forMain.addNode(nodes[i]);
            }

            List<Node> incr = parseCommand(splitCondition[2], forMain, funcname);

            While res = new While(host);
            Node cond = new LinearNode(res);
            LinearNode whilebody = new LinearNode(forMain);
            whilebody = parseBlock(body, forMain, funcname);

            if (splitCondition[1].Count > 0)
            {
                cond = parseBlock(splitCondition[1], forMain, funcname);
            }
            else
            {
                Value v = new Value("char", false);
                v.value = 1;
                cond = new Push(v);
            }

            whilebody.tag = "while body";
            res.setStuff(cond, whilebody);

            whilebody.addNode(new ContinueOff(num));
            for (int i = 0; i < incr.Count; i++)
            {
                whilebody.addNode(incr[i]);
            }

            forMain.addNode(res);
            return forMain;
        }


        public static Node parseIfElse(List<string> condition, List<string> ifBody, List<string> elseBody, Node host, string funcname)
        {
            Condition res = new Condition(host);
            Node cond = new LinearNode(res);
            Node ifN = new LinearNode(res);
            Node elseN = new LinearNode(res);

            cond = parseBlock(condition, res, funcname);
            ifN = parseBlock(ifBody, res, funcname);
            elseN = parseBlock(elseBody, res, funcname);

            ifN.tag = "if body";
            elseN.tag = "else body";
            res.setStuff(ifN, elseN, cond);

            return res;
        }

    }
}