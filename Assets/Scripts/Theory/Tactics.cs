using Syntax;
using Semantics;

namespace Theory;

public class Tactics {
    public static bool Intro(Inference i) {
        if (!(i.conclusion is JudgementTyping)) {
            return false;
        }

        JudgementTyping j = (JudgementTyping) i.conclusion;

        if (!(j.x is Hole && j.t is TypePi)) {
            return false;
        }

        Hole h = (Hole) j.x;
        TypePi tp = (TypePi) j.t;

        h.Fill(new Abs(tp.x, tp.t, new Hole()));
        return i.Apply("TAbs", new Object[0]);
    }

    public static bool Exact(Inference i, Var x) {
        if (!(i.conclusion is JudgementTyping)) {
            return false;
        }

        JudgementTyping j = (JudgementTyping) i.conclusion;

        if (!(j.x is Hole)) {
            return false;
        }

        Hole h = (Hole) j.x;

        h.Fill(x);
        return i.Apply("TVar", new Object[0]);
    }

    public static bool Apply(Inference i, Var x) {
        if (!(i.conclusion is JudgementTyping)) {
            return false;
        }

        JudgementTyping j = (JudgementTyping) i.conclusion;

        if (!(j.x is Hole)) {
            return false;
        }

        Hole h = (Hole) j.x;

        TType? tt = j.L.Check(x);
        if (tt == null || !(tt is TypePi)) {
            return false;
        }
        TypePi tp = (TypePi) tt;

        h.Fill(new App(x, new Hole()));
        return i.Apply("TApp", new Object[2] { tp.x, tp.t });
    }
}