using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{


    public class TypeParser
    {
        public static bool isValidType(List<string> tokens){
            if (tokens.Count == 0){
                return false;
            }

            if(tokens[0] == "$" || tokens.Count > 1){
                Dictionary<string, StructureDeclaration> declarations = Memory.instance.availableStructs;
                if(declarations.ContainsKey(tokens[1])){
                    for (int i = 2; i < tokens.Count; i++ )
                    {
                        if(tokens[i]!="*"){
                            return false;
                        }
                    }
                    return true;
                }
            }

            if(tokens.Count == 1 && (tokens[0] == "int" || tokens[0] == "float" || tokens[0] == "char" || tokens[0] == "double" || tokens[0] == "long")){
                return true;
            }
            else{
                if ((tokens[0] == "int" || tokens[0] == "float" || tokens[0] == "char" || tokens[0] == "double" || tokens[0] == "long"))
                {
                    for (int i = 1; i < tokens.Count; i++ )
                    {
                        if(tokens[i]!="*"){
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public static Value makeSampleValue(List<string> tokens)
        {
            if (tokens.Count == 1 && tokens[0] == "int")
            {
                return new Value("int", true);
            }

            if (tokens.Count == 1 && tokens[0] == "char")
            {
                return new Value("char", true);
            }

            if (tokens.Count == 1 && tokens[0] == "long")
            {
                return new Value("long", true);
            }

            if (tokens.Count == 1 && tokens[0] == "float")
            {
                return new Value("float", true);
            }

            if (tokens.Count == 1 && tokens[0] == "double")
            {
                return new Value("double", true);
            }

            if(tokens[tokens.Count - 1] != "*"){
                if(tokens[0]!="$"){
                    throw new UnexpectedTypeException(0, tokens);
                }
                string ss = "";
                for (int i = 1; i < tokens.Count; i++ )
                {
                    ss += tokens[i];
                }

                var description = Memory.instance.availableStructs;
                if(!description.ContainsKey(ss)){
                    throw new UnexpectedTypeException(0, tokens);
                }
                else
                {
                    return new Value("$"+ss, true);
                }
            }
            else{
                string ss = Utils.merge(tokens);
                string typename = ss.Substring(0, ss.Length - 1);
                return new Value(typename+"*", true);
            }
        }
    }
}
