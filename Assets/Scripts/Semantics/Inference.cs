using Syntax;

namespace Semantics;

class Inference {
    // when premises is null, the inference has not been populated
    // when premises is empty, the goal is an axiom
    public List<Inference>? premises { get; set; }
    public Judgement goal { get; set; }

    public Inference(Judgement goal) {
        this.premises = null;
        this.goal = goal;
    }

    public Inference(List<Inference> premises, Judgement goal) {
        this.premises = premises;
        this.goal = goal;
    }
}