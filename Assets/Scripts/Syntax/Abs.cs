namespace Syntax;

class Abs : Term {
    public Var x { get; set; }
    public TType t { get; set; }
    public Term body { get; set; }

    public Abs(Var x, TType t, Term body) {
        this.x = x;
        this.t = t;
        this.body = body;
    }
    
    public override string ToString() {
        return string.Format("(λ{0}:{1}. {2})", x, t, body);
    }
}