namespace KulamiClassLibrary
{
    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public float DistanceFrom(Position other)
        {
            int x = X - other.X;
            int y = Y - other.Y;

            return (float) Math.Sqrt(x * x + y * y);
        }

        public bool IsNegative()
        {
            return X < 0 || Y < 0;
        }

        public static Position operator+ (Position a, Position b)
            => new Position(a.X + b.X, a.Y + b.Y);

        public static Position operator- (Position a, Position b)
            => new Position(a.X - b.X, a.Y - b.Y);
    }
}