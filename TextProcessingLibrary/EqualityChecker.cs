using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessingLibrary
{
    public static class EqualityChecker
    {
        public static bool AreEqual(List<IWordFrequency> list1, List<IWordFrequency> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }
            else
            {
                for(int i = 0; i < list1.Count; i++)
                {
                    if (!list1[i].Equals(list2[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
