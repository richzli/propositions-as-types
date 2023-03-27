namespace Syntax;

public class Pi : Term {
    public Var x { get; set; }
    public Term t { get; set; }
    public Term body { get; set; }

    public Pi(Var x, Term t, Term body) {
        this.x = x;
        this.t = t;
        this.body = body;
    }

    public override bool Is(Object o) {
        if (o is Hole && ((Hole) o).Filled()) {
            return this.Is(((Hole) o).x!);
        }

        if (!(o is Pi)) {
            return false;
        }

        return x.Is(((Pi) o).x) && t.Is(((Pi) o).t) && body.Is(((Pi) o).body);
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
        if (!body.Free().Contains(x)) {
            return $"({t} → {body})";
        } else {
            return $"(Π{x}:{t}. {body})";
        }
    }
}