using Syntax;

namespace Semantics;

public class JudgementWF : Judgement {
    public Kind k { get; set; }

    public JudgementWF(Context L, Kind k) : base(L) {
        this.k = k;
    }

    public List<Judgement> WFStar() {
        if (!(k is KindStar)) {
            throw new InvalidRuleApplicationException("WFStar");
        }

        return new List<Judgement>();
    }

    public List<Judgement> WFPi() {
        if (!(k is KindPi)) {
            throw new InvalidRuleApplicationException("WFPi");
        }

        KindPi kp = (KindPi) k;

        List<Judgement> premises = new List<Judgement>();
        premises.Add(new JudgementKinding(
            L, kp.t, KindStar.STAR
        ));
        premises.Add(new JudgementWF(
            new ContextWithTerm(L, kp.x, kp.t), kp.kind
        ));

        return premises;
    }

    public override List<Judgement>? Apply(string s, Object[] args) {
        switch (s) {
            case "WFStar":
                return WFStar();
            case "WFPi":
                return WFPi();
            default:
                return null;
        }
    }

    public override string ToString()
    {
        return String.Format("{0} ⊢ {1}", L, k);
    }
}