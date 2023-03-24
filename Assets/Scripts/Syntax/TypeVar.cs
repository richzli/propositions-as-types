namespace Syntax;

class TypeVar : TType {
    public static int counter = 1;
    public static HashSet<string> names = new HashSet<String>();

    public string name { get; set; }

    public TypeVar() {
        this.name = GetNewName();
    }

    public TypeVar(string name) {
        this.name = name.ToUpper();
        TypeVar.names.Add(name.ToUpper());
    }

    public override string ToString() {
        return name;
    }

    private static string GetNewName() {
        string nm;

        do {
            nm = "";
            int i = counter;
            int j = 1;
            int k = 26;
            while (i >= k) {
                i -= k;
                ++j;
                k *= 26;
            }
            do {
                nm = Convert.ToChar(65 /* A */ + (i % 26)) + nm;
                i /= 26;
                --j;
            } while (j > 0);
            counter++;
        } while (!TypeVar.names.Add(nm));

        return nm;
    }
}