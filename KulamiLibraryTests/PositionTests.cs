namespace KulamiLibraryTests
{
    public class PositionTests
    {
        [Fact]
        public void Access_X_ReturnsCorrectValue()
        {
            // Arrange
            var expectedX = 3;
            var position = new Position(expectedX, 4);

            // Act
            var xValue = position.X;

            // Assert
            Assert.Equal(expectedX, xValue);
        }

        [Fact]
        public void Access_Y_ReturnsCorrectValue()
        {
            // Arrange
            var expectedY = 4;
            var position = new Position(3, expectedY);

            // Act
            var yValue = position.Y;

            // Assert
            Assert.Equal(expectedY, yValue);
        }

        [Fact]
        public void DistanceFrom_ReturnsCorrectDistance()
        {
            // Arrange
            var position1 = new Position(2, -1);
            var position2 = new Position(5, 3);
            var expectedDistance = 5.0f;

            // Act
            var distance = position1.DistanceFrom(position2);

            // Assert
            Assert.Equal(expectedDistance, distance, precision: 2);
        }

        [Theory]
        [InlineData(1, 2, false)]
        [InlineData(0, 0, false)]
        [InlineData(-1, -2, true)]
        [InlineData(1, -2, true)]
        [InlineData(-1, 2, true)]
        public void IsNegative_ReturnsCorrectValue(int x, int y, bool expectedIsNegative)
        {
            // Arrange
            var position = new Position(x, y);

            // Act
            var isNegative = position.IsNegative();

            // Assert
            Assert.Equal(expectedIsNegative, isNegative);
        }

        [Fact]
        public void OperatorPlus_AddsPositionsCorrectly()
        {
            // Arrange
            var position1 = new Position(1, 2);
            var position2 = new Position(3, 4);
            var expectedPosition = new Position(4, 6);

            // Act
            var result = position1 + position2;

            // Assert
            Assert.Equal(expectedPosition.X, result.X);
            Assert.Equal(expectedPosition.Y, result.Y);
        }

        [Fact]
        public void OperatorMinus_SubtractsPositionsCorrectly()
        {
            // Arrange
            var position1 = new Position(3, 4);
            var position2 = new Position(1, 2);
            var expectedPosition = new Position(2, 2);

            // Act
            var result = position1 - position2;

            // Assert
            Assert.Equal(expectedPosition.X, result.X);
            Assert.Equal(expectedPosition.Y, result.Y);
        }

        [Fact]
        public void OperatorEqual_ReturnsTrueForEqualPositions()
        {
            // Arrange
            var position1 = new Position(3, 4);
            var position2 = new Position(3, 4);

            // Act
            var isEqual = position1 == position2;

            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void OperatorNotEqual_ReturnsTrueForDifferentPositions()
        {
            // Arrange
            var position1 = new Position(3, 4);
            var position2 = new Position(1, 2);

            // Act
            var isNotEqual = position1 != position2;

            // Assert
            Assert.True(isNotEqual);
        }

        [Fact]
        public void Equals_ReturnsTrueForEqualPositions()
        {
            // Arrange
            var position1 = new Position(3, 4);
            var position2 = new Position(3, 4);

            // Act
            var isEqual = position1.Equals(position2);

            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void GetHashCode_ReturnsSameValueForEqualPositions()
        {
            // Arrange
            var position1 = new Position(3, 4);
            var position2 = new Position(3, 4);

            // Act
            var hash1 = position1.GetHashCode();
            var hash2 = position2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }
    }
}