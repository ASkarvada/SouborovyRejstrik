using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SouborovýRejstřík
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("ThreeMenInABoatEnglish.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string t = sr.ReadToEnd();
            sr.Close();
            fs.Close();

            Dictionary<string, List<int>> cetnost = new Dictionary<string, List<int>>();

            t = t.ToLower().Replace(".", " ").Replace(",", " ").
                    Replace("'", " ").Replace("/", " ").Replace(":", " ").
                    Replace(")", " ").Replace("(", " ").Replace(";", " ").
                    Replace("[", " ").Replace("]", " ").Replace("*", " ").
                    Replace("_", " ").Replace("@", " ").Replace("#", " ");

            string[] lines = t.Split(
    new[] { Environment.NewLine },
    StringSplitOptions.None
);

            int radek = 1;            
            foreach (var line in lines)
            {
                string[] t_arr = line.Split(' ');
                foreach (var item in t_arr)
                {
                    if (item != "and" && item != "or" && item != "the" && item != " " && item != "" && item != "a" && item != "in" && item != "of" && item != "at" && item != "it" && item != "to" && item != "for")
                    {
                        if (cetnost.ContainsKey(item))
                        {
                            cetnost[item].Add(radek);
                        }
                        else
                        {
                            cetnost.Add(item, new List<int>());
                            cetnost[item].Add(radek);
                        }
                    }
                }
                radek++;
            }


            StringBuilder sb = new StringBuilder();
            sb.Append("Rejstřík: \n\n");

            foreach (var item in cetnost)
            {
                sb.Append(item.Key + " - ");
                foreach (var stranky in item.Value)
                {
                    sb.Append(stranky + ", ");
                }
                sb.Append("\n\n");
            }

            fs = new FileStream("rejstrik.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(sb);
            sw.Close();
            fs.Close();
            

        }
    }
}
