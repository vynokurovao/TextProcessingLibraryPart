using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextProcessingLibrary
{
    public class WordFrequencyAnalyzer : IWordFrequencyAnalyzer
    {
        private const string Delimiters = ",:;?!. ";
        private const string Letters = "a-zA-Z";

        private static List<string> checkAndSplitText(string text)
        {
            if (text == "")
            {
                throw new EmptyTextException("There is no symbols in input text");
            }

            Match match = Regex.Match(text, @"^[" + Letters + Delimiters + "]*$");

            if (!match.Success)
            {
                throw new ArgumentException("The input text is invalid");
            }

            Match onlyDelimitersMatch = Regex.Match(text, @"^[" + Delimiters + "]+$");

            if (onlyDelimitersMatch.Success)
            {
                throw new EmptyTextException("There is no words in input text");
            }

            List<string> words = Regex.Split(text, @"[" + Delimiters + "]+").ToList();

            return words;
        }

        private static void checkWord(string word)
        {
            if (word == "")
            {
                throw new EmptyTextException("There is no symbols in input text");
            }

            Match match = Regex.Match(word, @"^[" + Letters + "]*$");

            if (!match.Success)
            {
                throw new ArgumentException("The input text is invalid");
            }
        }

        private static Dictionary<string, int> FindWordsFrequencies(List<string> words)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (dictionary.ContainsKey(word))
                {
                    dictionary[word]++;
                }
                else
                {
                    dictionary.Add(word, 1);
                }
            }

            return dictionary;
        }

        public int CalculateHighestFrequency(string text)
        {
            List<int> frequencies = FindWordsFrequencies(checkAndSplitText(text)).Values.ToList();

            return frequencies.Max(x => x);        
        }

        public int CalculateFrequencyForWord(string text, string word)
        {
            checkWord(word);

            Dictionary<string, int> dictionary = FindWordsFrequencies(checkAndSplitText(text));

            if (!dictionary.ContainsKey(word))
            {
                throw new KeyNotFoundException("There is no such word in database");
            }

            return dictionary[word];
        }

        public IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("N shoud be > 0");
            }

            IList<IWordFrequency> wordsFrequencies = new List<IWordFrequency>();

            Dictionary<string, int> dictionary = FindWordsFrequencies(checkAndSplitText(text));

            foreach (string word in dictionary.Keys.ToList())
            {
                wordsFrequencies.Add(new WordFrequency(word, dictionary[word]));
            }

            if (n > wordsFrequencies.Count)
            {
                throw new ArgumentException("N can not be bigger then quantity of words in library");
            }

            IList<IWordFrequency> mostFrequentNWords = (wordsFrequencies
                .OrderByDescending(i => i.Frequency).ThenBy(i => i.Word)
                .Take(n)).ToList();
            return mostFrequentNWords;
        }
    }
}
