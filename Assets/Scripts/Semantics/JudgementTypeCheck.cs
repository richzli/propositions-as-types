using Syntax;

namespace Semantics;

public class JudgementTypeCheck : Judgement {
    public Var x { get; set; }
    public TType t { get; set; }

    public JudgementTypeCheck(Context L, Var x, TType t) : base(L) {
        this.x = x;
        this.t = t;
    }

    public override List<Judgement>? Apply(string s, Object[] args) {
        Context G = L;
        while (G is not ContextEmpty) {
            if (G is ContextWithTerm) {
                // TODO: implement alpha equivalence
                if (((ContextWithTerm) G).x.Is(x) && ((ContextWithTerm) G).t.Is(t)) {
                    return new List<Judgement>();
                }
                G = ((ContextWithTerm) G).prev;
            } else if (G is ContextWithType) {
                G = ((ContextWithType) G).prev;
            }
        }
        return new List<Judgement>() { new JudgementFalse(L) };
    }

    public override string ToString()
    {
        return String.Format("{0}({1}) = {2}", L, x, t);
    }
}