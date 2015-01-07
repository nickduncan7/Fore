using Common.TypoGenerator.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TypoGenerator
{
    public static class TypoGenerator
    {
        private static double chance = 0.027;
        private static double capsChance = 0.04;

        public static string Process(string input)
        {
            #region Letter Definitions
            List<Letter> letters;
            letters = new List<Letter>();
            letters.Add(new Letter { Value = 'q', NearKeys = "12wsa" });
            letters.Add(new Letter { Value = 'w', NearKeys = "123qeasd" });
            letters.Add(new Letter { Value = 'e', NearKeys = "234wrsdf" });
            letters.Add(new Letter { Value = 'r', NearKeys = "345etdfg" });
            letters.Add(new Letter { Value = 't', NearKeys = "456ryfgh" });
            letters.Add(new Letter { Value = 'y', NearKeys = "567tughj" });
            letters.Add(new Letter { Value = 'u', NearKeys = "678yihjk" });
            letters.Add(new Letter { Value = 'i', NearKeys = "789uojkl" });
            letters.Add(new Letter { Value = 'o', NearKeys = "890ipkl;" });
            letters.Add(new Letter { Value = 'p', NearKeys = "90-o[l;'" });
            letters.Add(new Letter { Value = 'a', NearKeys = "qwsxz" });
            letters.Add(new Letter { Value = 's', NearKeys = "qweadzxc" });
            letters.Add(new Letter { Value = 'd', NearKeys = "wersfxcv" });
            letters.Add(new Letter { Value = 'f', NearKeys = "ertdgcvb" });
            letters.Add(new Letter { Value = 'g', NearKeys = "rtyfhvbn" });
            letters.Add(new Letter { Value = 'h', NearKeys = "tyugjbnm" });
            letters.Add(new Letter { Value = 'j', NearKeys = "yuihknm," });
            letters.Add(new Letter { Value = 'k', NearKeys = "uiojlm,." });
            letters.Add(new Letter { Value = 'l', NearKeys = "iopk;,./" });
            letters.Add(new Letter { Value = 'z', NearKeys = "asx" });
            letters.Add(new Letter { Value = 'x', NearKeys = "asdzc" });
            letters.Add(new Letter { Value = 'c', NearKeys = "sdfxv " });
            letters.Add(new Letter { Value = 'v', NearKeys = "dfgcb " });
            letters.Add(new Letter { Value = 'b', NearKeys = "fghvn " });
            letters.Add(new Letter { Value = 'n', NearKeys = "ghjbm " });
            letters.Add(new Letter { Value = 'm', NearKeys = "jkl,. " });
            letters.Add(new Letter { Value = 'Q', NearKeys = "!@WSA" });
            letters.Add(new Letter { Value = 'W', NearKeys = "!@#QEASD" });
            letters.Add(new Letter { Value = 'E', NearKeys = "@#$WRSDF" });
            letters.Add(new Letter { Value = 'R', NearKeys = "#$%ETDFG" });
            letters.Add(new Letter { Value = 'T', NearKeys = "$%^RYFGH" });
            letters.Add(new Letter { Value = 'Y', NearKeys = "%^&TUGHJ" });
            letters.Add(new Letter { Value = 'U', NearKeys = "^&*YIHJK" });
            letters.Add(new Letter { Value = 'I', NearKeys = "&*(UOJKL" });
            letters.Add(new Letter { Value = 'O', NearKeys = "*()IPKL:" });
            letters.Add(new Letter { Value = 'P', NearKeys = "()_O{L;'" });
            letters.Add(new Letter { Value = 'A', NearKeys = "QWSXZ" });
            letters.Add(new Letter { Value = 'S', NearKeys = "QWEADZXC" });
            letters.Add(new Letter { Value = 'D', NearKeys = "WERSFXCV" });
            letters.Add(new Letter { Value = 'F', NearKeys = "ERTDGCVB" });
            letters.Add(new Letter { Value = 'G', NearKeys = "RTYFHVBN" });
            letters.Add(new Letter { Value = 'H', NearKeys = "TYUGJBNM" });
            letters.Add(new Letter { Value = 'J', NearKeys = "YUIHKNM<" });
            letters.Add(new Letter { Value = 'K', NearKeys = "UIOJLM<>" });
            letters.Add(new Letter { Value = 'L', NearKeys = "IOPK:<>?" });
            letters.Add(new Letter { Value = 'Z', NearKeys = "ASX" });
            letters.Add(new Letter { Value = 'X', NearKeys = "ASDZC" });
            letters.Add(new Letter { Value = 'C', NearKeys = "SDFXV " });
            letters.Add(new Letter { Value = 'V', NearKeys = "DFGCB " });
            letters.Add(new Letter { Value = 'B', NearKeys = "FGHVN " });
            letters.Add(new Letter { Value = 'N', NearKeys = "GHJBM " });
            letters.Add(new Letter { Value = 'M', NearKeys = "JKL<> " }); 
            #endregion

            var idx = 0;

            var rng = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            bool makeCaps = false;
            int capsCounter = 0;

            var newString = String.Empty;

            input.ToList().ForEach(let =>
            {
                bool makeTypo = rng.NextDouble() <= chance;
                
                if (!makeCaps)
                {
                    if (rng.NextDouble() <= capsChance)
                    {
                        makeCaps = true;
                        capsCounter = rng.Next(1, 7);
                    }
                }

                if (capsCounter >= 1)
                {
                    let = let.ToString().ToUpper()[0];
                    capsCounter--;

                    if (capsCounter == 0)
                        makeCaps = false;

                }


                if (makeTypo && letters.Any(x => x.Value == let))
                {
                    bool SwitchLetters = rng.NextDouble() <= 0.5;
                    if (SwitchLetters)
                    {
                        int currentPos = idx;
                        int swapPos = currentPos + 1 >= input.Length - 1 ? currentPos - 1 : currentPos + 1;

                        newString += input[swapPos];
                        newString += input[currentPos];
                    }
                    else
                    {
                        int PossibleTypos = letters.First(x => x.Value == let).NearKeys.Length;
                        newString += letters.First(x => x.Value == let).NearKeys[rng.Next(PossibleTypos)];
                    }
                }
                else
                {
                    newString += let;
                }
                idx++;
            });

            return newString;
        }
    }
}
