namespace KulamiLibrary
{
    public class QuantumTile
    {
        #region STATIC METHODS
        private static int _nextIDIndex = -1;
        private const string _possibleIDs = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static int AssignID()
        {
            _nextIDIndex++;
            return _possibleIDs[_nextIDIndex % _possibleIDs.Length];
        }

        private static List<Tile> GetTiles(Position[] rawPositions, List<Position> positions, int id) 
        {
            List<Tile> tileList = new List<Tile>();

            foreach (Position rawPosition in rawPositions) 
            {
                List<Socket> tileSockets = new List<Socket>();
                bool outOfBounds = false;

                foreach (Position position in positions)
                {
                    Position possiblePosition = position - rawPosition;

                    if (possiblePosition.IsNegative())
                    {
                        outOfBounds = true;
                        break;
                    }

                    Socket possibleSocket = new Socket(possiblePosition, id);
                    tileSockets.Add(possibleSocket);
                }

                if (!outOfBounds)
                {
                    tileList.Add(new Tile(tileSockets, id));
                }
            }

            return tileList;
        }
        #endregion

        private Position[] _rawPositions;
        private Position[] _rotatedRawPositions;

        private int _id;

        private bool HasRotation { get; set; } = false;

        private List<Position> _positions;
        private List<Position> _rotatedPositions;

        public QuantumTile(List<Position> rawPositions, List<Position> rotatedRawPositions = null!)
        {
            _id = AssignID();
            
            _rawPositions = rawPositions.ToArray();
            _positions = rawPositions;

            if (rotatedRawPositions != null)
            {
                _rotatedRawPositions = rotatedRawPositions.ToArray();
                _rotatedPositions = rotatedRawPositions;
                HasRotation = true;
            } else
            {
                HasRotation = false;
            }
        }

        public List<Tile> GetPossibleTilesAt(Position position)
        {
            MoveTo(position);

            var tileList = new List<Tile>();
            tileList.AddRange(GetTiles(_rawPositions, _positions, _id));

            if (HasRotation)
            {
                tileList.AddRange(GetTiles(_rotatedRawPositions, _rotatedPositions, _id));
            }

            return tileList;
        }

        private void MoveTo(Position position) 
        {
            _positions.Clear();
            foreach (var pos in _rawPositions)
            {
                _positions.Add(pos + position);
            }

            if (HasRotation)
            {
                _rotatedPositions.Clear();
                foreach (var pos in _rotatedRawPositions)
                {
                    _rotatedPositions.Add(pos + position);
                }
            }
        }

    }
}
