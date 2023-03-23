using Syntax;

namespace Semantics;

class JudgementKinding : Judgement {
    public TType t { get; set; }
    public Kind k { get; set; }

    public JudgementKinding(Context L, TType t, Kind k) : base(L) {
        this.t = t;
        this.k = k;
    }
}