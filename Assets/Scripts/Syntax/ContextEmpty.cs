namespace Syntax;

class ContextEmpty : Context {
    public override string ToString()
    {
        return "∅";
    }

    public TType? Check(Var x) {
        return null;
    }

    public Kind? Check(TypeVar t) {
        return null;
    }
}