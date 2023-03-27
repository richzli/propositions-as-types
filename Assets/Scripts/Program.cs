using Syntax;
using Semantics;
using Theory;
using System;
class Program {
    static void Main(string[] args) {
        // modus ponens
        // A -> (A -> B) -> B
        Inference goal = new Inference(new JudgementTyping(
            new Context(),
            new Context(),
            new Hole(),
            new Pi(new Var("A"), Sort.PROP,
                new Pi(new Var("B"), Sort.PROP,
                    new Pi(new Var("x"), new Var("A"),
                        new Pi(new Var("y"),
                            new Pi(new Var("a"), new Var("A"), new Var("B")),
                            new Var("B")
                        )
                    )
                )
            )
        ));
        Console.WriteLine(goal.conclusion);

        Tactics.Intro(goal);

        Inference tpA = goal.premises![0];
        Inference goalA = goal.premises![1];
        Console.WriteLine(goalA.conclusion);

        Tactics.Intro(goalA);

        Inference tpB = goalA.premises![0];
        Inference goalB = goalA.premises![1];
        Console.WriteLine(goalB.conclusion);

        Tactics.Intro(goalB);

        Inference tp1 = goalB.premises![0];
        Inference goal2 = goalB.premises![1];
        Console.WriteLine(goal2.conclusion);

        // Console.WriteLine(goal.conclusion);

        Tactics.Intro(goal2);

        Inference tp2 = goal2.premises![0];
        Inference goal3 = goal2.premises![1];
        Console.WriteLine(goal3.conclusion);

        // Console.WriteLine(goal.conclusion);

        Tactics.Apply(goal3, new Var("y"));

        Inference tp3 = goal3.premises![0];
        Inference goal4 = goal3.premises![1];
        Console.WriteLine(goal4.conclusion);

        // Console.WriteLine(goal.conclusion);

        Tactics.Exact(goal4, new Var("x"));

        Console.WriteLine(goal.conclusion);
        
        Inference tp4 = goal4.premises![0];
        
        // at this point, we are done with filling holes--just need to type check everything else!
        // i already did it manually, and here are the commands for reference:
        /* 
        List<string> rules = new List<String>() {
            "Check", "Var", "Check",
            "ProdPropProp", "Var", "Check", "ProdPropProp", "Var", "Check", "Var", "Check",
            "ProdPropProp", "ProdPropProp", "Var", "Check", "ProdPropProp", "Var", "Check", "Var", "Check",
            "Var", "Check",
            "ProdTypeProp", "ProdPropProp", "ProdPropProp", "Var", "Check", "ProdPropProp", "Var", "Check", "Var", "Check", "Var", "Check", "Axiom",
            "ProdTypeProp", "ProdTypeProp", "ProdPropProp", "ProdPropProp", "Var", "Check", "ProdPropProp", "Var", "Check", "Var", "Check", "Var", "Check", "Axiom", "Axiom"
        };
        */
        // alternatively, the simple typechecker below also works for the simple example of modus ponens
        List<Inference> tp = new List<Inference>() { tpA, tpB, tp1, tp2, tp3, tp4 };
        int j = 0;
        while (tp.Count != 0) {
            Inference i = tp.Last();
            tp.RemoveAt(tp.Count - 1);

            TypeChecker.Check(i);
            // Console.Write($"{i.conclusion}: ");
            //i.Apply(rules[j] /*  Console.ReadLine()! */, new Object[1] { 0 }); // note: Prop : Type(0) is Axiom
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