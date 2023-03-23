namespace Syntax;

class Context {
    private Dictionary<Var, Type> termBind { get; set; }
    private Dictionary<TypeVar, Kind> typeBind { get; set; }

    public Context() {
        termBind = new Dictionary<Var, Type>();
        typeBind = new Dictionary<TypeVar, Kind>();
    }

    public bool Set(Var x, Type t) {
        if (termBind.ContainsKey(x)) {
            return false;
        }

        termBind.Add(x, t);
        return true;
    }

    public bool Set(TypeVar x, Kind k) {
        if (typeBind.ContainsKey(x)) {
            return false;
        }

        typeBind.Add(x, k);
        return true;
    }

    public Type? Get(Var x) {
        Type? t;
        termBind.TryGetValue(x, out t);
        return t;
    }

    public Kind? Get(TypeVar x) {
        Kind? k;
        typeBind.TryGetValue(x, out k);
        return k;
    }
}