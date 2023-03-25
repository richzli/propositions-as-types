namespace Syntax;

public class TypeApp : TType {
    public TType t1 { get; set; }
    public Term t2 { get; set; }

    public TypeApp(TType t1, Term t2) {
        this.t1 = t1;
        this.t2 = t2;
    }

    public override bool Is(Object ta)
    {
        if (!(ta is TypeApp)) {
            return false;
        }

        return t1.Is(((TypeApp) ta).t1) && t2.Is(((TypeApp) ta).t2);
    }

    public override string ToString() {
        return string.Format("{0} {1}", t1, t2);
    }
}