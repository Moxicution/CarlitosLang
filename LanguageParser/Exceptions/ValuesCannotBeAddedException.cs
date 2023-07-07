namespace Sandbox.Exceptions;

[Serializable]
public class ValuesCannotBeAddedException : Exception
{
    public ValuesCannotBeAddedException()
    {
    }

    public ValuesCannotBeAddedException(string message)
        : base(message)
    {
    }

    public ValuesCannotBeAddedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}