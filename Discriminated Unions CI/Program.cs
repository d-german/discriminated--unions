namespace Discriminated_Unions_CI;

internal static class Program
{
    private static void Main()
    {
        var calculator = new Calculator();

        var result1 = calculator.Divide(10, 2);
        DisplayResult(result1);

        var result2 = calculator.Divide(10, 0);
        DisplayResult(result2);

        var result3 = calculator.Divide(15, 3);
        DisplayResult(result3);
    }

    private static void DisplayResult(Maybe<double> result)
    {
        var message = result switch
        {
            Nothing<double> _ => "Operation could not be performed.",
            Something<double> s => "The result is: " + s.Value,
            Error<double> e => "An error occurred: " + e.CapturedError.Message,
            _ => throw new InvalidOperationException("Unknown type.")
        };
        Console.WriteLine(message);
    }
}