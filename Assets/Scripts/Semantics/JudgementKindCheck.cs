using Syntax;

namespace Semantics;

public class JudgementKindCheck : Judgement {
    public TypeVar x { get; set; }
    public Kind k { get; set; }

    public JudgementKindCheck(Context L, TypeVar x, Kind k) : base(L) {
        this.x = x;
        this.k = k;
    }

    public override List<Judgement>? Apply(string s, Object[] args) {
        Context G = L;
        while (G is not ContextEmpty) {
            if (G is ContextWithType) {
                // TODO: implement alpha equivalence
                if (((ContextWithType) G).x.Is(x) && ((ContextWithType) G).k.Is(k)) {
                    return new List<Judgement>();
                }
                G = ((ContextWithType) G).prev;
            } else if (G is ContextWithTerm) {
                G = ((ContextWithTerm) G).prev;
            }
        }
        return new List<Judgement>() { new JudgementFalse(L) };
    }

    public override string ToString()
    {
        return String.Format("{0}({1}) = {2}", L, x, k);
    }
}