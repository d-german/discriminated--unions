namespace Discriminated_Unions_CI;

public abstract class Maybe<T>
{
}

public class Something<T> : Maybe<T>
{
    public Something(T value)
    {
        Value = value;
    }

    public T Value { get; }
}

public class Nothing<T> : Maybe<T>
{
}

public class Error<T> : Maybe<T>
{
    public Error(Exception e)
    {
        CapturedError = e;
    }

    public Exception CapturedError { get; }
}