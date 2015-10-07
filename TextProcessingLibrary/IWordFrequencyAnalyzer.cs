using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessingLibrary
{
    public interface IWordFrequencyAnalyzer
    {
        int CalculateHighestFrequency(string text);

        int CalculateFrequencyForWord(string text, string word);

        IList<IWordFrequency> CalculateMostFrequentNWords(string text, int n);
    }
}
