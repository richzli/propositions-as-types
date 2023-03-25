namespace Syntax;

public class Hole : Term {
    public static int counter = 0;
    public static List<Hole> holes = new List<Hole>();

    public int label { get; set; }
    // if x is null, then the hole is unfilled
    public Term? x { get; set; }

    public Hole() {
        this.label = counter;
        ++counter;
        this.x = null;
        holes.Add(this);
    }

    public override bool Is(Object h)
    {
        if (x == null) {
            return false;
        }

        if (h is Hole) {
            if (((Hole) h).x == null) {
                return false;
            }
            return x.Is(((Hole) h).x!);
        } else {
            return x.Is(h);
        }
    }

    public bool Fill(Term x) {
        if (this.x == null) {
            this.x = x;
            return true;
        } else {
            return false;
        }
    }

    public override bool Filled()
    {
        return x != null;
    }

    public Term Get() {
        if (x == null) {
            throw new UnfilledHoleException(this);
        }

        return x;
    }

    public override string ToString()
    {
        return String.Format("[{0}]", label);
    }
}