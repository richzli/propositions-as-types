namespace Syntax;

abstract class Context {
    public abstract TType? Check(Var x);
    public abstract Kind? Check(TypeVar t);
}