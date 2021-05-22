using System;

namespace Resolution.Parser.Exceptions
{
    internal class ParsingException : Exception
    {
        public ParsingException(string message) 
            : base("Wrong Syntax: parsing is unavailable " + message)
        {
        }
    }
}
