using Syntax;

namespace Semantics;

class JudgementKindCheck : Judgement {
    public TypeVar x { get; set; }
    public Kind t { get; set; }

    public JudgementKindCheck(Context L, TypeVar x, Kind t) : base(L) {
        this.x = x;
        this.t = t;
    }
}