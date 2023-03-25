using Syntax;

namespace Semantics;

public class Judgement {
    public Context L { get; set; }

    public Judgement(Context L) {
        this.L = L;
    }

    public virtual List<Judgement>? Apply(string s, Object[] args) {
        return null;
    }
}