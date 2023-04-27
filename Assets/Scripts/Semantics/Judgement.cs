using System;
using System.Collections;
using System.Collections.Generic;
using Syntax;

namespace Semantics {

    public class Judgement {
        public Context E { get; set; }
        public Context L { get; set; }

        public Judgement(Context E, Context L) {
            this.E = E;
            this.L = L;
        }

        public Term? Get(Var v) {
            return L.Get(v) ?? E.Get(v);
        }

        public virtual List<Judgement>? Apply(string s, Object[] args) {
            return null;
        }

        public virtual bool Filled() {
            return false;
        }

        public virtual string GetTerm() {
            return "";
        }

        public virtual string GetGoal() {
            return "";
        }
    }

}