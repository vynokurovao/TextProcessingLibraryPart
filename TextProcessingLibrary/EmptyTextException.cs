using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessingLibrary
{
    public class EmptyTextException : Exception
    {
        public EmptyTextException()
        {

        }

        public EmptyTextException(string messege)
            :base(messege)
        {

        }

    }
}
