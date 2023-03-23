using Syntax;

namespace Semantics;

class Rule {
    public List<Judgement> premises { get; set; }
    public Judgement goal { get; set; }

    public Rule(List<Judgement> premises, Judgement goal) {
        this.premises = premises;
        this.goal = goal;
    }
}