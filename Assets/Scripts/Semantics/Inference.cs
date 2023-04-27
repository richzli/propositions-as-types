using System;
using System.Collections;
using System.Collections.Generic;
using Syntax;

namespace Semantics {

    public class Inference {
        // when premises is null, the inference has not been populated
        // when premises is empty, the goal is an axiom
        public List<Inference>? premises { get; set; }
        public Judgement conclusion { get; set; }

        public Inference(Judgement conclusion) {
            this.premises = null;
            this.conclusion = conclusion;
        }

        public Inference(List<Inference> premises, Judgement conclusion) {
            this.premises = premises;
            this.conclusion = conclusion;
        }

        public bool Valid() {
            if (premises == null) {
                // Console.WriteLine("null, {0}", this.conclusion);
                return false;
            }

            bool ok = true;
            foreach (Inference p in premises) {
                ok &= p.Valid();
            }

            /*
            if (!ok) {
                Console.WriteLine("false, {0}", this.conclusion);
            }
            */
            return ok;
        }

        public bool Apply(string rule, Object[] args) {
            try {
                List<Judgement>? p = conclusion.Apply(rule, args);
                if (p == null) {
                    return false;
                }

                premises = new List<Inference>();
                foreach (Judgement j in p) {
                    premises.Add(new Inference(j));
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }

}