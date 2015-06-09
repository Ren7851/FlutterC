using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;
using System.Globalization;

namespace FlutterCConsole
{
    public class ConstantParser
    {
        public static string LONG_MARK = "l";
        public static string FLOAT_MARK = "f";
        public static double EPS = 0.0000000000001;


        public static Value parseConstant(string s, List<string> tokens, string function, List<string> original)
        {
            try
            {
                if (s.Length == 0)
                {
                    throw new UnexpectedItem(tokens, s, function);
                }

                if(s[0] == '$'){
                    return new FieldName(s.Substring(1));
                }

                if(s[0] == '\"' && s[s.Length - 1] == '\"'){
                    string charArray = "";
                    for (int i = 1; i < s.Length - 1; i++ )
                    {
                        charArray += (char)(s[i] - CommandPreparator.DIFF_CONSTANT);
                    }

                    Value pointer = new Value("char*", true);
                    decimal[] values = new decimal[charArray.Length+1];
                    

                    for (int i = 0; i < charArray.Length; i++ )
                    {
                        values[i] = charArray[i];
                    }
                    values[charArray.Length] = 0;

                    pointer.set(charArray.Length + 1, values);
                    return pointer;
                }

                if (s.LastIndexOf(LONG_MARK) == s.Length - LONG_MARK.Length && s.Length > LONG_MARK.Length)
                {
                    string substr = s.Substring(0, s.Length - LONG_MARK.Length);
                    long v = Int64.Parse(substr);
                    Value res = new Value("long", false);
                    res.value = v;
                    return res;
                }
                else
                {
                    if (s.LastIndexOf(FLOAT_MARK) == s.Length - FLOAT_MARK.Length && s.Length > FLOAT_MARK.Length)
                    {
                        float v = Single.Parse(s.Substring(0, s.Length - 1));
                        Value res = new Value("float", false);
                        res.value = (decimal)v;
                        return res;
                    }
                    else
                    {
                        if (s[0] == '\'' && s[s.Length - 1] == '\'')
                        {
                            if(s.Length == 2){
                                Value res = new Value("char", false);
                                res.value = ' ';
                                return res;
                            }

                            if (s.Length == 3)
                            {
                                int v = (char)(s[1] - CommandPreparator.DIFF_CONSTANT);
                                Value res = new Value("char", false);
                                res.value = v;
                                return res;
                            }
                            else
                            {
                                if (s.Length == 4 && s[1] == (char)(CommandPreparator.DIFF_CONSTANT+'\\'))
                                {

                                    char qq = (char)(s[2] - CommandPreparator.DIFF_CONSTANT);

                                    if(qq == '0'){
                                        Value res = new Value("char", false);
                                        res.value = (char)0;
                                        return res;
                                    }

                                    throw new UnexpectedItem(tokens, s, function);
                                }
                                else
                                {
                                    throw new UnexpectedItem(tokens, s, function);
                                }
                            }
                        }
                        else
                        {
                            double dv = Double.Parse(s, CultureInfo.InvariantCulture);
                            if (Math.Abs(Math.Truncate(dv) - dv) > EPS | s.Contains('.'))
                            {
                                Value res = new Value("double", false);
                                res.value = (decimal)dv;
                                return res;
                            }
                            else
                            {
                                if (dv >= Int32.MaxValue || dv <= Int32.MinValue)
                                {
                                    throw new OverflowException();
                                }
                                int v = (int)dv;
                                Value res = new Value("int", false);
                                res.value = v;
                                return res;
                            }
                        }
                    }
                }
            }
            catch(OverflowException e) {
                throw new OverflowException();
            }
            catch (Exception e)
            {
                throw new UnexpectedItem(original, s, function);
            }
        }
    }
}
