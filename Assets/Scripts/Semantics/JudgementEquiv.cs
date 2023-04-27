using Syntax;

namespace Semantics {


    public class JudgementEquiv : Judgement {
        public Term t { get; set; }
        public Term u { get; set; }

        public JudgementEquiv(Context E, Context L, Term t, Term u) : base(E, L) {
            this.t = t;
            this.u = u;
        }

        // TODO
    }

}