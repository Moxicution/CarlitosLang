namespace Sandbox.Exceptions;

[Serializable]
public class ValueIsNotABooleanException : Exception
{
    public ValueIsNotABooleanException()
    {
    }

    public ValueIsNotABooleanException(string message)
        : base(message)
    {
    }

    public ValueIsNotABooleanException(string message, Exception inner)
        : base(message, inner)
    {
    }
}