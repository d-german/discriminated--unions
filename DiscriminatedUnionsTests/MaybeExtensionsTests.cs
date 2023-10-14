using Discriminated_Unions_CI;

namespace DiscriminatedUnionsTests;

[TestFixture]
public class MaybeExtensionsTests
{
    [Test]
    public void Bind_Something_ReturnsTransformedValue()
    {
        var something = new Something<int>(5);
        var result = something.BindVal(Increment);

        Assert.That(result, Is.TypeOf<Something<int>>());
        var somethingResult = result as Something<int>;
        Assert.That(somethingResult?.Value, Is.EqualTo(6));
    }

    [Test]
    public void MultipleBindOperations_IncrementValue()
    {
        var something = new Something<int>(5);
        var result = something
            .BindVal(Increment)
            .BindVal(Increment)
            .BindVal(Increment);

        Assert.That(result, Is.TypeOf<Something<int>>());
        var somethingResult = result as Something<int>;
        Assert.That(somethingResult?.Value, Is.EqualTo(8));
    }

    [Test]
    public void Bind_Nothing_ReturnsNothing()
    {
        var nothing = new Nothing<int>();
        var result = nothing.BindVal(Increment);

        Assert.That(result, Is.TypeOf<Nothing<int>>());
    }

    [Test]
    public void Bind_Error_ReturnsError()
    {
        var error = new Error<int>(new Exception("Original error"));
        var result = error.BindVal(Increment);

        Assert.That(result, Is.TypeOf<Error<int>>());
        var errorResult = result as Error<int>;
        Assert.That(errorResult?.CapturedError.Message, Is.EqualTo("Original error"));
    }

    private static int Increment(int value)
    {
        return value + 1;
    }

    [Test]
    public void BindNullable_SomethingWithNonNullValue_ReturnsTransformedValue()
    {
        var something = new Something<int?>(5);
        var result = something.BindNullable(Increment);

        Assert.That(result, Is.TypeOf<Something<int>>());
        var somethingResult = result as Something<int>;
        Assert.That(somethingResult?.Value, Is.EqualTo(6));
    }

    [Test]
    public void BindNullable_SomethingWithNullValue_ReturnsNothing()
    {
        var something = new Something<int?>(null);
        var result = something.BindNullable(Increment);

        Assert.That(result, Is.TypeOf<Nothing<int>>());
    }

    [Test]
    public void BindNullable_Nothing_ReturnsNothing()
    {
        var nothing = new Nothing<int?>();
        var result = nothing.BindNullable(Increment);

        Assert.That(result, Is.TypeOf<Nothing<int>>());
    }

    [Test]
    public void BindRef_SomethingWithNonNullValue_ReturnsTransformedValue()
    {
        var something = new Something<string>("hello");
        var result = something.BindRef(AppendWorld);

        Assert.That(result, Is.TypeOf<Something<string>>());
        var somethingResult = result as Something<string>;
        Assert.That(somethingResult?.Value, Is.EqualTo("hello world"));
    }

    [Test]
    public void BindRef_SomethingWithNullValue_ReturnsNothing()
    {
        var something = new Something<string>(null);
        var result = something.BindRef(AppendWorld);

        Assert.That(result, Is.TypeOf<Nothing<string>>());
    }

    [Test]
    public void BindRef_Nothing_ReturnsNothing()
    {
        var nothing = new Nothing<string>();
        var result = nothing.BindRef(AppendWorld);

        Assert.That(result, Is.TypeOf<Nothing<string>>());
    }

    private static string AppendWorld(string value)
    {
        return value + " world";
    }
}