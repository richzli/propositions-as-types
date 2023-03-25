using Syntax;

namespace Semantics;

public class JudgementKinding : Judgement {
    public TType t { get; set; }
    public Kind k { get; set; }

    public JudgementKinding(Context L, TType t, Kind k) : base(L) {
        this.t = t;
        this.k = k;
    }

    public List<Judgement> KVar() {
        if (!(t is TypeVar)) {
            throw new InvalidRuleApplicationException("KVar");
        }

        TypeVar tv = (TypeVar) t;

        List<Judgement> premises = new List<Judgement>();
        premises.Add(new JudgementKindCheck(
            L, tv, k
        ));
        premises.Add(new JudgementWF(
            L, k
        ));

        return premises;
    }

    public List<Judgement> KPi() {
        if (!(t is TypePi && k is KindStar)) {
            throw new InvalidRuleApplicationException("KPi");
        }

        TypePi tp = (TypePi) t;

        List<Judgement> premises = new List<Judgement>();
        premises.Add(new JudgementKinding(
            L, tp.t, KindStar.STAR
        ));
        premises.Add(new JudgementKinding(
            new ContextWithTerm(L, tp.x, tp.t), tp.body, KindStar.STAR
        ));

        return premises;
    }

    // the checker needs hints for x -- once alpha equivalence is implemented we don't need it anymore
    public List<Judgement> KApp(Var x, TType tt) {
        if (!(t is TypeApp)) {
            throw new InvalidRuleApplicationException("KApp");
        }

        // TODO: add support for parametrized kinds, i.e. [x -> t]K

        TypeApp ta = (TypeApp) t;

        List<Judgement> premises = new List<Judgement>();
        premises.Add(new JudgementKinding(
            L, ta.t1, new KindPi(x, tt, k)
        ));
        premises.Add(new JudgementTyping(
            L, ta.t2, tt
        ));

        return premises;
    }

    public List<Judgement> KConv(Kind k2) {
        List<Judgement> premises = new List<Judgement>();
        premises.Add(new JudgementKinding(
            L, t, k2
        ));
        premises.Add(new JudgementKindEquiv(
            L, k, k2
        ));

        return premises;
    }

    public override List<Judgement>? Apply(string s, Object[] args) {
        switch (s) {
            case "KVar":
                return KVar();
            case "KPi":
                return KPi();
            case "KApp":
                return KApp((Var) args[0], (TType) args[1]);
            case "KConv":
                return KConv((Kind) args[0]);
            default:
                return null;
        }
    }

    public override string ToString()
    {
        return String.Format("{0} ⊢ {1} :: {2}", L, t, k);
    }
}