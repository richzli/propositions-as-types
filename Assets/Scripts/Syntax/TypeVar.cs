namespace Syntax;

class TypeVar : Type {
    private string name { get; set; }

    public TypeVar(string name) {
        this.name = name;
    }

    public override string ToString() {
        return name;
    }
}