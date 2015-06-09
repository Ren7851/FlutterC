using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{
    public class CommandPreparator
    {
        public static int DIFF_CONSTANT = 150;
        public static string GET_ADDRESS = "#";

        public static void replaceInsideBrackets(List<string> source, string bracket, int diffCode)
        {
            bool isInside = false;
            for (int i = 0; i < source.Count; i++)
            {
                if (source[i] == bracket)
                {
                    bool isDisabled = false;
                    if (i > 0 && source[i - 1] == "\\")
                    {
                        isDisabled = true;
                    }

                    if (!isDisabled)
                    {
                        isInside = !isInside;
                    }
                }
                else
                {
                    if (isInside)
                    {
                        string newStr = "";
                        for (int j = 0; j < source[i].Length; j++)
                        {
                            newStr += (char)(source[i][j] + diffCode);
                        }
                        source[i] = newStr;
                    }
                }
            }
        }

        public static void resolveCrements(List<string> source)
        {
            operatorResolver(source, "++", "0preinc");
            operatorResolver(source, "--", "0predec");
            for (int i = 0; i < source.Count; i++ )
            {
                if(source[i] == "++"){
                    source[i] = "0postinc";
                }

                if (source[i] == "--")
                {
                    source[i] = "0postdec";
                }
            }
        }

        public static bool isDigit(char a)
        {
            return (a >= '0' && a <= '9');
        }

        public static void replaceConstantMarkers(List<string> source, string mark)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (i > 0)
                {
                    if (source[i] == mark && isDigit(source[i - 1][source[i - 1].Length - 1]))
                    {
                        string s = "";
                        for (int j = 0; j < mark.Length; j++)
                        {
                            s += (char)(mark[j] + DIFF_CONSTANT);
                        }
                        source[i] = s;
                    }
                }
            }
        }

        public static void operatorResolver(List<string> source, string template, string substitution){
            bool isCast = false;
            for(int i = 0; i<source.Count; i++){
                if(source[i] == "?"){
                    isCast = !isCast;
                }

                if(isCast){
                    continue;
                }

                if (source[i] == template)
                {
                    if (i != 0 && (OperatorSign.allSigns.Contains(source[i - 1]) || source[i - 1] == "?") && source[i - 1] != ")")
                    {
                        source[i] = substitution;
                    }
                    if(i == 0){
                        source[i] = substitution;
                    }
                }
            }
        }

        public static List<string> prepareSizeof(List<string> source)
        {
            for (int i = 0; i < source.Count; i++ )
            {
                if(source[i] == "sizeof"){
                    List<string> type = new List<string>();
                    if(i!=source.Count - 1 && source[i+1] == "("){
                        int ende = 0;
                        for (int j = i+2; j < source.Count; j++ )
                        {
                            if(source[j] == ")"){
                                ende = j;
                                break;
                            }
                            type.Add(source[j]);
                        }

                        if (TypeParser.isValidType(type))
                        {
                            List<string> res = new List<string>();
                            for (int j = 0; j < i; j++)
                            {
                                res.Add(source[j]);
                            }
                            res.Add("1");
                            for (int j = ende + 1; j < source.Count; j++)
                            {
                                res.Add(source[j]);
                            }
                            return prepareSizeof(res);
                        }
                    }
                }
            }
            return source;
        }

        public static List<string> obtainArrayLits(List<string> source)
        {
            for (int i = 1; i < source.Count; i++ )
            {
                if(source[i] == "["){
                    int balance = -1;
                    for (int j = i + 1; j < source.Count; j++)
                    {
                        if (source[j] == "]")
                        {
                            balance++;

                            if(balance!=0){
                                continue;
                            }

                            List<string> variable = new List<string>();
                            if(i == 0){
                                throw new BracketBalanceException("[", 0, source);
                            }

                            if(source[i-1]!=")"){
                                variable.Add(source[i-1]);
                            }
                            else
                            {
                                int bal = -1;
                                for (int q = i - 2; q >= 0; q--)
                                {
                                    if(source[q] == "("){
                                        bal++;
                                    }

                                    if (source[q] == ")")
                                    {
                                        bal--;
                                    }

                                    if(bal == 0){
                                        break;
                                    }

                                    variable.Add(source[q]);
                                }
                            }
                            variable.Reverse();



                            List<string> res = new List<string>();
                            if(variable.Count != 1){
                                for (int k = 0; k < i - variable.Count - 2; k++)
                                {
                                    res.Add(source[k]);
                                }
                            }
                            else
                            {
                                for (int k = 0; k < i - 1; k++)
                                {
                                    res.Add(source[k]);
                                }
                            }

                            res.Add("(");
                            res.Add("*");
                            res.Add("(");
                            res.AddRange(variable);
                            res.Add("+");
                            res.Add("(");
                            for (int k = i + 1; k < j; k++)
                            {
                                res.Add(source[k]);
                            }
                            res.Add(")");
                            res.Add(")");
                            res.Add(")");
                            for (int k = j + 1; k < source.Count; k++)
                            {
                                res.Add(source[k]);
                            }
                            return obtainArrayLits(res);
                        }
                    }
                    throw new BracketBalanceException("[", i, source);
                }
            }
            return source;
        }

        public static void findCastOperators(List<string> source){
            for (int i = 0; i < source.Count; i++ )
            {
                for (int j = i; j < source.Count; j++ )
                {
                    if(source[i] == "(" && source[j] == ")"){
                        if(i!=0 && source[i-1] == "sizeof"){
                            break;
                        }
                        if(ExpressionParser.isRaw(source, i+1, j-1)){
                            source[i] = "?";
                            source[j] = "?";
                        }
                    }
                }
            }
        }

        public static void makePrintf(List<string> source)
        {
            for (int i = 0; i < source.Count; i++ )
            {
                if(source[i] == "printf"){
                    source[i + 3] = ('ё') + source[i + 3];
                }
            }

            for (int i = 0; i < source.Count; i++)
            {
                if (source[i] == "scanf")
                {
                    source[i + 3] = ('ё') + source[i + 3];
                }
            }
        }

        public static void countFunctionParams(List<string> source)
        {
            List<KeyValuePair<string, int>> rawNames = Memory.getInstance().getFunctionsRawNames();
            for (int i = 0; i < source.Count; i++ )
            {
                for (int j = 0; j < rawNames.Count; j++ )
                {
                    if (source[i] == rawNames[j].Key && (i != source.Count - 1 && source[i + 1] == "(") && rawNames[j].Key != "printf" && rawNames[j].Key != "scanf")
                    {
                        int pointer = i+2;
                        int count = 1;
                        int balance = -1;
                        bool found = false;
                        bool isSomething = false;
                        while(pointer < source.Count){
                            if(source[pointer] == "," && balance == -1){
                                //Console.WriteLine(i+" "+pointer);
                                count++;
                            }

                            if(source[pointer] == ")"){
                                balance++;
                            }

                            if (source[pointer] == "(")
                            {
                                balance--;
                            }

                            if(balance == 0){
                                found = true;
                                break;
                            }

                            isSomething = true;

                            pointer++;
                        }

                        if(!isSomething){
                            count = 0;
                        }

                        if(found){
                            if(count == rawNames[j].Value){
                                source[i] = rawNames[j].Key + "$" + rawNames[j].Value;
                            }
                        }
                    }
                }
            }
        }

        public static List<string> handleFieldDereferencings(List<string> source)
        {
            for (int i = 0; i < source.Count; i++ )
            {
                if (source[i].IndexOf('.') != -1 && (source[i].Length>source[i].IndexOf('.')+1) && !(source[i][source[i].IndexOf('.') + 1] >= '0' && source[i][source[i].IndexOf('.') + 1] <= '9'))
                {
                    int position = source[i].IndexOf('.');
                    if(position!=source[i].Length - 1){
                        string behind = source[i].Substring(0, position);
                        string after = source[i].Substring(position+1);

                        List<string> res = new List<string>();
                        for (int j = 0; j < i; j++ )
                        {
                            if(source[j]!=""){
                                res.Add(source[j]);
                            }
                        }

                        if(behind.Length > 0){
                            res.Add(behind);
                        }
                        res.Add("$field");
                        res.Add("$"+after);

                        for (int j = i+1; j < source.Count; j++)
                        {
                            if (source[j] != "")
                            {
                                res.Add(source[j]);
                            }
                        }
                        return handleFieldDereferencings(res);
                    }
                }
            }
            return source;
        }

        public static List<string> prepareCommand(List<string> source)
        {
            source = obtainArrayLits(source);
            source = handleFieldDereferencings(source);
            makePrintf(source);
            replaceInsideBrackets(source, "\"", DIFF_CONSTANT);
            replaceInsideBrackets(source, "\'", DIFF_CONSTANT);
            findCastOperators(source);
            source = prepareSizeof(source);
            countFunctionParams(source);
            replaceConstantMarkers(source, ConstantParser.LONG_MARK);
            replaceConstantMarkers(source, ConstantParser.FLOAT_MARK);
            operatorResolver(source, "&", "@");
            operatorResolver(source, "*", "#");
            operatorResolver(source, "+", "u+");
            operatorResolver(source, "-", "u-");
            resolveCrements(source);
            return source;
        }
    }
}
