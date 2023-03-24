namespace Syntax;

class ContextWithTerm : Context {
    public Context prev { get; set; }
    public Var x { get; set; }
    public TType t { get; set; }

    public ContextWithTerm(Context prev, Var x, TType t) {
        this.prev = prev;
        this.x = x;
        this.t = t;
    }

    public override string ToString()
    {
        return String.Format("{0}, {1}:{2}", prev, x, t);
    }

    public TType? Check(Var x) {
        if (this.x.Equals(x)) {
            return t;
        } else {
            return prev.Check(x);
        }
    }

    public Kind? Check(TypeVar t) {
        return prev.Check(t);
    }
}