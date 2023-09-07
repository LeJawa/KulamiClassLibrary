namespace KulamiClassLibrary
{
    public class Board
    {
        private readonly int _maxBoardSize;

        private int _temporarySize;
        private Position _temporaryBoardCenter;

        private List<Tile> _tiles;
        private List<Socket> _sockets;

        #region CONSTRUCTOR

        public Board(int maxBoardSize)
        {
            _maxBoardSize = maxBoardSize;
            _temporarySize = 2 * maxBoardSize;

            _temporaryBoardCenter = new Position(_temporarySize / 2, _temporarySize / 2); 

            _tiles = new List<Tile>();
            _sockets = new List<Socket>();
        }

        #endregion

        #region PUBLIC METHODS
        public List<Socket> GetAllSockets()
        {
            throw new NotImplementedException();
        }

        public List<Tile> GetAllTiles() 
        {
            throw new NotImplementedException();
        }

        public List<Socket> GetPossibleMoves(PlayerNumber currentPlayer)
        {
            throw new NotImplementedException();
        }

        public Socket? GetSocketAtPosition(Position position) 
        {
            throw new NotImplementedException(); 
        }

        public SocketState GetSocketState(Position position)
        {
            throw new NotImplementedException();
        }

        public bool SetP1MarbleAtPosition(Position position) { throw new NotImplementedException(); }
        public bool SetP1MarbleAtSocket(Socket socket) { throw new NotImplementedException(); }
        public bool SetP2MarbleAtPosition(Position position) { throw new NotImplementedException(); }
        public bool SetP2MarbleAtSocket(Socket socket) { throw new NotImplementedException(); }

        public bool SetSocketState(Socket socket, SocketState state) {  throw new NotImplementedException(); }

        #endregion
    }

}
