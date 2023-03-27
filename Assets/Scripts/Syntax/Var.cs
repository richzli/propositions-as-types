namespace Syntax;

public class Var : Term
{
    
    public static int counter = 1;
    public static HashSet<string> names = new HashSet<String>();

    public string name { get; set; }

    public Var() {
        this.name = GetNewName();
    }

    public Var(Var prev) {
        this.name = prev.name;
    }

    public Var(string name) {
        this.name = name;
        Var.names.Add(name);
    }

    public override bool Is(Object t) {
        if (t is Hole && ((Hole) t).Filled()) {
            return this.Is(((Hole) t).x!);
        }

        if (!(t is Var)) {
            return false;
        }

        return this.name.Equals(((Var) t).name);
    }

    public override string ToString() {
        return name;
    }

    public override bool Filled() {
        return true;
    }

    public override HashSet<Var> Free() {
        return new HashSet<Var> { this };
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Var)) {
            return false;
        }

        return this.Is((Var) obj);
    }

    public override int GetHashCode()
    {
        return name.GetHashCode();
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