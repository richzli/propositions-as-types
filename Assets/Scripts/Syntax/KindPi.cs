namespace Syntax;

public class KindPi : Kind {
    public Var x { get; set; }
    public TType t { get; set; }
    public Kind kind { get; set; }

    public KindPi(KindPi prev) {
        this.x = prev.x;
        this.t = prev.t;
        this.kind = prev.kind;
    }

    public KindPi(Var x, TType t, Kind kind) {
        this.x = x;
        this.t = t;
        this.kind = kind;
    }

    public override bool Is(object o)
    {
        if (!(o is KindPi)) {
            return false;
        }

        return x.Is(((KindPi) o).x) && t.Is(((KindPi) o).t) && kind.Is(((KindPi) o).kind);
    }

    public override string ToString() {
        return string.Format("(Π{0}:{1}. {2})", x, t, kind);
    }

    public KindPi Subst(Var x, Term t) {
        KindPi kp = new KindPi(this);
        
        if (kp.kind is KindPi) {
            kp.kind = ((KindPi) kp.kind).Subst(x, t);
        }

        return kp;
    }
}