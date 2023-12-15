using GameEngine.Logic;
using GameEngine.Models;

namespace Tests.GameEngine;

[TestFixture]
public class ShotLogicTests
{
    [Test]
    public void IsHitted_EnemyShip_ReturnsTrue()
    {
        // Arrange
        Map map = new Map();
        var coordinates = (3, 4);
        map.Coordinates[coordinates] = AllocationType.EnemyShip;

        // Act
        bool result = ShotLogic.IsHitted(map, coordinates);
        Assert.Multiple(() =>
        {

            // Assert
            Assert.That(result, Is.True);
            Assert.That(map.Coordinates[coordinates], Is.EqualTo(AllocationType.EnemyHitted));
        });
    }

    [Test]
    public void IsHitted_AllyHittedOrAllyShipHitted_ReturnsFalse()
    {
        // Arrange
        var map = new Map();
        var coordinates = (3, 4);

        map.Coordinates[coordinates] = AllocationType.AllyShip;

        // Act
        bool result = ShotLogic.IsHitted(map, coordinates);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result, Is.False);
            Assert.That(map.Coordinates[coordinates], Is.EqualTo(AllocationType.ShotMissed));
        });
    }

    [Test]
    public void IsHitted_ShotMissed_ReturnsFalse()
    {
        // Arrange
        Map map = new Map();
        (int, int) coordinates = (3, 4);
        map.Coordinates[coordinates] = AllocationType.Water;

        // Act
        bool result = ShotLogic.IsHitted(map, coordinates);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(map.Coordinates[coordinates], Is.EqualTo(AllocationType.ShotMissed));
        });
    }
}

[TestFixture]
public class HandleShotTests
{
    [Test]
    public void HandleHit_ShipHit_SinksShip()
    {
        // Arrange
        PlayerFleet playerFleet = new PlayerFleet();
        Map map = new Map
        {
            Coordinates =
            {
                [(3, 4)] = AllocationType.EnemyShip
            }
        };

        var ship = new Ship(
            new Dictionary<(int, int), bool>
            {
                [(3, 4)] = true,
                [(2, 4)] = true
            },
            1,
            "Destroyer"
        );
        
        playerFleet.Ships.Add(ship);

        int x = 3;
        int y = 4;

        // Act
        ShotLogic.HandleHit(playerFleet, map, x, y);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(ship.Position[(3, 4)], Is.False);
            Assert.That(ship.Position[(2, 4)], Is.True);
            Assert.That(ship.IsSunk, Is.False);
        });
    }

    [Test]
    public void HandleHit_ShipHit_ShipNotSunk()
    {
        // Arrange
        PlayerFleet playerFleet = new PlayerFleet();
        Map map = new Map
        {
            Coordinates =
            {
                [(3, 4)] = AllocationType.EnemyShip
            }
        };

        var ship = new Ship(
            new Dictionary<(int, int), bool>
            {
                [(3, 4)] = true
            },
            1,
            "Destroyer"
        );
        playerFleet.Ships.Add(ship);

        int x = 3;
        int y = 4;

        // Act
        ShotLogic.HandleHit(playerFleet, map, x, y);
        Assert.Multiple(() =>
        {

            // Assert
            Assert.That(ship.Position[(3, 4)], Is.False);
            Assert.That(ship.IsSunk, Is.True);
        });
    }

    [Test]
    public void HandleHit_ShipNotHit()
    {
        // Arrange
        PlayerFleet playerFleet = new PlayerFleet();
        Map map = new Map
        {
            Coordinates =
            {
                [(3, 4)] = AllocationType.Water
            }
        };

        var ship = new Ship(
            new Dictionary<(int, int), bool>
            {
                [(3, 3)] = true
            },
            1,
            "Destroyer"
        );
        playerFleet.Ships.Add(ship);

        int x = 3;
        int y = 4;

        // Act
        ShotLogic.HandleHit(playerFleet, map, x, y);

        // Assert
        Assert.That(ship.Position.ContainsKey((3, 4)), Is.False);
    }
}




