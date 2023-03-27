namespace Syntax;

public abstract class Term
{
    // whether the term has holes or not
    public abstract bool Filled();
    public abstract bool Is(Object t);

    public virtual Term Get() {
        return this;
    }

    public virtual HashSet<Var> Free() {
        return new HashSet<Var>();
    }

    public virtual Term Subst(Term v, Term t) {
        return this;
    }
}