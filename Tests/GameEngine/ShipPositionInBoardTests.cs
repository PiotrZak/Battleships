using GameEngine.Logic;

namespace Tests.GameEngine;

[TestFixture]
public class ShipPositionInBoardTests
{
    [TestCase(0, 0, true)]
    [TestCase(5, 5, true)]
    [TestCase(9, 9, true)]
    [TestCase(10, 5, true)]
    [TestCase(10, 10, true)]
    [TestCase(-1, 5, false)]
    [TestCase(5, -1, false)]
    [TestCase(11, 17, false)]
    public void IsValidPosition_WhenCalled_ReturnsCorrectValidity(int x, int y, bool expectedValidity)
    {
        var result = Validators.IsInBoard(x, y);
        Assert.That(result, Is.EqualTo(expectedValidity));
    }
}
