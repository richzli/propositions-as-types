namespace Syntax;

class Abs : Term {
    private Var x { get; set; }
    private Type t { get; set; }
    private Term body { get; set; }

    public Abs(Var x, Type t, Term body) {
        this.x = x;
        this.t = t;
        this.body = body;
    }
    
    public override string ToString() {
        return string.Format("(λ{0}:{1}. {2})", x, t, body);
    }
}