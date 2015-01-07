using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader.Types
{
    public class Word
    {
        public List<string> Tags { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }
        public string Pluralization { get; set; }

        public Word()
        {
            Tags = new List<string>();
        }

        public string ToString(bool plural = false)
        {
            if (plural && !string.IsNullOrEmpty(Pluralization))
            {
                int subtract = Pluralization.Count(x => x == '-');
                string ret = Value.Remove((Value.Length - subtract), subtract);
                ret += Pluralization.Trim('-');
                return ret;
            }
            else
            {
                return Value;
            }
        }
    }
}

