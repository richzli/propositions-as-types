namespace Syntax;

class Var : Term {
    public static int counter = 1;
    public static HashSet<string> names = new HashSet<String>();

    public string name { get; set; }

    public Var() {
        this.name = GetNewName();
    }

    public Var(string name) {
        this.name = name.ToLower();
        Var.names.Add(name.ToLower());
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
                nm = Convert.ToChar(97 /* a */ + (i % 26)) + nm;
                i /= 26;
                --j;
            } while (j > 0);
            counter++;
        } while (!Var.names.Add(nm));

        return nm;
    }
}