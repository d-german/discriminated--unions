using Discriminated_Unions_CI;

namespace DiscriminatedUnionsTests;

[TestFixture]
public class CalculatorTests
{
    private Calculator _calculator = null!;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator();
    }

    [Test]
    public void Divide_ValidInputs_ReturnsCorrectResult()
    {
        var result = _calculator.Divide(10, 2);

        Assert.That(result, Is.TypeOf<Something<double>>());
        var somethingResult = result as Something<double>;
        Assert.That(somethingResult?.Value, Is.EqualTo(5));
    }

    [Test]
    public void Divide_DivideByZero_ReturnsNothing()
    {
        var result = _calculator.Divide(10, 0);

        Assert.That(result, Is.TypeOf<Nothing<double>>());
    }
}