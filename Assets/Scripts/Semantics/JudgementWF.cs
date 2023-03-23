using Syntax;

namespace Semantics;

class JudgementWF : Judgement {
    public Kind k { get; set; }

    public JudgementWF(Context L, Kind k) : base(L) {
        this.k = k;
    }
}