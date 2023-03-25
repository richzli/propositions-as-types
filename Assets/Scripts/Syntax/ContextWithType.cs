namespace Syntax;

public class ContextWithType : Context {
    public Context prev { get; set; }
    public TypeVar x { get; set; }
    public Kind k { get; set; }

    public ContextWithType(Context prev, TypeVar x, Kind k) {
        this.prev = prev;
        this.x = x;
        this.k = k;
    }

    public override string ToString()
    {
        return String.Format("{0}[{1}:{2}]", prev, x, k);
    }

    public override TType? Check(Var x) {
        return prev.Check(x);
    }

    public override Kind? Check(TypeVar t) {
        if (this.x.Is(t)) {
            return k;
        } else {
            return prev.Check(t);
        }
    }
}