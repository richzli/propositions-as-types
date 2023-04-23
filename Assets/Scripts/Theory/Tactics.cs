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
            Console.WriteLine("didn't find in context");
            return false;
        }
        Pi p = (Pi) tt;

        h.Fill(new App(x, new Hole()));
        return i.Apply("App", new Object[2] { p.x, p.t });
    }

    public static bool Apply(Inference i, Var x, Pi a) {
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
            Console.WriteLine("didn't find in context");
            h.Fill(new App(x, new Hole()));
            Console.WriteLine(i.conclusion);
            Console.WriteLine($"a.x: {a.x}, a.t: {a.t}");
            return i.Apply("App", new Object[2] { a.x, a.t });
        }
        Pi p = (Pi) tt;

        h.Fill(new App(x, new Hole()));
        return i.Apply("App", new Object[2] { p.x, p.t });
    }

    public static bool Apply(Inference i, App a) {
        if (!(i.conclusion is JudgementTyping)) {
            return false;
        }

        JudgementTyping j = (JudgementTyping) i.conclusion;

        if (!(j.x is Hole)) {
            return false;
        }

        Hole h = (Hole) j.x;

        // TODO: figure out how to do multiple levels of App later, i.e., what if tt is App
        // honestly could be simplified to some beta reduction or something idk brain not working rn
        if (!(a.t1 is Var)) {
            return false;
        }

        Term? tt = j.Get((Var) a.t1);
        if (tt == null || !(tt is Pi)) {
            return false;
        }
        Pi p = (Pi) tt;

        Term t2 = p.body.Subst(p.x, a.t2);
        if (!(t2 is Pi)) {
            return false;
        }
        Pi p2 = (Pi) t2;

        h.Fill(new App(a, new Hole()));
        return i.Apply("App", new Object[2] { p2.x, p2.t });
    }
}