using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{
    public class Preprocessor
    {
        private static StringBuilder DELIMITER = new StringBuilder("$$$");
        private List<StringBuilder> tokens;
        private string s;

        public Preprocessor(string content)
        {
            string a = content;
            StringBuilder res = new StringBuilder();
            res.Append('\n');
            res.Append('\n');
            res.Append('\n');
            res.Append(a);
            res.Append('\n');
            res.Append('\n');
            s = res.ToString();
        }

        public List<string> getTokens() {
            List<string> newList = new List<string>();
            for (int i = 0; i < tokens.Count; i++ )
            {
                if (tokens[i] != DELIMITER)
                {
                    newList.Add(tokens[i].ToString());
                }
            }
            return newList;
        } 

        public void process()
        {
            s = Utils.eliminateComments(s);
            //Console.WriteLine(s);
            tokenize();
            parseConditions();
            lookForUnsupportedFeatures();
            replacement();
        }

        public override string ToString()
        {
            string res = tokens.Count + " items :\n";
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == DELIMITER)
                {
                    res += "\n";
                    continue;
                }
                else
                {
                    res += tokens[i];
                }
                res += " ";
            }
            return res;
        }

        private void deleteComments()
        {
        }


        private void makeConditionMap(Dictionary<int, int> res, List<StringBuilder> l)
        {
            List<int> s = new List<int>();

            int i = 0;
            int it = 0;
            while (i < l.Count)
            {
                StringBuilder label = l[it];
                bool isIf = false;
                bool isElse = false;
                bool isEndif = false;


                if (label.ToString() == "#ifdef")
                {
                    isIf = true;
                }

                if (label.ToString() == "#ifndef")
                {
                    isIf = true;
                }

                if (label.ToString() == "#else")
                {
                    isElse = true;
                }

                if (label.ToString() == "#endif")
                {
                    isEndif = true;
                }

                if (isIf)
                {
                    s.Add(i);
                }

                if (isElse)
                {
                    if (s.Count == 0)
                    {
                        throw new InvalidDataException("Conditional macros error");
                    }
                    int var = s[s.Count - 1];
                    res[var] = i + 1;
                }

                if (isEndif)
                {
                    if (s.Count == 0)
                    {
                        throw new InvalidDataException("Conditional macros error");
                    }
                    int var = s[s.Count - 1];
                    if (!res.ContainsKey(var))
                    {
                        res[var] = i + 1;
                    }
                    else
                    {
                        int els = res[var] - 1;
                        res[els] = i + 1;
                    }
                    s.Remove(s.Last());
                }

                i++;
                it++;
            }

            if (s.Count != 0)
            {
                throw new InvalidDataException("Conditional macros error");
            }
        }

        List<StringBuilder> getMacroTokensUntillEnd(int index)
        {
            int balance = -1;
            List<StringBuilder> res = new List<StringBuilder>();
            while (index < tokens.Count)
            {
                StringBuilder entry = tokens[index];
                if (entry == DELIMITER)
                {
                    balance++;
                }

                if (entry.ToString() == "\\")
                {
                    balance--;
                }

                if (balance == 0)
                {
                    return res;
                }

                if (entry == DELIMITER)
                {
                    index++;
                    continue;
                }

                if (entry.ToString() == "\\")
                {
                    index++;
                    continue;
                }

                res.Add(entry);
                index++;
            }
            return res;
        }

        private StringBuilder getSub(int from, int to)
        {
            StringBuilder str = new StringBuilder();
            for (int q = from; q <= to; q++)
            {
                str.Append(s[q]);
            }
            return str;
        }

        private void clean()
        {
            List<StringBuilder> newlist = new List<StringBuilder>();
            for (int j = 0; j < tokens.Count; j++)
            {
                bool g = true;

                for (int q = 0; q < tokens[j].Length; q++)
                {
                    if (tokens[j][q] != ' ' && tokens[j][q] != '\r' && tokens[j][q] != '\t' && tokens[j][q] != '\n')
                    {
                        g = false;
                    }
                }
                if (g)
                {
                    continue;
                }
                else
                {
                    newlist.Add(tokens[j]);
                }
            }

            tokens = newlist;
        }

        public void replacement()
        {
            bool[] delete = new bool[tokens.Count];
            for (int i = 0; i < tokens.Count; i++ )
            {
                if(tokens[i].ToString() == "void"){
                    tokens[i] = new StringBuilder("char");
                }

                if (tokens[i].ToString() == "struct")
                {
                    tokens[i] = new StringBuilder("$");
                }

                if (tokens[i].ToString() == "bool")
                {
                    tokens[i] = new StringBuilder("char");
                }

                if (tokens[i].ToString() == "true")
                {
                    tokens[i] = new StringBuilder("1");
                }

                if (tokens[i].ToString() == "false")
                {
                    tokens[i] = new StringBuilder("0");
                }
            }

            for (int i = 0; i < tokens.Count-1; i++ )
            {
                string th = tokens[i].ToString();
                string next = tokens[i+1].ToString();
                if(th == "long" && next == "int"){
                    delete[i+1] = true;
                    i += 2;
                }

                if (th == "long" && next == "double")
                {
                    delete[i] = true;
                    i += 2;
                }

                if (th == "long" && next == "long")
                {
                    delete[i] = true;
                }

                if (th == "long" && next == "float")
                {
                    delete[i] = true;
                    tokens[i + 1] = new StringBuilder("double");
                    i += 2;
                }
            }


            List<StringBuilder> fuck = new List<StringBuilder>();
            for (int i = 0; i < tokens.Count; i++ )
            {
                if(delete[i] == false){
                    fuck.Add(tokens[i]);
                }
            }
            tokens = fuck;
        }

        public void lookForUnsupportedFeatures()
        {
            for (int i = 0; i < tokens.Count; i++ )
            {
                string token = tokens[i].ToString();
                if(token == "const"){
                    throw new FlutterCConsole.Exceptions.UnsupportedFeatureException(token);
                }

                if (token == "goto")
                {
                    throw new FlutterCConsole.Exceptions.UnsupportedFeatureException(token);
                }

                if (token == "label")
                {
                    throw new FlutterCConsole.Exceptions.UnsupportedFeatureException(token);
                }

                if (token == "union")
                {
                    throw new FlutterCConsole.Exceptions.UnsupportedFeatureException(token);
                }

                if (token == "enum")
                {
                    throw new FlutterCConsole.Exceptions.UnsupportedFeatureException(token);
                }

                if (token == "register")
                {
                    throw new FlutterCConsole.Exceptions.UnsupportedFeatureException(token);
                }

                if (token == "unsigned")
                {
                    throw new FlutterCConsole.Exceptions.UnsupportedFeatureException(token);
                }

                if (token == "do")
                {
                    throw new FlutterCConsole.Exceptions.UnsupportedFeatureException(token);
                }

                if (token == "case")
                {
                    throw new FlutterCConsole.Exceptions.UnsupportedFeatureException(token);
                }
            }
        }

        private void tokenize()
        {
            List<char> specialSingleSigns = new List<char>();
            List<Tuple<char, char>> specialDoubleSigns = new List<Tuple<char, char>>();

            specialSingleSigns.Add(',');
            specialSingleSigns.Add('~');
            specialSingleSigns.Add(',');
            specialSingleSigns.Add(';');
            specialSingleSigns.Add('\'');
            specialSingleSigns.Add(')');
            specialSingleSigns.Add('!');
            specialSingleSigns.Add('(');
            specialSingleSigns.Add('"');
            specialSingleSigns.Add('[');
            specialSingleSigns.Add(']');
            //specialSingleSigns.Add('.');
            specialDoubleSigns.Add(new Tuple<char, char>('+', '+'));
            specialDoubleSigns.Add(new Tuple<char, char>('-', '-'));
            specialDoubleSigns.Add(new Tuple<char, char>('=', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('!', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('>', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('<', '='));
            specialSingleSigns.Add('*');
            specialSingleSigns.Add('=');
            specialSingleSigns.Add('{');
            specialSingleSigns.Add('}');
            specialSingleSigns.Add('/');
            specialSingleSigns.Add('+');
            specialSingleSigns.Add('-');
            specialSingleSigns.Add('%');
            specialSingleSigns.Add('?');
            specialDoubleSigns.Add(new Tuple<char, char>('&', '&'));
            specialDoubleSigns.Add(new Tuple<char, char>('|', '|'));
            specialSingleSigns.Add('&');
            specialSingleSigns.Add('|');
            specialSingleSigns.Add('^');
            specialDoubleSigns.Add(new Tuple<char, char>('>', '>'));
            specialDoubleSigns.Add(new Tuple<char, char>('<', '<'));

            specialDoubleSigns.Add(new Tuple<char, char>('+', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('-', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('*', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('/', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('%', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('&', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('|', '='));
            specialDoubleSigns.Add(new Tuple<char, char>('^', '='));

            specialSingleSigns.Add('<');
            specialSingleSigns.Add('>');

            bool isFuckedString = false;
            string buffer = "";

            int len = s.Length;
            tokens = new List<StringBuilder>();

            int last = -1;
            int i = 0;
            while (i < len)
            {

                if(s[i] == '\"' && !isFuckedString){
                    isFuckedString = true;
                    tokens.Add(new StringBuilder("\""));
                    i++;
                    continue;
                }


                if (isFuckedString && s[i] != '\"')
                {
                    buffer += s[i];
                    i++;
                    continue;
                }


                if (s[i] == '\"' && isFuckedString)
                {
                    isFuckedString = false;
                    tokens.Add(new StringBuilder(buffer));
                    tokens.Add(new StringBuilder("\""));
                    last = i+1;
                    buffer = "";
                    i++;
                    continue;
                }



                bool doubl = false;
                bool triple = false;

                if(i<len-2){
                    if (s[i] == '>' && s[i + 1] == '>' && s[i + 2] == '=')
                    {
                        tokens.Add(new StringBuilder(">>="));
                        last = i + 3;
                        i = last - 1;
                        triple = true;
                    }
                    else
                    {
                        if (s[i] == '<' && s[i + 1] == '<' && s[i + 2] == '=')
                        {
                            tokens.Add(new StringBuilder("<<="));
                            last = i + 3;
                            i = last - 1;
                            triple = true;
                        }
                    }
                }

                if(triple){
                    i++;
                    continue;
                }

                for (int j = 0; j < specialDoubleSigns.Count; j++)
                {
                    if (s[i] == specialDoubleSigns[j].Item1 && i < len - 1 && s[i + 1] == specialDoubleSigns[j].Item2)
                    {
                        if (last <= i - 1)
                        {
                            tokens.Add(getSub(last, i - 1));
                        }
                        tokens.Add(getSub(i, i + 1));
                        last = i + 2;
                        i = last - 1;
                        doubl = true;
                        break;
                    }
                }

                if (!doubl)
                {
                    for (int j = 0; j < specialSingleSigns.Count; j++)
                    {
                        if (s[i] == specialSingleSigns[j])
                        {
                            if (last <= i - 1)
                            {
                                tokens.Add(getSub(last, i - 1));
                            }
                            tokens.Add(new StringBuilder(s[i] + ""));
                            last = i + 1;
                            break;
                        }
                    }
                }

                if (s[i] == ' ' || s[i] == '\n' || s[i] == '\t' || s[i] == '\r')
                {
                    if (last != -1 && last <= i - 1)
                    {
                        tokens.Add(getSub(last, i - 1));
                    }
                    last = i + 1;
                    if (s[i] == '\n')
                    {
                        tokens.Add(DELIMITER);
                    }
                }
                i++;
            }
        }

        private void parseConditions()
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            makeConditionMap(map, tokens);

            List<StringBuilder> newList = new List<StringBuilder>();

            Dictionary<string, List<StringBuilder>> simpleDefines = new Dictionary<string, List<StringBuilder>>();

            int it = 0;
            int i = 0;
            while (it < tokens.Count)
            {
                StringBuilder entry = tokens[it];
                if (entry.ToString() == "#define")
                {
                    char next = tokens[it + 1][0];
                    if (next != '(')
                    {
                        List<StringBuilder> means;
                        means = getMacroTokensUntillEnd(it);
                        means.Add(DELIMITER);
                        string s = tokens[it + 1].ToString();
                        simpleDefines[s] = means;
                    }
                }

                if (entry.ToString() == "#ifndef")
                {
                    StringBuilder value = tokens[it + 1];
                    if (simpleDefines.ContainsKey(value.ToString()) && simpleDefines[value.ToString()].Count != 0)
                    {
                        int next = map[i];
                        int diff = next - i;
                        i += diff;
                        for (int q = 0; q < diff; q++)
                        {
                            it++;
                        }
                    }
                    else
                    {
                        it++;
                        it++;
                        i++;
                        i++;
                    }
                }

                if (entry.ToString() == "#ifdef")
                {
                    StringBuilder value = tokens[it + 1];
                    if (!(simpleDefines.ContainsKey(value.ToString()) && simpleDefines[value.ToString()].Count != 0))
                    {
                        int next = map[i];
                        int diff = next - i;
                        i += diff;
                        for (int q = 0; q < diff; q++)
                        {
                            it++;
                        }
                    }
                    else
                    {
                        it++;
                        it++;
                        i++;
                        i++;
                    }
                }


                if (entry.ToString() == "#undef")
                {
                    StringBuilder value = tokens[it + 1];
                    string realString = value.ToString();
                    simpleDefines[realString] = new List<StringBuilder>();
                }


                if (entry.ToString() == "#endif")
                {
                    it++;
                    i++;
                }


                if (entry.ToString() == "#else")
                {
                    int next = map[i];
                    int diff = next - i;
                    i += diff;
                    for (int q = 0; q < diff; q++)
                    {
                        it++;
                    }
                }

                newList.Add(tokens[it]);
                i++;
                it++;
            }

            tokens = newList;
        }
    }
}