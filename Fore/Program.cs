using Common.TypoGenerator;
using DataLoader;
using DataLoader.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Types;

namespace Fore
{
    class Program
    {
        static readonly Random _Random = new Random();

        static T RandomEnumValue<T>()
        {
            return Enum
                .GetValues(typeof(T))
                .Cast<T>()
                .OrderBy(x => _Random.Next())
                .FirstOrDefault();
        }

        static void Main(string[] args)
        {
            var words = DictionaryReader.GetAllWords();
            var punctuation = new List<char> {',', '.', '!', '?'};
            var vowels = new List<string> {"a","e","i","o","u"};

            double punctChance = 0.04;
            double pluralChance = 0.3;

            for (int z = 0; z <= 5; z++)
            {
                var random = new Random();
                var currentWord = words.Where(word => word.Category == "STARTER").ElementAt(random.Next(words.Count(word => word.Category == "STARTER")));
                var previousWord = new Word();
                int maxWords = random.Next(4, 21);
                int maxChars = 160;
                var myString = String.Empty;
                for (int i = 0; i <= maxWords; i++)
                {
                    if (myString.Length < maxChars)
                    {
                        if (currentWord.GetType() == typeof (Verb))
                        {
                            myString += ((Verb) currentWord).ToString(RandomEnumValue<VerbState>());
                        }

                        else
                        {
                            myString += currentWord.ToString(random.NextDouble() <= pluralChance);
                        }

                        if (random.NextDouble() <= punctChance)
                            for (int a = 0; a < random.Next(1, 3); a++)
                            {
                                myString += punctuation.ElementAt(random.Next(punctuation.Count()));
                            }
                        myString += " ";

                        previousWord = currentWord;

                        var nextWordType = currentWord.Tags.ElementAt(random.Next(currentWord.Tags.Count()));
                        if (nextWordType == "any")
                        {
                            currentWord = words.ElementAt(random.Next(words.Count()));
                        }
                        else if (nextWordType == "consonantword")
                        {
                            currentWord = words.Where(word =>
                                !vowels.Contains(word.Value[0].ToString())).ElementAt(random.Next(words.Count(word => !vowels.Contains(word.Value[0].ToString()))));
                        }
                        else if (nextWordType == "vowelword")
                        {
                            currentWord = words.Where(word =>
                                vowels.Contains(word.Value[0].ToString())).ElementAt(random.Next(words.Count(word => vowels.Contains(word.Value[0].ToString()))));
                        }
                        else
                        {
                            currentWord =
                                words.Where(word => word.Category == nextWordType.ToUpper())
                                    .ElementAt(random.Next(words.Count(word => word.Category == nextWordType.ToUpper())));
                        }

                        
                    }
                }

                Console.WriteLine(TypoGenerator.Process(myString) + Environment.NewLine);
                Thread.Sleep(103);
            }
            Console.ReadLine();
        }
    }
}
