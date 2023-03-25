using Syntax;

namespace Semantics;

class JudgementTermEquiv : Judgement {
    public Term x1 { get; set; }
    public Term x2 { get; set; }
    public TType t { get; set; }

    public JudgementTermEquiv(Context L, Term x1, Term x2, TType t) : base(L) {
        this.x1 = x1;
        this.x2 = x2;
        this.t = t;
    }
}