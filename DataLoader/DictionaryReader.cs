using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Types;
using DataLoader.Types;

namespace DataLoader
{
    public static class DictionaryReader
    {
        private static readonly string dictionaryLocation = @"../../../Dictionaries";

        public static List<string> GetDictionaries()
        {
            return new List<string>(Directory.GetFiles(dictionaryLocation, "*.fdict"));
        }

        public static List<Word> GetAllWords()
        {
            var dictionaries = GetDictionaries();
            var ret = new List<Word>();

            ret.AddRange(GetArticles());
            ret.AddRange(GetAuxiliaryVerbs());

            dictionaries.ForEach(dict =>
            {
                var line = String.Empty;
                var category = String.Empty;

                var linecount = 0;

                var parsingDefinitions = false;

                var file = new StreamReader(dict);
                while ((line = file.ReadLine()) != null)
                {
                    linecount++;

                    if (line.Contains("WORD_CATEGORY:"))
                    {
                        category = line.Remove(0, 14);
                    }

                    if (line == "END FDICT")
                    {
                        parsingDefinitions = false;
                    }

                    if (parsingDefinitions)
                    {
                        var currentLine = line.TrimEnd(' ').TrimStart(' ');

                        var elements = currentLine.Split('|').ToList();
                        switch (category)
                        {
                            case "NOUN":
                            {
                                var tempWord = new Word();
                                tempWord.Category = category;
                                //VALUE OF THE WORD
                                tempWord.Value = elements[0];

                                //IF A NOUN, THE PLURALIZATION
                                if (elements[1].Contains("plur:"))
                                {
                                    tempWord.Pluralization = elements[1].Remove(0, 5);
                                }

                                var tags = elements.LastOrDefault();
                                if (tags != null) tags.Split(' ').ToList().ForEach(tag => tempWord.Tags.Add(tag));
                                ret.Add(tempWord);
                                break;
                            }
                            case "VERB":
                            {
                                var tempWord = new Verb();
                                tempWord.Category = category;
                                //VALUE OF THE WORD
                                tempWord.Value = elements[0];

                                //IF A VERB, THE CONJUGATIONS
                                if (elements[1].Contains("verb"))
                                {
                                    var conjugations = elements[1].Split(' ');
                                    if (conjugations.Count() == 3)
                                    {
                                        tempWord.Present = conjugations[0].Split(':')[1];
                                        tempWord.Preterite = conjugations[1].Split(':')[1];
                                        tempWord.PresentThirdPerson = conjugations[2].Split(':')[1];
                                    }
                                    else
                                    {
                                        //throw error
                                    }
                                }

                                var tags = elements.LastOrDefault();
                                if (tags != null) tags.Split(' ').ToList().ForEach(tag => tempWord.Tags.Add(tag));
                                ret.Add(tempWord);
                                break;
                            }
                            default:
                            {
                                var tempWord = new Word();
                                tempWord.Category = category;
                                //VALUE OF THE WORD
                                tempWord.Value = elements[0];
                                var tags = elements.LastOrDefault();
                                if (tags != null) tags.Split(' ').ToList().ForEach(tag => tempWord.Tags.Add(tag));
                                ret.Add(tempWord);
                                break;
                            }
                        }
                    }

                    if (line == "BEGIN FDICT")
                    {
                        parsingDefinitions = true;
                    }
                }

                file.Close();
            });
            return ret;
        }

        private static List<Word> GetAuxiliaryVerbs()
        {
            var ret = new List<Word>();
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "am", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "is", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "are", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "were", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "having", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "been", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "does", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "did", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "do", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "has", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "had", Tags = new List<string> { "article", "adjective", "noun" } });
            ret.Add(new Word { Category = "AUXILIARYVERB", Value = "have", Tags = new List<string> { "article", "adjective", "noun" } });
            return ret;
        }

        private static List<Word> GetArticles()
        {
            var ret = new List<Word>();
            ret.Add(new Word {Category = "ARTICLE", Value = "a", Tags = new List<string> {"consonantword", "any"}});
            ret.Add(new Word {Category = "ARTICLE", Value = "an", Tags = new List<string> {"vowelword", "any"}});
            ret.Add(new Word {Category = "ARTICLE", Value = "the", Tags = new List<string> {"adverb", "noun", "adjective"}});
            return ret;
        }
    }
}