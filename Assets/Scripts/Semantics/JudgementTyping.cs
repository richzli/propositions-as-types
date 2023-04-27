using System;
using System.Collections;
using System.Collections.Generic;

using Syntax;

namespace Semantics {

    public class JudgementTyping : Judgement {
        public Term x { get; set; }
        public Term t { get; set; }

        public JudgementTyping(Context E, Context L, Term x, Term t) : base(E, L) {
            this.x = x;
            this.t = t;
        }
        public List<Judgement>? Var()
        {
            Term x2 = x.Get();
            Term t2 = t.Get();

            if (!(x2 is Var)) {
                return null;
            }

            Var v = (Var) x2;

            return new List<Judgement>() {
                new JudgementChecking(
                    E, L, v, t
                )
            };
        }

        public List<Judgement>? Abs(Sort s)
        {
            Term x2 = x.Get();
            Term t2 = t.Get();

            if (!(x2 is Abs && t2 is Pi)) {
                return null;
            }

            Abs a = (Abs) x2;
            Pi p = (Pi) t2;

            return new List<Judgement>() {
                new JudgementTyping(
                    E, L, p, s
                ),
                new JudgementTyping(
                    E, L.With(a.x, a.t), a.body, p.body
                )
            };
        }

        public List<Judgement>? App(Var v, Term u)
        {
            Term x2 = x.Get();
            Term t2 = t.Get();

            if (!(x2 is App)) {
                return null;
            }

            App a = (App) x2;

            // TODO: implement substitution
            return new List<Judgement>() {
                new JudgementTyping(
                    E, L, a.t1, new Pi(v, u, t2.Subst(a.t2, v))
                ),
                new JudgementTyping(
                    E, L, a.t2, u
                )
            };
        }

        public List<Judgement>? App(Var v, Term u, Term b)
        {
            Term x2 = x.Get();
            Term t2 = t.Get();

            if (!(x2 is App)) {
                return null;
            }

            App a = (App) x2;

            // TODO: implement substitution
            return new List<Judgement>() {
                new JudgementTyping(
                    E, L, a.t1, new Pi(v, u, b)
                ),
                new JudgementTyping(
                    E, L, a.t2, u
                )
            };
        }

        // TODO: implement let definitions? probably necessary for recursion

        public List<Judgement>? Axiom()
        {
            if (!(x is Sort && t is Sort)) {
                return null;
            }

            Sort x2 = (Sort) x;
            Sort t2 = (Sort) t;

            if ((x2.Is(Sort.SET) || x2.Is(Sort.PROP)) && t2.Is(Sort.TYPE(1))) {
                return new List<Judgement>();
            }

            if (x2.IsType() && t2.IsType() && x2.Level() + 1 == t2.Level()) {
                return new List<Judgement>();
            }

            return null;
        }    

        public List<Judgement>? Prod(Sort s1, Sort s2, Sort s3) {
            Term x2 = x.Get();
            Term t2 = t.Get();

            if (!(x2 is Pi && t2.Is(s3))) {
                return null;
            }

            Pi p = (Pi) x2;

            if (s2.Is(s3)) {
                if (s2.Is(Sort.SET) && s1.IsType()) {
                    return null;
                }
                if (s2.IsType() && s1.IsType() && s1.Level() > s2.Level()) {
                    return null;
                }
            } else {
                if (!(s1.IsType() && s2.IsType() && s3.IsType() && s3.Level() == Math.Max(s1.Level(), s2.Level()))) {
                    return null;
                }
            }

            return new List<Judgement>() {
                new JudgementTyping(
                    E, L, p.t, s1
                ),
                new JudgementTyping(
                    E, L.With(p.x, p.t), p.body, s2
                )
            };
        }

        public List<Judgement>? Conv(Term u, Sort s) {
            return new List<Judgement> {
                new JudgementTyping(
                    E, L, x, u
                ),
                new JudgementTyping(
                    E, L, u, s
                ),
                new JudgementEquiv(
                    E, L, t, u
                )
            };
        }

        public override List<Judgement>? Apply(string s, Object[] args) {
            switch (s) {
                case "Var":
                    return Var();
                case "Abs":
                    return Abs((Sort) args[0]);
                case "App":
                    return App((Var) args[0], (Term) args[1], (Term) args[2]);
                case "Axiom":
                    return Axiom();
                case "ProdPropProp":
                    return Prod(Sort.PROP, Sort.PROP, Sort.PROP);
                case "ProdSetProp":
                    return Prod(Sort.SET, Sort.PROP, Sort.PROP);
                case "ProdPropSet":
                    return Prod(Sort.PROP, Sort.SET, Sort.SET);
                case "ProdSetSet":
                    return Prod(Sort.SET, Sort.SET, Sort.SET);
                case "ProdTypeProp":
                    return Prod(Sort.TYPE((int) args[0]), Sort.PROP, Sort.PROP);
                case "ProdTypeType":
                    return Prod(Sort.TYPE((int) args[0]), Sort.TYPE((int) args[1]), Sort.TYPE((int) args[2]));
                case "Conv":
                    return Conv((Term) args[0], (Sort) args[1]);
                default:
                    return null;
            }
        }

        public override bool Filled()
        {
            return x.Filled() && t.Filled();
        }

        public override string GetTerm()
        {
            return x.ToString();
        }

        public override string GetGoal()
        {
            return t.ToString();
        }

        public override string ToString()
        {
            return $"[{E}][{L}] ⊢ {x} : {t}";
        }
    }

}