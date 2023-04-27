using System;
using System.Collections;
using System.Collections.Generic;

using Syntax;
using Semantics;
using Theory;

class Program {
    static void Main(string[] args) {
        ModusTollens();
        //ModusPonens();
        //test();
    }

    static void ModusTollens() {
        // modus tollens
        // (P -> Q) -> (not Q -> not P)
        // not is defined as not X = forall Y: X -> Y
        Inference goal = new Inference(new JudgementTyping(
            new Context()/*.With(
                new Var("not"),
                new Pi(new Var("X"), Sort.PROP,
                    new Pi(new Var("Y"), Sort.PROP,
                        new Pi(new Var("z"), new Var("X"), new Var("Y"))
                    )
                )
            )*/,
            new Context(),
            new Hole(),
            new Pi(new Var("A"), Sort.PROP,
                new Pi(new Var("B"), Sort.PROP,
                    new Pi(new Var("x"), new Pi(new Var("y"), new Var("A"), new Var("B")),
                        /* TODO: after implementing conversion rules
                        new Pi(new Var("z"), new App(new Var("not"), new Var("B")),
                            new App(new Var("not"), new Var("A"))
                        )
                        */
                        new Pi(new Var("z"), new Pi(new Var("C"), Sort.PROP, new Pi(new Var("b"), new Var("B"), new Var("C"))),
                            new Pi(new Var("D"), Sort.PROP, new Pi(new Var("a"), new Var("A"), new Var("D")))
                        )
                    )
                )
            )
        ));
        Console.WriteLine(goal.conclusion);

        List<Inference> tp = new List<Inference>();
        Inference goal1 = goal;
        
        Tactics.Intro(goal1);
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Tactics.Intro(goal1);
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Tactics.Intro(goal1);
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Tactics.Intro(goal1);
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Tactics.Intro(goal1);
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Tactics.Intro(goal1);
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Console.WriteLine(goal1.conclusion);
        Console.WriteLine(goal.conclusion);

        Tactics.Apply(goal1, new App(new Var("z"), new Var("D")), new Var("D"));
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Console.WriteLine(goal1.conclusion);
        Console.WriteLine(goal.conclusion);

        Tactics.Apply(goal1, new Var("x"), new Var("B"));
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Console.WriteLine(goal1.conclusion);
        Console.WriteLine(goal.conclusion);

        Tactics.Exact(goal1, new Var("a"));
        tp.Add(goal1.premises![0]);

        Console.WriteLine(goal.conclusion);

        while (tp.Count != 0) {
            Inference i = tp[tp.Count - 1];
            tp.RemoveAt(tp.Count - 1);

            TypeChecker.Check(i);

            tp.AddRange(i.premises!);
        }

        if (goal.Valid()) {
            Console.WriteLine("Proof is complete!");
        } else {
            Console.WriteLine("Incomplete proof.");
        }
    }

    static void ModusPonens() {
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

        Tactics.Apply(goal3, new Var("y"), new Var("B"));

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
            Inference i = tp[tp.Count - 1];
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

    static void test() {

        Inference goal = new Inference(new JudgementTyping(
            new Context(),
            new Context(),
            new Hole(),
            new Pi(new Var("B"), Sort.PROP,
                    new Pi(new Var("x"), new Var("B"),
                        new Pi(new Var("y"),
                            new Pi(new Var("a"), new Var("B"), new Var("B")),
                            new Var("B")
                        )
                    )
                )
            ));

        Console.WriteLine($"goal: {goal.conclusion}");

        List<Inference> tp = new List<Inference>();
        Inference goal1 = goal;

        Tactics.Intro(goal1);
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Console.WriteLine(goal1.conclusion);
        Console.WriteLine(goal.conclusion);

        Tactics.Intro(goal1);
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Console.WriteLine(goal1.conclusion);
        Console.WriteLine(goal.conclusion);

        Tactics.Intro(goal1);
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Console.WriteLine(goal1.conclusion);
        Console.WriteLine(goal.conclusion);

        Tactics.Apply(goal1, new Var("y"), new Var("B"));
        tp.Add(goal1.premises![0]);
        goal1 = goal1.premises![1];

        Console.WriteLine(goal1.conclusion);
        Console.WriteLine(goal.conclusion);

        Tactics.Exact(goal1, new Var("x"));
        tp.Add(goal1.premises![0]);

        Console.WriteLine(goal1.conclusion);
        Console.WriteLine(goal.conclusion);

        while (tp.Count != 0) {
            Inference i = tp[tp.Count - 1];
            tp.RemoveAt(tp.Count - 1);

            TypeChecker.Check(i);

            tp.AddRange(i.premises!);
        }

        if (goal.Valid()) {
            Console.WriteLine("Proof is complete!");
        } else {
            Console.WriteLine("Incomplete proof.");
        }
    }
}