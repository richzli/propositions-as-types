namespace Syntax;

class ContextWithType : Context {
    private Context prev { get; set; }
    private TypeVar x { get; set; }
    private Kind k { get; set; }

    public ContextWithType(Context prev, TypeVar x, Kind k) {
        this.prev = prev;
        this.x = x;
        this.k = k;
    }

    public override string ToString()
    {
        return String.Format("{0}, {1}:{2}", prev, x, k);
    }
}