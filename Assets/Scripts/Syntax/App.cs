namespace Syntax;

class App : Term {
    public Term t1 { get; set; }
    public Term t2 { get; set; }

    public App(Term t1, Term t2) {
        this.t1 = t1;
        this.t2 = t2;
    }

    public override string ToString() {
        return string.Format("{0} {1}", t1, t2);
    }
}