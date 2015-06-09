using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlutterCConsole;

namespace FlutterCConsole
{
    public class TokensCollector
    {
        public static List<string> collectTokens(List<string> contents)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < contents.Count; i++)
            {
                Preprocessor p = new Preprocessor(contents[i]);
                p.process();
                List<string> tokens = p.getTokens();

                for (int j = 0; j < tokens.Count; j++ )
                {
                    res.Add(tokens[j]);
                }
            }
            return res;
        }
    }
}
