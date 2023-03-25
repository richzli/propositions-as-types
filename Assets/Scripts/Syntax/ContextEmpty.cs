namespace Syntax;

class ContextEmpty : Context {
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