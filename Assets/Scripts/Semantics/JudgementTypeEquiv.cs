using Syntax;

namespace Semantics;

class JudgementTypeEquiv : Judgement {
    public TType t1 { get; set; }
    public TType t2 { get; set; }
    public Kind k { get; set; }

    public JudgementTypeEquiv(Context L, TType t1, TType t2, Kind k) : base(L) {
        this.t1 = t1;
        this.t2 = t2;
        this.k = k;
    }
}