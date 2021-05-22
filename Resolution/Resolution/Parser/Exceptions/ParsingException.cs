using System;

namespace Resolution.Parser.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException(string message) 
            : base("Wrong Syntax: parsing is unavailable " + message)
        {
        }
    }
}
