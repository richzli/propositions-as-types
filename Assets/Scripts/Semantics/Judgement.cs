using Syntax;

namespace Semantics;

class Judgement {
    public Context L { get; set; }

    public Judgement(Context L) {
        this.L = L;
    }
}