using Syntax;
using Semantics;
using Theory;
using System;

class Program {
    static void Main(string[] args) {
        // modus ponens
        // A -> (A -> B) -> B
        Inference goal = new Inference(new JudgementTyping(
            // no dependent types yet, so manually say A and B are well-kinded
            new ContextWithType(
                new ContextWithType(
                    ContextEmpty.EMPTY,
                    new TypeVar("A"), KindStar.STAR
                ),
                new TypeVar("B"), KindStar.STAR
            ),
            new Hole(),
            new TypePi(new Var("x"), new TypeVar("A"),
                new TypePi(new Var("y"),
                    new TypePi(new Var("a"), new TypeVar("A"), new TypeVar("B")),
                    new TypeVar("B")
                )
            )
        ));
        Console.WriteLine(goal.conclusion);

        Tactics.Intro(goal);

        Inference tp1 = goal.premises![0];
        Inference goal2 = goal.premises![1];
        Console.WriteLine(goal2.conclusion);

        Tactics.Intro(goal2);

        Inference tp2 = goal2.premises![0];
        Inference goal3 = goal2.premises![1];
        Console.WriteLine(goal3.conclusion);

        Tactics.Apply(goal3, new Var("y"));

        Inference tp3 = goal3.premises![0];
        Inference goal4 = goal3.premises![1];
        Console.WriteLine(goal4.conclusion);

        Tactics.Exact(goal4, new Var("x"));
        
        Inference tp4 = goal4.premises![0];
        Inference tp5 = goal4.premises![1];
        
        // at this point, we are done with filling holes--just need to type check everything else!
        // i already did it manually, and here are the commands for reference:
        List<string> rules = new List<String>() {
            "KVar", "WFStar", "",
            "", "TVar", "KPi", "KVar", "WFStar", "", "KVar", "WFStar", "", "",
            "KPi", "KVar", "WFStar", "",
            "KVar", "WFStar", "",
            "KVar", "WFStar", ""
        };
        List<Inference> tp = new List<Inference>() { tp1, tp2, tp3, tp4, tp5 };
        int j = 0;
        while (tp.Count != 0) {
            Inference i = tp.Last();
            tp.RemoveAt(tp.Count - 1);

            i.Apply(rules[j], new Object[0]);
            ++j;

            tp.AddRange(i.premises!);
        }

        if (goal.Valid()) {
            Console.WriteLine("Proof is complete!");
        } else {
            Console.WriteLine("Incomplete proof.");
        }
    }
}