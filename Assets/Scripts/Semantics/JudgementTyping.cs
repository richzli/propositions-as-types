using Syntax;

namespace Semantics;

public class JudgementTyping : Judgement {
    public Term x { get; set; }
    public TType t { get; set; }

    public JudgementTyping(Context L, Term x, TType t) : base(L) {
        this.x = x;
        this.t = t;
    }

    public List<Judgement> TVar() {
        Term x2;

        if (x is Hole) {
            x2 = ((Hole) x).Get();
        } else {
            x2 = x;
        }

        if (!(x2 is Var)) {
            throw new InvalidRuleApplicationException("TVar");
        }

        Var v = (Var) x2;

        List<Judgement> premises = new List<Judgement>();
        premises.Add(new JudgementTypeCheck(
            L, v, t
        ));
        premises.Add(new JudgementKinding(
            L, t, KindStar.STAR
        ));

        return premises;
    }

    public List<Judgement> TAbs() {
        Term x2;

        if (x is Hole) {
            x2 = ((Hole) x).Get();
        } else {
            x2 = x;
        }

        if (!(x2 is Abs && t is TypePi)) {
            throw new InvalidRuleApplicationException("TAbs");
        }

        Abs a = (Abs) x2;
        TypePi tp = (TypePi) t;

        // TODO: implement alpha equivalence/subst instead of this
        if (!(a.x.Is(tp.x) && a.t.Is(tp.t))) {
            throw new InvalidRuleApplicationException("TAbs (alpha)");
        }

        List<Judgement> premises = new List<Judgement>();
        premises.Add(new JudgementKinding(
            L, a.t, KindStar.STAR
        ));
        premises.Add(new JudgementTyping(
            new ContextWithTerm(L, a.x, a.t), a.body, tp.body
        ));

        return premises;
    }

    // like before, checker needs hints for now
    public List<Judgement> TApp(Var v, TType tt) {
        Term x2;

        if (x is Hole) {
            x2 = ((Hole) x).Get();
        } else {
            x2 = x;
        }

        if (!(x2 is App)) {
            throw new InvalidRuleApplicationException("TApp");
        }

        // TODO: implement [x -> t2]T

        App a = (App) x2;

        List<Judgement> premises = new List<Judgement>();
        premises.Add(new JudgementTyping(
            L, a.t1, new TypePi(v, tt, t)
        ));
        premises.Add(new JudgementTyping(
            L, a.t2, tt
        ));

        return premises;
    }

    public List<Judgement> TConv(TType t2) {
        List<Judgement> premises = new List<Judgement>();
        premises.Add(new JudgementTyping(
            L, x, t2
        ));
        premises.Add(new JudgementTypeEquiv(
            L, t, t2, KindStar.STAR
        ));

        return premises;
    }

    public override List<Judgement>? Apply(string s, Object[] args) {
        switch (s) {
            case "TVar":
                return TVar();
            case "TAbs":
                return TAbs();
            case "TApp":
                return TApp((Var) args[0], (TType) args[1]);
            case "TConv":
                return TConv((TType) args[0]);
            default:
                return null;
        }
    }

    public override string ToString()
    {
        return String.Format("{0} ⊢ {1} : {2}", L, x, t);
    }
}