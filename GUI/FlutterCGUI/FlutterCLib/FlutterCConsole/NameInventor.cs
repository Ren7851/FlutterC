using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class NameInventor
    {
        static int whiles = 0;
        static int conds = 0;
        static int linears = 0;
        static int num = 1;

        public static void reset()
        {
            whiles = 0;
            conds = 0;
            linears = 0;
            num = 0;
        }

        public static string makeBlockName(string type) {
            if(type == "w"){
                whiles++;
                return "while" + (whiles);
            }

            if (type == "l")
            {
                linears++;
                return "linear" + (linears);
            }

            if (type == "i")
            {
                conds++;
                return "if" + (conds);
            }

            throw new ArgumentException();
        }

        public static int numb()
        {
            num++;
            return num;
        }
    }
}
