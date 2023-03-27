using Syntax;
using Semantics;

namespace Theory;

public class TypeChecker {
    // TODO: finish type checker
    public static bool Check(Inference i) {
        if (i.conclusion is JudgementTyping) {
            JudgementTyping j = (JudgementTyping) i.conclusion;
            if (j.x is Var) {
                return i.Apply("Var", new Object[0]);
            } else if (j.x is Pi && j.t.Is(Sort.PROP)) {
                Pi p = (Pi) j.x;
                if (p.t.Is(Sort.PROP)) {
                    return i.Apply("ProdTypeProp", new Object[1] { 0 });
                } else {
                    return i.Apply("ProdPropProp", new Object[0]);
                }
            } else if (j.x is Sort) {
                return i.Apply("Axiom", new Object[0]);
            }
        } else if (i.conclusion is JudgementChecking) {
            JudgementChecking j = (JudgementChecking) i.conclusion;
            return i.Apply("Check", new Object[0]);
        }

        return false;
    }
}