using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{
    public class CommandParser
    {
        public static List<Node> parseCommand(Node host, List<string> tokens, List<string> real, string funcname){
            if (tokens.Count > 1 && tokens[0] == "return")
            {
                List<Node> res = new List<Node>();
                tokens.RemoveAt(0);
                Variable v = host.getVariables()[FunctionBody.RETURN_VAR];
                res.Add(new PushVariable(v));
                res.AddRange(parseCommand(host, tokens, real, funcname));
                res.Add(new Assignment());
                res.Add(new Return());
                return res;
            }

            if(tokens.Count == 1 && tokens[0] == "return"){
                List<Node> res = new List<Node>();
                res.Add(new Return());
                return res;
            }
            
            if (tokens.Count == 1 && tokens[0] == "returnoff")
            {
                List<Node> res = new List<Node>();
                res.Add(new ReturnOff());
                return res;
            }

            if (tokens.Count == 1 && tokens[0] == "return")
            {
                List<Node> res = new List<Node>();
                res.Add(new Return());
                return res;
            }

            if (tokens.Count == 1 && tokens[0] == "continueoff")
            {
                List<Node> res = new List<Node>();
                res.Add(new ContinueOff(SyntaxParser.peek()));
                return res;
            }
            

            if (tokens.Count == 1 && tokens[0] == "breakoff")
            {
                List<Node> res = new List<Node>();
                res.Add(new BreakOff(SyntaxParser.peek()));
                return res;
            }

            if (tokens.Count == 1 && tokens[0] == "continue")
            {
                List<Node> res = new List<Node>();
                res.Add(new Continue(SyntaxParser.peek()));
                return res;
            }

            if (tokens.Count == 1 && tokens[0] == "break")
            {
                List<Node> res = new List<Node>();
                res.Add(new Break(SyntaxParser.peek()));
                return res;
            }

            if(isVariableDeclaration(tokens)){
                int equalsPos = Utils.indexOf("=", tokens);
                List<List<string>> spl = Utils.split(tokens, "=");
                string varname = spl[0][spl[0].Count - 1];
                spl[0].RemoveAt(spl[0].Count - 1);

                Value val = TypeParser.makeSampleValue(spl[0]);
                
                Node vardec= null;

                if(spl.Count > 1){
                    List<Node> res = new List<Node>();
                    List<string> nextDeclaration = new List<string>();
                    nextDeclaration.Add(varname);
                    nextDeclaration.Add("=");
                    nextDeclaration.AddRange(Utils.merge(spl, 1, spl.Count - 1));
                    vardec = new VariableDeclaration(host, varname, val);
                    res.Add(vardec);
                    res.AddRange(parseCommand(host, nextDeclaration, real, funcname));
                    return res;
                }
                else{
                    List<Node> res = new List<Node>();
                    vardec = new VariableDeclaration(host, varname, val);
                    res.Add(vardec);
                    return res;
                }
            }
            else{
                Node res = parseNonDeclarationCommand(host, tokens, real, funcname);
                List<Node> r = new List<Node>();
                r.Add(res);
                return r;
            }
        }

        public static Node parseNonDeclarationCommand(Node host, List<string> command, List<string> real, string funcname) {
            List<string> realCopy = new List<string>();
            for (int i = 0; i < real.Count; i++ )
            {
                realCopy.Add(real[i]);
            }
            LinearNode res = new LinearNode(host);
            List<string> coms = CommandPreparator.prepareCommand(command);
            List<Node> rem = parseString(res, coms, realCopy, funcname);
            for (int j = 0; j < rem.Count; j++ )
            {
                res.addNode(rem[j]);
            }
            return res;
        }

        public static List<Node> parseString(Node host, List<string> command, List<string> real, string funcname)
        {
            List<Node> res = ExpressionParser.parseExpression(command, host, real, funcname);
            return res;
        }

        public static bool isVariableDeclaration(List<string> tokens) {
            List<string> tokensBeforeEquals = new List<string>();
            for (int i = 0; i < tokens.Count; i++ )
            {
                if (tokens[i] != "=")
                {
                    tokensBeforeEquals.Add(tokens[i]);
                }
                else {
                    break;
                }
            }

            string varname = tokensBeforeEquals[tokensBeforeEquals.Count - 1];
            if(!Utils.checkVarName(varname)){
                return false;
            }
            else
            {
                tokensBeforeEquals.RemoveAt(tokensBeforeEquals.Count - 1);
                if (TypeParser.isValidType(tokensBeforeEquals))
                {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
    }
}
