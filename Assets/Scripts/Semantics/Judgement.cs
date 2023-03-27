using Syntax;

namespace Semantics;

public class Judgement {
    public Context E { get; set; }
    public Context L { get; set; }

    public Judgement(Context E, Context L) {
        this.E = E;
        this.L = L;
    }

    public Term? Get(Var v) {
        return L.Get(v) ?? E.Get(v);
    }

    public virtual List<Judgement>? Apply(string s, Object[] args) {
        return null;
    }
}