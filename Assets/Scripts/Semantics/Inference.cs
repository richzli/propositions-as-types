using Syntax;

namespace Semantics;

class Inference {
    public List<Inference>? premises { get; set; }
    public Judgement goal { get; set; }

    public Inference(Judgement goal) {
        this.premises = null;
        this.goal = goal;
    }

    public Inference(List<Judgement> premises, Judgement goal) {
        this.premises = premises;
        this.goal = goal;
    }
}