namespace Syntax;

class KindPi : Kind {
    public Var x { get; set; }
    public TType t { get; set; }
    public Kind kind { get; set; }

    public KindPi(Var x, TType t, Kind kind) {
        this.x = x;
        this.t = t;
        this.kind = kind;
    }

    public override string ToString() {
        return string.Format("(Π{0}:{1}. {2})", x, t, kind);
    }
}