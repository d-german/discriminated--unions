using Discriminated_Unions_CI;

namespace DiscriminatedUnionsTests;

[TestFixture]
public class MaybeExtensionsTests
{
    [Test]
    public void Bind_Something_ReturnsTransformedValue()
    {
        var something = new Something<int>(5);
        var result = something.Bind(Increment);

        Assert.That(result, Is.TypeOf<Something<int>>());
        var somethingResult = result as Something<int>;
        Assert.That(somethingResult?.Value, Is.EqualTo(6));
    }

    [Test]
    public void Bind_Nothing_ReturnsNothing()
    {
        var nothing = new Nothing<int>();
        var result = nothing.Bind(Increment);

        Assert.That(result, Is.TypeOf<Nothing<int>>());
    }

    [Test]
    public void Bind_Error_ReturnsError()
    {
        var error = new Error<int>(new Exception("Original error"));
        var result = error.Bind(Increment);

        Assert.That(result, Is.TypeOf<Error<int>>());
        var errorResult = result as Error<int>;
        Assert.That(errorResult?.CapturedError.Message, Is.EqualTo("Original error"));
    }

    private static int Increment(int value)
    {
        return value + 1;
    }
}