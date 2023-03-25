namespace Syntax;

public class Abs : Term {
    public Var x { get; set; }
    public TType t { get; set; }
    public Term body { get; set; }

    public Abs(Var x, TType t, Term body) {
        this.x = x;
        this.t = t;
        this.body = body;
    }

    public override bool Is(Object o) {
        if (o is Hole && ((Hole) o).Filled()) {
            return this.Is(((Hole) o).x!);
        }

        if (!(o is Abs)) {
            return false;
        }

        return x.Is(((Abs) o).x) && t.Is(((Abs) o).t) && body.Is(((Abs) o).body);
    }

    public override string ToString() {
        return string.Format("(λ{0}:{1}. {2})", x, t, body);
    }

    public override bool Filled()
    {
        return body.Filled();
    }
}