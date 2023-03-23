namespace Syntax;

class TypeVar : TType {
    public string name { get; set; }

    public TypeVar(string name) {
        this.name = name;
    }

    public override string ToString() {
        return name;
    }
}