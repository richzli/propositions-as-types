namespace Syntax;

public class Abs : Term {
    public Var x { get; set; }
    public Term t { get; set; }
    public Term body { get; set; }

    public Abs(Var x, Term t, Term body) {
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

    public override bool Filled()
    {
        return t.Filled() && body.Filled();
    }

    public override HashSet<Var> Free()
    {
        HashSet<Var> ret = new HashSet<Var>(t.Free());
        ret.UnionWith(body.Free());
        ret.Remove(x);
        return ret;
    }


    public override string ToString() {
        return $"(λ{x}:{t}. {body})";
    }
}