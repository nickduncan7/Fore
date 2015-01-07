using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TypoGenerator.Types
{
    public class Letter
    {
        public char Value;
        public string NearKeys;

        public Letter()
        {
            NearKeys = String.Empty;
        }
    }
}
