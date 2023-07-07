namespace Sandbox.Exceptions;

[Serializable]
public class ValuesCannotdNotBeComparedException : Exception
{
    public ValuesCannotdNotBeComparedException()
    {
    }

    public ValuesCannotdNotBeComparedException(string message)
        : base(message)
    {
    }

    public ValuesCannotdNotBeComparedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}