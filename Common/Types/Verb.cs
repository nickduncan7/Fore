using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLoader.Types;

namespace Common.Types
{
    public class Verb : Word
    {
        public string Present { get; set; }
        public string Preterite { get; set; }
        public string PresentThirdPerson { get; set; }

        public string ToString(VerbState state)
        {
            switch (state)
            {
                case VerbState.Past:
                    return Preterite;
                case VerbState.Future:
                    return "will " + Value;
                case VerbState.Present:
                    return Present;
                case VerbState.PresentThirdPerson:
                    return PresentThirdPerson;
            }
            return String.Empty;
        }
    }

    public enum VerbState
    {
        Present, Past, PresentThirdPerson, Future
    }
}
