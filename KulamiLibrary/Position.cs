namespace KulamiLibrary
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position() { }

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

        public static Position operator+ (Position pos1, Position pos2)
            => new Position(pos1.X + pos2.X, pos1.Y + pos2.Y);

        public static Position operator- (Position pos1, Position pos2)
            => new Position(pos1.X - pos2.X, pos1.Y - pos2.Y);

        public static bool operator ==(Position pos1, Position pos2)
        {
            if (ReferenceEquals(pos1, pos2))
                return true;

            if (pos1 is null || pos2 is null)
                return false;

            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }

        public static bool operator !=(Position pos1, Position pos2)
        {
            return !(pos1 == pos2);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj == null || GetType() != obj.GetType())
                return false;

            Position other = (Position)obj;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return (X.GetHashCode() * 397) ^ Y.GetHashCode();
        }
    }

    public class FloatPosition
    {
        public float X;
        public float Y;

        public FloatPosition(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float DistanceFrom(Position other)
        {
            float x = X - other.X;
            float y = Y - other.Y;

            return (float)Math.Sqrt(x * x + y * y);
        }
    }


}