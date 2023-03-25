using Syntax;

namespace Semantics;

public class JudgementKindEquiv : Judgement {
    public Kind k1 { get; set; }
    public Kind k2 { get; set; }

    public JudgementKindEquiv(Context L, Kind k1, Kind k2) : base(L) {
        this.k1 = k1;
        this.k2 = k2;
    }

    public override string ToString()
    {
        return String.Format("{0} ⊢ {1} ≡ {2}", L, k1, k2);
    }
}