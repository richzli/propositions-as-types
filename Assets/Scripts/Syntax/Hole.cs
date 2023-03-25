namespace Syntax;

class Hole : Term {
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

    public bool Fill(Term x) {
        if (this.x == null) {
            this.x = x;
            return true;
        } else {
            return false;
        }
    }
}