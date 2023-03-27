using Syntax;
using Semantics;

namespace Theory;

public class Tactics {
    public static bool Intro(Inference i) {
        if (!(i.conclusion is JudgementTyping)) {
            return false;
        }

        JudgementTyping j = (JudgementTyping) i.conclusion;

        if (!(j.x is Hole && j.t is Pi)) {
            return false;
        }

        Hole h = (Hole) j.x;
        Pi p = (Pi) j.t;

        h.Fill(new Abs(p.x, p.t, new Hole()));
        return i.Apply("Abs", new Object[1] { Sort.PROP });
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
        return i.Apply("Var", new Object[0]);
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

        Term? tt = j.Get(x);
        if (tt == null || !(tt is Pi)) {
            return false;
        }
        Pi p = (Pi) tt;

        h.Fill(new App(x, new Hole()));
        return i.Apply("App", new Object[2] { p.x, p.t });
    }
}