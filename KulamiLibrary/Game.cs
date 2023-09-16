namespace KulamiLibrary
{
    public class Game
    {
        private const int MARBLES_PER_PLAYER = 28;

        private Board _board;

        private IPlayer _player1;
        private IPlayer _player2;

        private int _turn = 0;
        private int _maxTurns = 2 * MARBLES_PER_PLAYER;

        private Socket _player1LastMarble = null!;
        private Socket _player2LastMarble = null!;

        private List<Socket> _possibleMoves = new List<Socket>();

        public Game(IPlayer player1, IPlayer player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public void InitializeStandardBoard()
        {
            _board = BoardMaker.GetStandardBoard();
            _possibleMoves = _board.GetPossibleMoves(PlayerNumber.ONE);
        }

        public void InitializeVerySmallBoard()
        {
            _board = BoardMaker.GetVerySmallBoard();
            _possibleMoves = _board.GetPossibleMoves(PlayerNumber.ONE);
        }

        private PlayerNumber GetCurrentPlayer()
        {
            if (_turn % 2 == 0)
            {
                return PlayerNumber.ONE;
            }
            return PlayerNumber.TWO;
        }

        private void PlayerTurn()
        {
            var currentPlayer = GetCurrentPlayer();
            var gameInfo = new GameInfo()
            {
                Board = _board,
                CurrentPlayer = currentPlayer,
                PossibleMoves = _possibleMoves,
                Turn = _turn
            };

            Position marblePosition;
            if (currentPlayer == PlayerNumber.ONE)
                marblePosition = _player1.GetNextMove(gameInfo);
            else
                marblePosition = _player2.GetNextMove(gameInfo);


            if (currentPlayer == PlayerNumber.ONE)
            {
                if (_board.SetP1MarbleAtPosition(marblePosition))
                {
                    _player1LastMarble = _board.GetSocketAtPosition(marblePosition) ?? throw new NullReferenceException();
                }
                else throw new Exception("Invalid Move");
            }
            else
            {
                if (_board.SetP2MarbleAtPosition(marblePosition))
                {
                    _player2LastMarble = _board.GetSocketAtPosition(marblePosition) ?? throw new NullReferenceException();
                }
                else throw new Exception("Invalid Move");
            }

            _turn += 1;

            _possibleMoves = _board.GetPossibleMoves(currentPlayer == PlayerNumber.TWO ? PlayerNumber.ONE : PlayerNumber.TWO);
        }


        public PlayerNumber? Play()
        {
            while (_turn < _maxTurns && _possibleMoves.Count > 0)
            {
                PlayerTurn();
            }

            var score = GameHelper.GetScore(_board);

            return score.Winner;
        }

    }
}
