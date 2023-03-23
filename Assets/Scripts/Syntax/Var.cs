namespace Syntax;

class Var : Term {
    private string name { get; set; }

    public Var(string name) {
        this.name = name;
    }

    public override string ToString() {
        return name;
    }
}