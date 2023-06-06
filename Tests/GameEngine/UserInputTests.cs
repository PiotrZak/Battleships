using GameEngine.Logic;

namespace Tests.GameEngine;

[TestFixture]
public class UserInputTests
{
    [TestCase("J10",  10,10)]
    [TestCase("A10", 1,10)]
    [TestCase("H5", 8,5)]
    [TestCase("D3", 4,3)]
    public void ParseUserInput_ValidInputs_ReturnsExpectedCoordinates(string input, int expectedX, int expectedY)
    {
        // Test implementation
        (int actualX, int actualY) = Parsers.ParseUserInput(input);
        Assert.Multiple(() =>
        {
            Assert.That(actualX, Is.EqualTo(expectedX));
            Assert.That(actualY, Is.EqualTo(expectedY));
        });
    }
}