namespace Syntax;

public class ContextEmpty : Context {
    private ContextEmpty() {}

    private static ContextEmpty? instance = null;

    public static ContextEmpty EMPTY {
        get {
            if (instance == null) {
                instance = new ContextEmpty();
            }
            return instance;
        }
    }

    public override string ToString()
    {
        return "∅";
    }

    public override TType? Check(Var x) {
        return null;
    }

    public override Kind? Check(TypeVar t) {
        return null;
    }
}