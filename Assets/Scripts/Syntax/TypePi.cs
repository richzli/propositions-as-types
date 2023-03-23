namespace Syntax;

class TypePi : TType {
    public Var x { get; set; }
    public TType t { get; set; }
    public TType body { get; set; }

    public TypePi(Var x, TType t, TType body) {
        this.x = x;
        this.t = t;
        this.body = body;
    }

    public override string ToString() {
        return string.Format("(Π{0}:{1}. {2})", x, t, body);
    }
}