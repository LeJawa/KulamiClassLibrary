namespace KulamiLibraryTests
{
    public class TileTests
    {
        public enum TileCharacteristic
        {
            EmptyTile3x1,
            Player1Owned3x1,
            Player1Owned3x1_bis,  // Different configuration for Player 1 owned tile
            Player1Owned3x1_ter,  // Ditto
            Player1Owned3x1_quad, // Ditto
            Player2Owned3x1,
            Player2Owned3x1_bis,
            Player2Owned3x1_ter,
            Player2Owned3x1_quad,
            SharedTile3x1,
            SharedTile3x1_bis,
            SharedTile3x1_ter,
        }

        private static Tile GetTile(TileCharacteristic tileCharacteristic)
        {
            List<Socket> sockets;
            switch (tileCharacteristic)
            {
                case TileCharacteristic.EmptyTile3x1:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.EMPTY },
                        new Socket(new Position(1, 0)) { State = SocketState.EMPTY },
                        new Socket(new Position(2, 0)) { State = SocketState.EMPTY },
                    };
                    return new Tile(sockets, 1);
                    
                case TileCharacteristic.Player1Owned3x1:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER1 },
                        new Socket(new Position(1, 0)) { State = SocketState.EMPTY },
                        new Socket(new Position(2, 0)) { State = SocketState.EMPTY },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.Player1Owned3x1_bis:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER1_LAST },
                        new Socket(new Position(1, 0)) { State = SocketState.EMPTY },
                        new Socket(new Position(2, 0)) { State = SocketState.EMPTY },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.Player1Owned3x1_ter:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER1 },
                        new Socket(new Position(1, 0)) { State = SocketState.PLAYER1 },
                        new Socket(new Position(2, 0)) { State = SocketState.PLAYER2 },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.Player1Owned3x1_quad:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER1 },
                        new Socket(new Position(1, 0)) { State = SocketState.PLAYER1 },
                        new Socket(new Position(2, 0)) { State = SocketState.PLAYER2_LAST },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.Player2Owned3x1:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER2 },
                        new Socket(new Position(1, 0)) { State = SocketState.EMPTY },
                        new Socket(new Position(2, 0)) { State = SocketState.EMPTY },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.Player2Owned3x1_bis:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER2_LAST },
                        new Socket(new Position(1, 0)) { State = SocketState.EMPTY },
                        new Socket(new Position(2, 0)) { State = SocketState.EMPTY },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.Player2Owned3x1_ter:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER1 },
                        new Socket(new Position(1, 0)) { State = SocketState.PLAYER2 },
                        new Socket(new Position(2, 0)) { State = SocketState.PLAYER2 },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.Player2Owned3x1_quad:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER1_LAST },
                        new Socket(new Position(1, 0)) { State = SocketState.PLAYER2 },
                        new Socket(new Position(2, 0)) { State = SocketState.PLAYER2 },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.SharedTile3x1:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER1_LAST },
                        new Socket(new Position(1, 0)) { State = SocketState.EMPTY },
                        new Socket(new Position(2, 0)) { State = SocketState.PLAYER2 },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.SharedTile3x1_bis:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER1 },
                        new Socket(new Position(1, 0)) { State = SocketState.PLAYER2 },
                        new Socket(new Position(2, 0)) { State = SocketState.EMPTY },
                    };
                    return new Tile(sockets, 1);
                case TileCharacteristic.SharedTile3x1_ter:
                    sockets = new List<Socket>
                    {
                        new Socket(new Position(0, 0)) { State = SocketState.PLAYER1 },
                        new Socket(new Position(1, 0)) { State = SocketState.PLAYER2_LAST },
                        new Socket(new Position(2, 0)) { State = SocketState.EMPTY },
                    };
                    return new Tile(sockets, 1);
                default:
                    throw new NotImplementedException();
            }
        }


        [Theory]
        [InlineData(3, 73)]   // Bit mask: 001 001 001
        [InlineData(4, 273)]  // Bit mask: 0000 0001 0001 0001
        [InlineData(5, 1057)] // Bit mask: 00000 00000 00001 00001 00001
        public void GetBitMask_ReturnsCorrectBitMask(int maskSize, int expectedBitMask)
        {
            // Arrange
            var tile = GetTile(TileCharacteristic.EmptyTile3x1);

            // Act
            int bitMask = tile.GetBitMask(maskSize);

            // Assert
            Assert.Equal(expectedBitMask, bitMask);
        }

        [Theory]
        [InlineData(TileCharacteristic.Player1Owned3x1, TileOwner.PLAYER1)]
        [InlineData(TileCharacteristic.Player1Owned3x1_bis, TileOwner.PLAYER1)]
        [InlineData(TileCharacteristic.Player1Owned3x1_ter, TileOwner.PLAYER1)]
        [InlineData(TileCharacteristic.Player1Owned3x1_quad, TileOwner.PLAYER1)]
        [InlineData(TileCharacteristic.Player2Owned3x1, TileOwner.PLAYER2)]
        [InlineData(TileCharacteristic.Player2Owned3x1_bis, TileOwner.PLAYER2)]
        [InlineData(TileCharacteristic.Player2Owned3x1_ter, TileOwner.PLAYER2)]
        [InlineData(TileCharacteristic.Player2Owned3x1_quad, TileOwner.PLAYER2)]
        [InlineData(TileCharacteristic.EmptyTile3x1, TileOwner.NONE)]
        [InlineData(TileCharacteristic.SharedTile3x1, TileOwner.NONE)]
        [InlineData(TileCharacteristic.SharedTile3x1_bis, TileOwner.NONE)]
        [InlineData(TileCharacteristic.SharedTile3x1_ter, TileOwner.NONE)]
        public void GetOwner_ReturnsCorrectOwner(TileCharacteristic tileCharacteristic, TileOwner expectedOwner)
        {
            // Arrange
            var tile = GetTile(tileCharacteristic);

            // Act
            var owner = tile.GetOwner();

            // Assert
            Assert.Equal(expectedOwner, owner);
        }

        [Fact]
        public void GetPoints_ReturnsCorrectPoints()
        {
            // Arrange
            var tile = GetTile(TileCharacteristic.EmptyTile3x1);

            // Act
            int points = tile.GetPoints();

            // Assert
            Assert.Equal(3, points);
        }
    }
}
