namespace Syntax;

class KindPi : Kind {
    private Var x { get; set; }
    private Type t { get; set; }
    private Kind kind { get; set; }

    public KindPi(Var x, Type t, Kind kind) {
        this.x = x;
        this.t = t;
        this.kind = kind;
    }

    public override string ToString() {
        return string.Format("(Π{0}:{1}. {2})", x, t, kind);
    }
}