using Syntax;

namespace Semantics;

public class JudgementChecking : Judgement {
    public Var x { get; set; }
    public Term t { get; set; }

    public JudgementChecking(Context E, Context L, Var x, Term t) : base(E, L) {
        this.x = x;
        this.t = t;
    }

    public List<Judgement>? Check() {
        if (L.Has(x)) {
            // TODO: alpha equivalence
            if (L.Get(x)!.Is(t)) {
                return new List<Judgement>();
            } else {
                return null;
            }
        } else if (E.Has(x)) {
            if (E.Get(x)!.Is(t)) {
                return new List<Judgement>();
            }
        }
        return null;
    }

    public override List<Judgement>? Apply(string s, Object[] args) {
        switch (s) {
            case "Check":
                return Check();
            default:
                return null;
        }
    }

    public override string ToString()
    {
        return $"[{E}][{L}]({x}) = {t}";
    }
}