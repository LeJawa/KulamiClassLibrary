namespace KulamiLibrary
{
    public static class BoardMaker
    {
        public static Board GetStandardBoard()
        {
            var board = new Board(10);
            board.InitializeStandardBoard();

            return board;
        }

        public static Board GetVerySmallBoard()
        {
            var board = new Board(6);

            var bagOfQTiles = new List<QuantumTile>()
            {
                QuantumTileMaker.Get2x2(),
                QuantumTileMaker.Get2x2(),
                QuantumTileMaker.Get2x2(),
                QuantumTileMaker.Get2x2()
            };

            board.InitializeBoardWithQTiles(bagOfQTiles);

            return board;
        }
    }
}
