using Syntax;

namespace Semantics;

public class JudgementFalse : Judgement {
    public JudgementFalse(Context L) : base(L) {}

    public override string ToString()
    {
        return "FALSE";
    }
}