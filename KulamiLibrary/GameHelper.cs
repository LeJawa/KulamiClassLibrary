namespace KulamiLibrary
{
    public static class GameHelper
    {
        public struct Score
        {
            public int Player1 { get; private set; }
            public int Player2 { get; private set; }

            public Score(int player1, int player2)
            {
                Player1 = player1;
                Player2 = player2;
            }
        }
        public static Score GetScore(Board board)
        {
            int player1Score = 0, player2Score = 0;

            foreach (var tile in board.GetAllTiles())
            {
                var owner = tile.GetOwner();
                if (owner == TileOwner.PLAYER1)
                    player1Score += tile.GetPoints();
                else if (owner == TileOwner.PLAYER2)
                    player2Score += tile.GetPoints();
            }

            return new Score(player1Score, player2Score);
        }
    }
}
