using Syntax;

namespace Semantics;

public class JudgementTermEquiv : Judgement {
    public Term x1 { get; set; }
    public Term x2 { get; set; }
    public TType t { get; set; }

    public JudgementTermEquiv(Context L, Term x1, Term x2, TType t) : base(L) {
        this.x1 = x1;
        this.x2 = x2;
        this.t = t;
    }

    public override string ToString()
    {
        return String.Format("{0} ⊢ {1} ≡ {2} : {3}", L, x1, x2, t);
    }
}