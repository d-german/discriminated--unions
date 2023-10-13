namespace Discriminated_Unions_CI;

public class Calculator
{
    public static Maybe<double> Divide(double dividend, double divisor)
    {
        try
        {
            if (divisor == 0)
            {
                return new Nothing<double>();
            }
            var result = dividend / divisor;
            return new Something<double>(result);
        }
        catch (Exception e)
        {
            return new Error<double>(e);
        }
    }
}