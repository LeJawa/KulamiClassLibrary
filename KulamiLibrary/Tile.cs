namespace KulamiLibrary
{
    public class Tile
    {
        public int ID { get; private set; }
        public Socket[] Sockets { get; private set; }

        public Tile(List<Socket> sockets, int id)
        {
            ID = id;
            Sockets = sockets.ToArray();
        }

        public int GetBitMask(int maskSize)
        {
            int mask = 0;

            foreach (Socket socket in Sockets)
            {
                int bit_position = socket.Position.X * maskSize + socket.Position.Y;
                mask |= 1 << bit_position;
            }

            return mask;
        }

        public TileOwner GetOwner()
        {
            int score = 0;

            foreach (Socket socket in Sockets)
            {
                if (socket.State == SocketState.PLAYER1 || socket.State == SocketState.PLAYER1_LAST)
                    score++;
                if (socket.State == SocketState.PLAYER2 || socket.State == SocketState.PLAYER2_LAST)
                    score--;
            }

            if (score > 0)
                return TileOwner.PLAYER1;
            if (score < 0) 
                return TileOwner.PLAYER2;

            return TileOwner.NONE;
        }

        public int GetPoints()
        {
            return Sockets.Length;
        }

    }
}
