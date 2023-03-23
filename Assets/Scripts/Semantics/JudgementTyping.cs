using Syntax;

namespace Semantics;

class JudgementTyping : Judgement {
    public Term x { get; set; }
    public TType t { get; set; }

    public JudgementTyping(Context L, Term x, TType t) : base(L) {
        this.x = x;
        this.t = t;
    }
}