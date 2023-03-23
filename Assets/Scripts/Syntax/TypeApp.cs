namespace Syntax;

class TypeApp : Type {
    private Type t1 { get; set; }
    private Term t2 { get; set; }

    public TypeApp(Type t1, Term t2) {
        this.t1 = t1;
        this.t2 = t2;
    }

    public override string ToString() {
        return string.Format("{0} {1}", t1, t2);
    }
}