using System;
using System.Collections;
using System.Collections.Generic;

namespace Syntax {

    public class App : Term {
        public Term t1 { get; set; }
        public Term t2 { get; set; }

        public App(Term t1, Term t2) {
            this.t1 = t1;
            this.t2 = t2;
        }

        public override bool Is(Object t) {
            if (t is Hole && ((Hole) t).Filled()) {
                return this.Is(((Hole) t).x!);
            }

            if (!(t is App)) {
                return false;
            }

            return t1.Is(((App) t).t1) && t2.Is(((App) t).t2);
        }

        public override string ToString() {
            return string.Format("{0} {1}", t1, t2);
        }

        public override HashSet<Var> Free()
        {
            HashSet<Var> ret = new HashSet<Var>(t1.Free());
            ret.UnionWith(t2.Free());
            return ret;
        }

        public override bool Filled()
        {
            return t1.Filled() && t2.Filled();
        }

        public override Term Subst(Term v, Term t)
        {
            if (this.Is(v)) {
                return t;
            } else {
                return new App(t1.Subst(v, t), t2.Subst(v, t));
            }
        }
    }

}