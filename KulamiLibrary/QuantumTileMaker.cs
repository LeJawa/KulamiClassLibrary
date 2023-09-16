namespace KulamiLibrary
{
    public static class QuantumTileMaker
    {
        public static QuantumTile Get1x1()
        {
            var rawPositions = new List<Position> 
            {
                new Position(0, 0),
            };

            return new QuantumTile(rawPositions);
        }

        public static QuantumTile Get2x1() 
        {
            var rawPositions = new List<Position>
            {
                new Position(0, 0),
                new Position(1, 0),
            };

            var rotatedRawPositions = new List<Position>
            {
                new Position(0, 0),
                new Position(0, 1),
            };

            return new QuantumTile(rawPositions, rotatedRawPositions);
        }

        public static QuantumTile Get2x2()
        {
            var rawPositions = new List<Position>
            {
                new Position(0, 0),
                new Position(1, 0),
                new Position(0, 1),
                new Position(1, 1),
            };

            return new QuantumTile(rawPositions);
        }

        public static QuantumTile Get3x1()
        {
            var rawPositions = new List<Position>
            {
                new Position(0, 0),
                new Position(1, 0),
                new Position(2, 0),
            };

            var rotatedRawPositions = new List<Position>
            {
                new Position(0, 0),
                new Position(0, 1),
                new Position(0, 2),
            };

            return new QuantumTile(rawPositions, rotatedRawPositions);
        }

        public static QuantumTile Get3x2()
        {
            var rawPositions = new List<Position>
            {
                new Position(0, 0),
                new Position(1, 0),
                new Position(2, 0),
                new Position(0, 1),
                new Position(1, 1),
                new Position(2, 1),
            };

            var rotatedRawPositions = new List<Position>
            {
                new Position(0, 0),
                new Position(0, 1),
                new Position(0, 2),
                new Position(1, 0),
                new Position(1, 1),
                new Position(1, 2),
            };

            return new QuantumTile(rawPositions, rotatedRawPositions);
        }
    }
}
