using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace FlutterCGUI
{
    public class FlutterCProject
    {
        public List<string> cfiles;
        public List<string> shortNames;

        public FlutterCProject()
        {
            cfiles = new List<string>();
            shortNames = new List<string>();
            //addFile("main.c");
        }

        public static string cropFile(string s)
        {
            if(s.IndexOf('\\')!=-1){
                return s.Substring(s.LastIndexOf('\\')+1);
            }
            else
            {
                return s;
            }
        }

        public void addFile(string s)
        {
            string cr = cropFile(s);
            cfiles.Add(s);
            shortNames.Add(cropFile(s));
        }

        public bool canAdd(string s)
        {
            if(shortNames.Contains(cropFile(s))){
                return false;
            }
            else{
                return true;
            }
        }

        public void removeAt(int i)
        {
            cfiles.RemoveAt(i);
            shortNames.RemoveAt(i);
        }

        public static string Serialize(FlutterCProject data)
        {
            var serializer = new XmlSerializer(typeof(FlutterCProject));

            TextWriter writer = new StringWriter();
            serializer.Serialize(writer, data);

            return writer.ToString();
        }

        public static FlutterCProject Deserialize(string tData)
        {
            var serializer = new XmlSerializer(typeof(FlutterCProject));

            TextReader reader = new StringReader(tData);

            return (FlutterCProject)serializer.Deserialize(reader);
        }        
    }
}
