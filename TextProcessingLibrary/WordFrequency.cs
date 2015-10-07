using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessingLibrary
{
    public class WordFrequency : IWordFrequency
    {
        private readonly string _word;
        private readonly int _frequency;

        public WordFrequency(string word, int frequency)
        {
            _word = word;
            _frequency = frequency;
        }

        public string Word
        {
            get { return _word; }
        }

        public int Frequency
        {
            get { return _frequency; }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            WordFrequency wf = obj as WordFrequency;
            if ((Object)wf == null)
            {
                return false;
            }

            return (Word == wf.Word) && (Frequency == wf.Frequency);
        }

        public override int GetHashCode()
        {
            return Word.GetHashCode() ^ Frequency.GetHashCode();
        }
    }
}
