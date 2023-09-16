using KulamiLibrary;

namespace KulamiLibrary
{
    public struct GameInfo
    {
        public Board Board { get; set; }
        public PlayerNumber CurrentPlayer { get; set; }
        public List<Socket> PossibleMoves { get; set; }
        public int Turn { get; set; }

    }
}
