namespace Resolution.Parser.ChainParser
{
    public interface IParseable
    {
        IParseable Next(IParseable next);
        ParsedValue Recognise(string text);
    }
}
