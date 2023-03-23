using Syntax;

namespace Semantics;

class JudgementTypeCheck : Judgement {
    public Var x { get; set; }
    public TType t { get; set; }

    public JudgementTypeCheck(Context L, Var x, TType t) : base(L) {
        this.x = x;
        this.t = t;
    }
}