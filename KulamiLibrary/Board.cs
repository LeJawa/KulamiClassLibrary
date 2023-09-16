namespace KulamiLibrary
{
    public class Board
    {
        private readonly int _maxBoardSize;

        private int _availableSize;
        private Position _temporaryBoardCenter;

        private List<Tile> _tiles;
        private List<Socket> _sockets;

        private const float RANDOM_WEIGHT_MULTIPLIER = 0.001f;

        private Random _rand = new Random();

        #region CONSTRUCTOR

        public Board(int maxBoardSize)
        {
            _maxBoardSize = maxBoardSize;
            _availableSize = 2 * maxBoardSize;

            _temporaryBoardCenter = new Position(_availableSize / 2, _availableSize / 2);

            _tiles = new List<Tile>();
            _sockets = new List<Socket>();
        }

        #endregion

        #region PUBLIC METHODS
        public static List<QuantumTile> GetStandardBagOfQTiles()
        {
            var bagOfQTiles = new List<QuantumTile>();

            for (int i = 0; i < 4; i++)
            {
                bagOfQTiles.Add(QuantumTileMaker.Get2x2());
                bagOfQTiles.Add(QuantumTileMaker.Get2x1());
                bagOfQTiles.Add(QuantumTileMaker.Get3x1());
                bagOfQTiles.Add(QuantumTileMaker.Get3x2());
            }
            bagOfQTiles.Add(QuantumTileMaker.Get2x2());

            return bagOfQTiles;
        }

        public List<Socket> GetAllSockets()
        {
            return _sockets;
        }

        public List<Tile> GetAllTiles()
        {
            return _tiles;
        }

        public List<Socket> GetPossibleMoves(PlayerNumber currentPlayer)
        {
            var allSockets = GetAllSockets();

            Socket player1LastMarble = null!, player2LastMarble = null!;

            foreach (var socket in allSockets)
            {
                if (socket.State == SocketState.PLAYER1_LAST)
                    player1LastMarble = socket;
                else if (socket.State == SocketState.PLAYER2_LAST)
                    player2LastMarble = socket;
            }

            if (player1LastMarble is null && player2LastMarble is null) // First turn
            {
                return allSockets;
            }


            List<Socket> possibleMoves = new();

            foreach (var socket in allSockets)
            {
                if (socket.State != SocketState.EMPTY)
                    continue;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (socket.Position == player1LastMarble.Position)
                    continue;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (socket.TileID == player1LastMarble.TileID)
                    continue;

                if (player2LastMarble is not null) // To handle the second turn
                {
                    if (socket.Position == player2LastMarble.Position)
                        continue;
                    if (socket.TileID == player2LastMarble.TileID)
                        continue;
                }

                Position lastMove;
                if (currentPlayer == PlayerNumber.ONE)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    lastMove = player2LastMarble.Position;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                else
                    lastMove = player1LastMarble.Position;

                if (socket.Position.X == lastMove.X || socket.Position.Y == lastMove.Y)
                {
                    possibleMoves.Add(socket);
                }
            }

            return possibleMoves;
        }

        public Socket? GetSocketAtPosition(Position position)
        {
            return GetSocketAt(position.X, position.Y);
        }

        public bool SetSocketState(Socket socket, SocketState state)
        {
            if (socket == null) return false;
            if (socket.State != SocketState.EMPTY) return false;

            socket.State = state;
            return true;
        }

        public bool SetP1MarbleAtPosition(Position position)
        {
            var socket = GetSocketAtPosition(position);

            if (socket == null) return false;

            return SetP1MarbleAtSocket(socket);
        }

        public bool SetP1MarbleAtSocket(Socket socket)
        {
            foreach (var boardSocket in _sockets)
            {
                if (boardSocket.State == SocketState.PLAYER1_LAST)
                {
                    boardSocket.State = SocketState.PLAYER1;
                    break;
                }
            }

            return SetSocketState(socket, SocketState.PLAYER1_LAST);
        }

        public bool SetP2MarbleAtPosition(Position position)
        {
            var socket = GetSocketAtPosition(position);

            if (socket == null) return false;

            return SetP2MarbleAtSocket(socket);
        }

        public bool SetP2MarbleAtSocket(Socket socket)
        {
            foreach (var boardSocket in _sockets)
            {
                if (boardSocket.State == SocketState.PLAYER2_LAST)
                {
                    boardSocket.State = SocketState.PLAYER2;
                    break;
                }
            }

            return SetSocketState(socket, SocketState.PLAYER2_LAST);
        }


        #endregion

        #region PRIVATE METHODS

        private float CalculateWeight(Position position)
        {
            var randomFloatBetweenMinusOneAndOne = () => { return (float)_rand.NextDouble() * 2 - 1; };

            var jitteredPosition = new FloatPosition(
                position.X + randomFloatBetweenMinusOneAndOne() * RANDOM_WEIGHT_MULTIPLIER,
                position.Y + randomFloatBetweenMinusOneAndOne() * RANDOM_WEIGHT_MULTIPLIER
            );

            return jitteredPosition.DistanceFrom(_temporaryBoardCenter);
        }

        private bool CanPlaceTile(Tile tile)
        {
            if (TileCollidesWithBoard(tile)) return false;

            if (PlacingTileExceedsMaxSize(tile)) return false;

            return true;
        }

        private bool TileCollidesWithBoard(Tile tile)
        {
            var boardMask = GetBoardBitMask();
            var tileMask = tile.GetBitMask(_availableSize);

            return (boardMask | tileMask) - tileMask != boardMask;
        }

        private bool PlacingTileExceedsMaxSize(Tile candidateTile)
        {
            int minX = _availableSize;
            int maxX = 0;
            int minY = _availableSize;
            int maxY = 0;

            foreach (var socket in candidateTile.Sockets)
            {
                minX = Math.Min(minX, socket.Position.X);
                maxX = Math.Max(maxX, socket.Position.X);
                minY = Math.Min(minY, socket.Position.Y);
                maxY = Math.Max(maxY, socket.Position.Y);
            }

            foreach (var placedTile in _tiles)
            {
                foreach (var socket in placedTile.Sockets)
                {
                    minX = Math.Min(minX, socket.Position.X);
                    maxX = Math.Max(maxX, socket.Position.X);
                    minY = Math.Min(minY, socket.Position.Y);
                    maxY = Math.Max(maxY, socket.Position.Y);
                }
            }

            return maxX - minX >= _maxBoardSize || maxY - minY >= _maxBoardSize;
        }

        private void FitToMaxBoardSize()
        {
            int minX = _availableSize;
            int minY = _availableSize;

            foreach (var socket in _sockets)
            {
                minX = Math.Min(minX, socket.Position.X);
                minY = Math.Min(minY, socket.Position.Y);
            }

            foreach (var socket in _sockets)
            {
                socket.Position.X -= minX;
                socket.Position.Y -= minY;
            }
            _availableSize = _maxBoardSize;
        }

        private int GetBoardBitMask()
        {
            int mask = 0;

            foreach (var tile in _tiles)
            {
                foreach (var socket in tile.Sockets)
                {
                    var bit_position =
                        socket.Position.X * _availableSize + socket.Position.Y;
                    mask |= 1 << bit_position;
                }
            }
            return mask;
        }

        private Socket? GetSocketAt(int x, int y)
        {
            foreach (var tile in _tiles)
            {
                foreach (var socket in tile.Sockets)
                {
                    if (socket.Position.X == x && socket.Position.Y == y)
                        return socket;
                }
            }
            return null;
        }

        private List<Position> GetSortedPositions()
        {
            var unsortedPositions = new List<Position>();
            for (int x = 0; x < _availableSize; x++)
            {
                for (int y = 0; y < _availableSize; y++)
                {
                    unsortedPositions.Add(new Position(x, y));
                }
            }

            var sortedPositions = unsortedPositions.OrderBy(CalculateWeight).ToList();

            return sortedPositions;
        }

        private int GetTileIdAt(int x, int y)
        {
            if (x < 0 || x >= _availableSize || y < 0 || y >= _availableSize)
                return -1; // Out of bounds

            foreach (var tile in _tiles)
            {
                foreach (var socket in tile.Sockets)
                {
                    if (socket.Position.X == x && socket.Position.Y == y)
                        return tile.ID;
                }
            }

            return -1; // Not tile at position
        }

        private void InitializeListOfSockets()
        {
            _sockets.Clear();
            foreach (var tile in _tiles)
            {
                foreach (var socket in tile.Sockets)
                {
                    _sockets.Add(socket);
                }
            }
        }

        private void InitializeStandardBoard()
        {
            var bagOfQTiles = GetStandardBagOfQTiles();

            PlaceAllQTiles(bagOfQTiles);

            FitToMaxBoardSize();
        }

        private bool PlaceQTile(QuantumTile qtile, Position position)
        {
            var possibleTiles = qtile.GetPossibleTilesAt(position);

            possibleTiles = possibleTiles.OrderBy((Tile tile) => { return _rand.NextDouble(); }).ToList();

            foreach (var tile in possibleTiles)
            {
                if (CanPlaceTile(tile))
                {
                    _tiles.Add(tile);
                    return true;
                }
            }

            return false;
        }

        private bool PlaceAllQTiles(List<QuantumTile> qTiles)
        {
            var positions = GetSortedPositions();

            qTiles = qTiles.OrderBy((QuantumTile qt) => { return _rand.NextDouble(); }).ToList();

            foreach (var position in positions)
            {
                foreach (var qtile in qTiles)
                {
                    if (PlaceQTile(qtile, position))
                    {
                        qTiles.Remove(qtile);
                        break;
                    }
                }

                if (qTiles.Count == 0)
                {
                    InitializeListOfSockets();
                    return true;
                }
            }

            return false;
        }

        #endregion
    }

}
