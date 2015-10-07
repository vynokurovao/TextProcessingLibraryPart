using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextProcessingLibrary;
using System.Collections.Generic;
using System.IO;

namespace TextProcessingLibraryTest
{
    [TestClass]
    public class WordFrequencyAnalyzerTest
    {
        IWordFrequencyAnalyzer analyzer = new WordFrequencyAnalyzer();

        //CalculateHighestFrequency

        [TestMethod]
        [ExpectedException(typeof(EmptyTextException), "There is no symbols in input text")]
        public void CalculateHighestFrequency_WithEmptyText()
        {
            analyzer.CalculateHighestFrequency("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The input text is invalid")]
        public void CalculateHighestFrequency_WithInvalidText_WithoutWords()
        {
            analyzer.CalculateHighestFrequency("&763$% ^&#@& (:>:");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The input text is invalid")]
        public void CalculateHighestFrequency_WithInvalidText_WithWords()
        {
            analyzer.CalculateHighestFrequency("Hello world! &^");
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyTextException), "There is no symbols in input text")]
        public void CalculateHighestFrequency_WithValidText_WithoutWords()
        {
            analyzer.CalculateHighestFrequency("  ; : ,  . ! ? ; ,.");
        }

        [TestMethod]
        public void CalculateHighestFrequency_WithValidText_WithAllDifferentWords()
        {
            int expected = 1;
            int actual = analyzer.CalculateHighestFrequency("one, two, three, four, five, six, seven, eight, nine, ten");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateHighestFrequency_WithValidText_WithAllSameWords()
        {
            int expected = 10;
            int actual = analyzer.CalculateHighestFrequency("one, one, one, one, one, one, one, one, one, one");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateHighestFrequency_WithValidText_WithTwoWords_WithSameFrequency()
        {
            int expected = 5;
            int actual = analyzer.CalculateHighestFrequency("one, two, one, two, one, two, one, two, one, two");
            Assert.AreEqual(expected, actual);
        }

        //CalculateFrequencyForWord

        [TestMethod]
        [ExpectedException(typeof(EmptyTextException), "There is no symbols in input text")]
        public void CalculateFrequencyForWord_WithEmptyText()
        {
            analyzer.CalculateFrequencyForWord("", "word");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The input text is invalid")]
        public void CalculateFrequencyForWord_WithInvalidText_WithoutWords()
        {
            analyzer.CalculateFrequencyForWord("&763$% ^&#@& (:>:", "word");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The input text is invalid")]
        public void CalculateFrequencyForWord_WithInvalidText_WithWords()
        {
            analyzer.CalculateFrequencyForWord("Hello world! &^", "word");
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyTextException), "There is no symbols in input text")]
        public void CalculateFrequencyForWord_WithValidText_WithoutWords()
        {
            analyzer.CalculateFrequencyForWord("  ; : ,  . ! ? ; ,.", "word");
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyTextException), "There is no symbols in input text")]
        public void CalculateFrequencyForWord_WithValidText_WithEmptyWord()
        {
            analyzer.CalculateFrequencyForWord("one, two, three, four, five, six, seven, eight, nine", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The input text is invalid")]
        public void CalculateFrequencyForWord_WithValidText_WithInvalidWord()
        {
            analyzer.CalculateFrequencyForWord("one, two, three, four, five, six, seven, eight, nine", "one3");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "There is no such word in database")]
        public void CalculateFrequencyForWord_WithValidText_WithValidWord_WhichIsNotInDictionary()
        {
            analyzer.CalculateFrequencyForWord("one, two, three, four, five, six, seven, eight, nine", "ten");
        }

        [TestMethod]
        public void CalculateFrequencyForWord_WithValidText_WithValidWord_WhichIsInDictionary()
        {
            int expected = 5;
            int actual = analyzer.CalculateFrequencyForWord("one, two, three, four, one, two, one, one, one", "one");
            Assert.AreEqual(expected, actual);
        }

        //CalculatingMostFrequentNWords

        [TestMethod]
        [ExpectedException(typeof(EmptyTextException), "There is no symbols in input text")]
        public void CalculateMostFrequentNWords_WithEmptyText()
        {
            analyzer.CalculateMostFrequentNWords("", 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The input text is invalid")]
        public void CalculateMostFrequentNWords_WithInvalidText_WithoutWords()
        {
            analyzer.CalculateMostFrequentNWords("&763$% ^&#@& (:>:", 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The input text is invalid")]
        public void CalculateMostFrequentNWords_WithInvalidText_WithWords()
        {
            analyzer.CalculateMostFrequentNWords("Hello world! &^", 5);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyTextException), "There is no symbols in input text")]
        public void CalculateMostFrequentNWords_WithValidText_WithoutWords()
        {
            analyzer.CalculateMostFrequentNWords("  ; : ,  . ! ? ; ,.", 5);
        }       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "N shoud be > 0")]
        public void CalculateMostFrequentNWords_WithValidText_WithNegativeNValue()
        {
            analyzer.CalculateMostFrequentNWords("one, two, three, two, three, two, three, one, one, four, five", -23);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "N can not be bigger then quantity of words in library")]
        public void CalculateMostFrequentNWords_WithValidText_WithNValueBiggerThenWordsQuantity()
        {
            analyzer.CalculateMostFrequentNWords("one, two, three, two, three, two, three, one, one, four, five", 10);
        }

        [TestMethod]
        public void CalculateMostFrequentNWords_WithValidText_WithValidNValue()
        {
            IList<IWordFrequency> expected = new List<IWordFrequency>()
            {
                new WordFrequency("one", 3),
                new WordFrequency("three", 3),
                new WordFrequency("two", 3),
                new WordFrequency("five", 1)
            };

            List<IWordFrequency> expectedToList = expected as List<IWordFrequency>;

            IList<IWordFrequency> actual = (analyzer.CalculateMostFrequentNWords("one, two, three, two, three, two, three, one, one, four, five", 4));
                
            List<IWordFrequency> actualToList = actual as List<IWordFrequency>;

            Assert.IsTrue(EqualityChecker.AreEqual(expectedToList, actualToList));
        }

    }
}
