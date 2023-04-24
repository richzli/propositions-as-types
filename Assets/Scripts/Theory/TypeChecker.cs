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
                    return i.Apply("ProdTypeProp", new Object[1] { 1 });
                } else {
                    return i.Apply("ProdPropProp", new Object[0]);
                }
            } else if (j.x is App) {
                App a = (App) j.x;
                Pi? p = null;
                Abs? aa = null;
                if (a.t1 is Pi) {
                    p = (Pi) a.t1;
                } else if (a.t1 is Abs) {
                    aa = (Abs) a.t1;
                } else if (a.t1 is Var) {
                    Var v = (Var) a.t1;
                    if (j.L.Has(v)) {
                        if (j.L.Get(v) is Pi) {
                            p = (Pi) j.L.Get(v)!;
                        } else if (j.L.Get(v) is Abs) {
                            aa = (Abs) j.L.Get(v)!;
                        }
                    } else if (j.E.Has(v)) {
                        if (j.E.Get(v) is Pi) {
                            p = (Pi) j.E.Get(v)!;
                        } else if (j.L.Get(v) is Abs) {
                            aa = (Abs) j.E.Get(v)!;
                        }
                    }
                }

                if (p != null) {
                    return i.Apply("App", new Object[3] { p.x, p.t, p.body });
                } else if (aa != null) {
                    return i.Apply("App", new Object[3] { aa.x, aa.t, aa.body });
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