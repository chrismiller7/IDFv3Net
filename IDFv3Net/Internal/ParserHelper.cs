using System;
using System.Collections.Generic;


namespace IDFv3Net.Internal
{
    public class ParserHelpers
    {
        public static string[] GetFields(string line)
        {
            List<string> fields = new List<string>();

            var str = "";
            bool quote = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '\"')
                {
                    quote = !quote;
                }
                else if (char.IsWhiteSpace(line[i]) && quote || !char.IsWhiteSpace(line[i]))
                {
                    str += line[i];
                }
                else
                {
                    if (str.Length > 0)
                    {
                        fields.Add(str.Trim());
                        str = "";
                    }
                }
            }

            if (str.Length > 0)
            {
                fields.Add(str.Trim());
            }

            return fields.ToArray();
        }
    }
}
