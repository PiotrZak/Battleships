using GameEngine.Logic;
using GameEngine.Models;

namespace Tests.GameEngine;

public class PopulateShipTests
{
    [Test]
    public void PlaceShipOnMap_ValidPlacement_SetsCoordinatesToAllocateType()
    {
        // Arrange
        var map = new Map();
        var startCoordinates = (2, 3);
        var length = 4;
        var isHorizontal = true;
        var allocateType = AllocationType.EnemyShip;

        // Act
        PopulateShips.PlaceShipOnMap(map, startCoordinates, length, isHorizontal, allocateType);

        // Assert
        for (int i = 0; i < length; i++)
        {
            int coordinateX = isHorizontal ? startCoordinates.Item1 + i : startCoordinates.Item1;
            int coordinateY = isHorizontal ? startCoordinates.Item2 : startCoordinates.Item2 + i;
            var coordinates = (coordinateX, coordinateY);
            Assert.That(map.Coordinates[coordinates], Is.EqualTo(allocateType));
        }
    }
    
    [Test]
    public void IsPlaceForShip_ValidPlacement_ReturnsTrue()
    {
        // Arrange
        var map = new Map
        {
            Coordinates =
            {
                [(0, 0)] = AllocationType.Water,
                [(0, 1)] = AllocationType.Water,
                [(0, 2)] = AllocationType.Water,
                [(0, 3)] = AllocationType.Water
            }
        };

        var isHorizontal = false;
        var length = 4;
        var startX = 0;
        var startY = 0;

        // Act
        bool result = PopulateShips.IsPlaceForShip(map, isHorizontal, length, startX, startY);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsPlaceForShip_InvalidPlacement_ReturnsFalse()
    {
        // Arrange
        var map = new Map
        {
            Coordinates =
            {
                [(0, 0)] = AllocationType.Water,
                [(0, 1)] = AllocationType.EnemyShip,
                [(0, 2)] = AllocationType.Water,
                [(0, 3)] = AllocationType.Water
            }
        };

        var isHorizontal = true;
        var length = 4;
        var startX = 0;
        var startY = 0;

        // Act
        bool result = PopulateShips.IsPlaceForShip(map, isHorizontal, length, startX, startY);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsPlaceForShip_OutOfBoundsPlacement_ReturnsFalse()
    {
        // Arrange
        var map = new Map();

        var isHorizontal = true;
        var length = 4;
        var startX = 7;
        var startY = 7;

        // Act
        bool result = PopulateShips.IsPlaceForShip(map, isHorizontal, length, startX, startY);

        // Assert
        Assert.That(result, Is.False);
    }
}