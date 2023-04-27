using Syntax;

namespace Semantics {

    public class JudgementFalse : Judgement {
        public JudgementFalse(Context E, Context L) : base(E, L) {}

        public override string ToString()
        {
            return "FALSE";
        }
    }

}