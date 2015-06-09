using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{
    public class StructureDeclaration
    {
        public string name;
        public List<string> typetokens;
        public List<string> names;

        public StructureDeclaration(string name, List<string> tokens)
        {
            typetokens = new List<string>();
            this.name = name;
            names = new List<string>();
            List<List<string>> spl = Utils.split(tokens, ";");
            for (int i = 0; i < spl.Count; i++)
            {
                if(spl[i].Count == 0){
                    continue;
                }

                string type = "";
                for (int j = 0; j < spl[i].Count - 1; j++ )
                {
                    if(spl[i][j]=="$"){
                        type += "$";
                    }
                    else{
                        type += spl[i][j];
                    }
                }
                string n = spl[i][spl[i].Count - 1];

                typetokens.Add(type);
                names.Add(n);
            }
            Memory.getInstance().addAvailableStruct(this);
        }

        public override string ToString()
        {
            string res = "struct " + name + "\n";
            for (int i = 0; i < typetokens.Count; i++)
            {
               res+="\t"+typetokens[i] + " " + names[i]+"\n";
            }
            return res;
        }
    }

    public class Structure
    {
        public List<FunctionBody> functions;
        public List<List<string>> tokens;
        public List<StructureDeclaration> structs;
        public List<string> outerTokens;

        public Structure()
        {
            functions = new List<FunctionBody>();
            tokens = new List<List<string>>();
            structs = new List<StructureDeclaration>();
            outerTokens = new List<string>();
        }

        public void addOuterToken(string token)
        {
            outerTokens.Add(token);
        }

        public void addFunction(FunctionBody prototype, List<string> tokens)
        {
            functions.Add(prototype);
            this.tokens.Add(tokens);
        }

        public void addStruct(StructureDeclaration prototype, List<string> tokens)
        {
            structs.Add(prototype);
        }

        public void dropToMemory()
        {
            for (int i = 0; i < functions.Count; i++)
            {
                Memory.getInstance().addAvailableFunction(functions[i]);
            }

            for (int i = 0; i < structs.Count; i++)
            {
                Memory.getInstance().addAvailableStruct(structs[i]);
            }
        }
    }

    public class StructureParser
    {
        private static bool isBorderToken(string token)
        {
            return token == ";" || token == "}";
        }

        private static List<string> getArgStrings(List<string> tokens, int prev, int i)
        {
            List<string> argtokens = new List<string>();
            for (int j = prev; j < i; j++)
            {
                argtokens.Add(tokens[j]);
            }
            return argtokens;
        }

        private static FunctionBody determineFunction(int head, List<string> tokens, bool[] used)
        {
            int numOfArgs = 0;
            string name = "";

            FunctionBodyStringNames fbsn = new FunctionBodyStringNames();

            int pointer = head - 1;
            int left = 0;
            while (true)
            {
                if (pointer < 0 || isBorderToken(tokens[pointer]))
                {
                    throw new FunctionRecognisionException(head, tokens);
                }

                if (tokens[pointer] == "(")
                {
                    left = pointer;
                    break;
                }

                pointer--;
            }

            int countArgs = 0;


            if (tokens[head - 2] != "(")
            {
                countArgs = 1;
                int prev = left + 1;

                List<string> argtokens;
                for (int i = left; i < head - 1; i++)
                {
                    if (tokens[i] == ",")
                    {
                        argtokens = getArgStrings(tokens, prev, i);
                        if (argtokens.Count < 2)
                        {
                            throw new FunctionRecognisionException(head, tokens);
                        }
                        else
                        {
                            string argname = argtokens[argtokens.Count - 1];
                            argtokens.RemoveAt(argtokens.Count - 1);
                            if (Utils.checkVarName(argname))
                            {
                                fbsn.addArgument(argname, argtokens);
                            }
                            else
                            {
                                throw new UnappropriateNameException(head, tokens, argname);
                            }
                        }
                        prev = i + 1;


                        countArgs++;
                    }
                }

                argtokens = getArgStrings(tokens, prev, head - 1);
                if (argtokens.Count < 2)
                {
                    throw new FunctionRecognisionException(head, tokens);
                }
                else
                {
                    string argname = argtokens[argtokens.Count - 1];
                    argtokens.RemoveAt(argtokens.Count - 1);
                    if (Utils.checkVarName(argname))
                    {
                        fbsn.addArgument(argname, argtokens);
                    }
                    else
                    {
                        throw new UnappropriateNameException(head, tokens, argname);
                    }
                }
                numOfArgs = countArgs;
            }
            else
            {
                countArgs = 0;
            }


            if (left == 0)
            {
                throw new FunctionRecognisionException(head, tokens);
            }
            else
            {
                name = tokens[left - 1];
            }

            pointer = left - 1;
            while (true)
            {
                if (pointer < 0 || isBorderToken(tokens[pointer]))
                {
                    break;
                }
                else
                {
                    pointer--;
                }
            }

            for (int i = pointer + 1; i <= head; i++)
            {
                used[i] = true;
            }

            List<string> returnType = new List<string>();
            for (int i = pointer + 1; i < left - 1; i++)
            {
                returnType.Add(tokens[i]);
            }
            if (returnType.Count == 0)
            {
                throw new FunctionRecognisionException(head, tokens);
            }
            fbsn.setReturnType(returnType);


            FunctionBody res = new FunctionBody();
            res.init(numOfArgs, name, fbsn);
            Memory.getInstance().addAvailableFunction(res);
            return res;
        }

        private static bool detectFunction(int i, List<string> tokens)
        {
            if (i == 0)
            {
                return false;
            }
            else
            {
                if (tokens[i - 1] == ")")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static bool detectStruct(int i, List<string> tokens)
        {
            if (i < 2)
            {
                return false;
            }
            else
            {
                if (tokens[i - 2] == "$")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static Structure parseStructure(List<string> tokens)
        {
            string FUNCTION_TAG = "function";
            string STRUCT_TAG = "struct";
            string NON_FUNCTION_TAG = "non_function";

            bool[] used = new bool[tokens.Count];

            bool isFunction = false;
            bool isStruct = false;
            Structure res = new Structure();

            List<KeyValuePair<string, int>> stack = new List<KeyValuePair<string, int>>();
            Dictionary<int, int> borders = new Dictionary<int, int>();
            List<string> tags = new List<string>();

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == "{")
                {
                    if (!isFunction && !isStruct)
                    {
                        if (detectFunction(i, tokens))
                        {
                            isFunction = true;
                            stack.Add(new KeyValuePair<string, int>(FUNCTION_TAG, i));
                        }
                        else
                        {
                            if (detectStruct(i, tokens))
                            {
                                isStruct = true;
                                stack.Add(new KeyValuePair<string, int>(STRUCT_TAG, i));
                            }
                            else
                            {
                                stack.Add(new KeyValuePair<string, int>(NON_FUNCTION_TAG, i));
                            }
                        }
                    }
                    else
                    {
                        stack.Add(new KeyValuePair<string, int>(NON_FUNCTION_TAG, i));
                    }
                }

                if (tokens[i] == "}")
                {
                    if (stack.Count == 0)
                    {
                        throw new BracketBalanceException("{", i, tokens);
                    }

                    KeyValuePair<string, int> top = stack[stack.Count - 1];
                    if (top.Key == FUNCTION_TAG || top.Key == STRUCT_TAG)
                    {
                        borders.Add(top.Value, i);
                        tags.Add(top.Key);
                        isFunction = false;
                        isStruct = false;
                    }

                    if (stack.Count > 0)
                    {
                        stack.RemoveAt(stack.Count - 1);
                    }
                    else
                    {
                        throw new BracketBalanceException("{", i, tokens);
                    }
                }
            }

            if (stack.Count != 0)
            {
                throw new BracketBalanceException("{", 0, tokens);
            }

            int counter = 0;



            foreach (var i in borders)
            {
                int left = i.Key;
                int right = i.Value;


                for (int j = left; j <= right; j++ )
                {
                    used[j] = true;
                }

                if (tags[counter] == FUNCTION_TAG)
                {
                    List<string> bodyTokens = new List<string>();
                    for (int j = left + 1; j < right; j++)
                    {
                        bodyTokens.Add(tokens[j]);
                    }
                    FunctionBody body = determineFunction(left, tokens, used);

                    res.addFunction(body, bodyTokens);
                }
                else
                {
                    List<string> bodyTokens = new List<string>();
                    for (int j = left + 1; j < right; j++)
                    {
                        bodyTokens.Add(tokens[j]);
                    }
                    StructureDeclaration body = new StructureDeclaration(tokens[left - 1], bodyTokens);

                    used[left - 2] = true;
                    used[left - 1] = true;
                    res.addStruct(body, tokens);
                }
                counter++;
            }

            for (int i = 0; i < tokens.Count; i++ )
            {
                if(!used[i]){
                    res.addOuterToken(tokens[i]);
                }
            }

            return res;
        }
    }
}
