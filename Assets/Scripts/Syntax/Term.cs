namespace Syntax;

public abstract class Term {
    // whether the term has holes or not
    public abstract bool Filled();
    public abstract bool Is(Object t);
}