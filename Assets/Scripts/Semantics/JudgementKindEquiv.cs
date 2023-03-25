using Syntax;

namespace Semantics;

class JudgementKindEquiv : Judgement {
    public Kind k1 { get; set; }
    public Kind k2 { get; set; }

    public JudgementKindEquiv(Context L, Kind k1, Kind k2) : base(L) {
        this.k1 = k1;
        this.k2 = k2;
    }
}