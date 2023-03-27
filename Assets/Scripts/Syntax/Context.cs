namespace Syntax;

public class Context {
    public Dictionary<Var, Term> c;

    public Context() {
        c = new Dictionary<Var, Term>();
    }

    private Context(Context prev) {
        this.c = new Dictionary<Var, Term>(prev.c);
    }

    public Term? Get(Var x) {
        if (c.ContainsKey(x)) {
            return c[x];
        }
        return null;
    }

    public bool Has(Var x) {
        return c.ContainsKey(x);
    }

    public Context With(Var x, Term t) {
        Context ret = new Context(this);
        if (c.ContainsKey(x)) {
            throw new ArgumentException();
        }
        ret.c[x] = t;
        return ret;
    }

    public override string ToString()
    {
        if (c.Count == 0) {
            return "∅";
        } else {
            string ret = String.Empty;
            foreach (Var v in c.Keys) {
                if (ret == String.Empty) {
                    ret = $"{v}: {c[v]}";
                } else {
                    ret += $"; {v}: {c[v]}";
                }
            }
            return ret;
        }
    }
}