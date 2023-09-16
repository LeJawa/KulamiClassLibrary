using System.Collections.Generic;

namespace KulamiLibrary
{
    public class VirtualBoard
    {
        private Board _board;

        private PlayerNumber _currentPlayer;

        private List<Position> _movesMade = new();

        private Position _player1LastMove = null!;
        private Position _player2LastMove = null!;

        public VirtualBoard(Board board, PlayerNumber currentPlayer)
        {
            _board = board;
            _currentPlayer = currentPlayer;

            foreach (var socket in board.GetAllSockets())
            {
                if (socket.State == SocketState.PLAYER1_LAST)
                    _player1LastMove = socket.Position;
                else if (socket.State == SocketState.PLAYER2_LAST)
                    _player2LastMove = socket.Position;
            }
        }

        public bool PlaceMarbleAtPosition(Position position)
        {
            if (_currentPlayer == PlayerNumber.ONE)
            {
                if (_board.SetP1MarbleAtPosition(position))
                {
                    _movesMade.Add(position);
                    SwitchPlayer();
                    return true;
                }
            }
            else
            {
                if (_board.SetP2MarbleAtPosition(position))
                {
                    _movesMade.Add(position);
                    SwitchPlayer();
                    return true;
                }
            }

            return false;
        }

        private void SwitchPlayer()
        {
            if (_currentPlayer == PlayerNumber.ONE)
                _currentPlayer = PlayerNumber.TWO;
            else
                _currentPlayer = PlayerNumber.ONE;
        }

        public void RevertLastMove()
        {
            if (_movesMade.Count == 0)
                return;

            var position = _movesMade[_movesMade.Count - 1];
            _movesMade.RemoveAt(_movesMade.Count - 1);
            var socket = _board.GetSocketAtPosition(position) ?? throw new NullReferenceException();
            socket.State = SocketState.EMPTY;


            Position lastMove = null!;
            if (_movesMade.Count > 1)
            {
                lastMove = _movesMade[-2];
            }
            else
            {
                if (_currentPlayer == PlayerNumber.ONE)
                    lastMove = _player2LastMove;
                else
                    lastMove = _player1LastMove;
            }

            if (lastMove is not null)
            {
                var lastSocket = _board.GetSocketAtPosition(lastMove) ?? throw new NullReferenceException();
                lastSocket.State = lastSocket.State == SocketState.PLAYER1 ? SocketState.PLAYER1_LAST : SocketState.PLAYER2_LAST;
            }

            SwitchPlayer();
        }

        private void RevertAllMoves()
        {
            while(_movesMade.Count > 0)
            {
                RevertLastMove();
            }
        }

        public int Evaluate()
        {
            var score = GameHelper.GetScore(_board);
            return score.Player1 - score.Player2;
        }

        public List<Socket> GetPossibleMoves()
        {
            return _board.GetPossibleMoves(_currentPlayer);
        }

        public bool IsGameOver => GetPossibleMoves().Count == 0;

        ~VirtualBoard()
        {
            // TODO: Ensure that the destructor is correctly run before reusing VirtualBoard
            RevertAllMoves();
        }

    }
}
