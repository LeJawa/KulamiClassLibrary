namespace KulamiLibraryTests
{
    public class SocketTests
    {
        [Fact]
        public void Constructor_WithPosition_SetsPositionAndDefaults()
        {
            // Arrange
            var position = new Position(1, 2);

            // Act
            var socket = new Socket(position);

            // Assert
            Assert.Equal(position, socket.Position);
            Assert.Equal(SocketState.EMPTY, socket.State);
            Assert.Equal(-1, socket.TileID);
        }

        [Fact]
        public void Constructor_WithPositionAndTileID_SetsPositionAndTileID()
        {
            // Arrange
            var position = new Position(3, 4);
            var tileID = 42;

            // Act
            var socket = new Socket(position, tileID);

            // Assert
            Assert.Equal(position, socket.Position);
            Assert.Equal(SocketState.EMPTY, socket.State);
            Assert.Equal(tileID, socket.TileID);
        }

        [Fact]
        public void TileID_IsAccessible()
        {
            // Arrange
            var expectedTileID = 42;
            var socket = new Socket(new Position(1, 2), expectedTileID);

            // Act
            var actualTileID = socket.TileID;

            // Assert
            Assert.Equal(expectedTileID, actualTileID);
        }

        [Fact]
        public void Position_IsAccessible()
        {
            // Arrange
            var expectedPosition = new Position(3, 4);
            var socket = new Socket(new Position(3, 4));

            // Act
            var actualPosition = socket.Position;

            // Assert
            Assert.Equal(expectedPosition, actualPosition);
        }

        [Fact]
        public void State_IsAccessible()
        {
            // Arrange
            var socket = new Socket(new Position(1, 2));
            var expectedState = SocketState.PLAYER1;

            // Act
            socket.State = expectedState;
            var actualState = socket.State;

            // Assert
            Assert.Equal(expectedState, actualState);
        }

        [Fact]
        public void IsEmpty_WhenSocketIsEmpty_ReturnsTrue()
        {
            // Arrange
            var socket = new Socket(new Position(1, 2));
            socket.State = SocketState.EMPTY;

            // Act
            var isEmpty = socket.IsEmpty();

            // Assert
            Assert.True(isEmpty);
        }

        [Theory]
        [InlineData(SocketState.PLAYER1)]
        [InlineData(SocketState.PLAYER1_LAST)]
        [InlineData(SocketState.PLAYER2)]
        [InlineData(SocketState.PLAYER2_LAST)]
        public void IsEmpty_WhenSocketIsNotEmpty_ReturnsFalse(SocketState nonEmptyState)
        {
            // Arrange
            var socket = new Socket(new Position(1, 2), 42);
            socket.State = nonEmptyState;

            // Act
            var isEmpty = socket.IsEmpty();

            // Assert
            Assert.False(isEmpty);
        }

        
    }
}
