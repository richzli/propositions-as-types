using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Syntax;
using Semantics;
using Theory;

public class ProofSetter : MonoBehaviour {
    public void ModusTollens() {
        GlobalState.SetRoot(
            new Inference(new JudgementTyping(
                new Context(),
                new Context(),
                new Hole(),
                new Pi(new Var("A"), Sort.PROP,
                    new Pi(new Var("B"), Sort.PROP,
                        new Pi(new Var("x"), new Pi(new Var("y"), new Var("A"), new Var("B")),
                            new Pi(new Var("z"), new Pi(new Var("C"), Sort.PROP, new Pi(new Var("b"), new Var("B"), new Var("C"))),
                                new Pi(new Var("D"), Sort.PROP, new Pi(new Var("a"), new Var("A"), new Var("D")))
                            )
                        )
                    )
                )
            ))
        );
    }

    public void ModusPonens() {
        GlobalState.SetRoot(
            new Inference(new JudgementTyping(
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
            ))
        );
    }
}