namespace KulamiClassLibrary
{
    public class Socket
    {
        public Socket(Position position)
        {
            Position = position;
            State = SocketState.EMPTY;
            TileID = -1;
        }

        public Socket(Position position, int tileID)
        {
            Position = position;
            State = SocketState.EMPTY;
            TileID = tileID;
        }

        public int TileID { get; private set; }
        public Position Position { get; private set; }
        public SocketState State { get; set; }

        public bool IsEmpty()
        {
            return State == SocketState.EMPTY;
        }

        // TODO: remove
        //public void SetTileID(int tileID)
        //{
        //    TileID = tileID;
        //}

    }
}
