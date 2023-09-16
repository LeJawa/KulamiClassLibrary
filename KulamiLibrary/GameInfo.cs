using KulamiClassLibrary;

namespace KulamiLibrary
{
    public struct GameInfo
    {
        public Board Board { get; private set; }
        public PlayerNumber CurrentPlayer { get; private set; }
        public List<Socket> PossibleMoves { get; private set; }
        public int Turn { get; private set; }

    }
}
