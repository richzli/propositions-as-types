namespace Syntax;

class TypePi : Type {
    private Var x { get; set; }
    private Type t { get; set; }
    private Type body { get; set; }

    public TypePi(Var x, Type t, Type body) {
        this.x = x;
        this.t = t;
        this.body = body;
    }

    public override string ToString() {
        return string.Format("(Π{0}:{1}. {2})", x, t, body);
    }
}