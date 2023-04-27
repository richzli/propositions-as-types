using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Syntax;
using Semantics;
using Theory;

public class TacticApplier : MonoBehaviour {
    public InputField input1, input2;

    public void Intro() {
        if (GlobalState.GetCurrentGoal() == null) {
            return;
        }

        Tactics.Intro(GlobalState.GetCurrentGoal());
        GlobalState.Resolve();
        input1.text = "";
        input2.text = "";
    }

    public void Apply() {
        if (GlobalState.GetCurrentGoal() == null) {
            return;
        }

        int i1 = 0, i2 = 0;
        Term t1 = Parse(input1.text, ref i1), t2 = Parse(input2.text, ref i2);
        Tactics.Apply(GlobalState.GetCurrentGoal(), t1, t2);
        GlobalState.Resolve();
        input1.text = "";
        input2.text = "";
    }

    public void Exact() {
        if (GlobalState.GetCurrentGoal() == null) {
            return;
        }

        int i1 = 0;
        Tactics.Exact(GlobalState.GetCurrentGoal(), (Var) Parse(input1.text, ref i1));
        GlobalState.Resolve();
        input1.text = "";
        input2.text = "";
    }

    public void Check() {
        GlobalState.Check();
    }

    static Term? Parse(string text, ref int i) {
        int j = i;
        while (text[j] != '(') {
            ++j;
        }
        if (j == i) {
            while (text[j] != ')') {
                ++j;
            }
            string v = text.Substring(i+1, j-i-1);
            i = j + 1;
            return new Var(v);
        }

        string type = text.Substring(i, j-i);
        i = j + 1;
        switch (type) {
            case "Abs": {
                Term x, t, b;
                x = Parse(text, ref i);
                if (text[i++] != ',') return null;
                t = Parse(text, ref i);
                if (text[i++] != ',') return null;
                b = Parse(text, ref i);
                if (text[i++] != ')') return null;
                if (!(x is Var)) return null;
                return new Abs((Var) x, t, b);
            }
            case "Pi": {
                Term x, t, b;
                x = Parse(text, ref i);
                if (text[i++] != ',') return null;
                t = Parse(text, ref i);
                if (text[i++] != ',') return null;
                b = Parse(text, ref i);
                if (text[i++] != ')') return null;
                if (!(x is Var)) return null;
                return new Pi((Var) x, t, b);
            }
            case "App": {
                Term t1, t2;
                t1 = Parse(text, ref i);
                if (text[i++] != ',') return null;
                t2 = Parse(text, ref i);
                if (text[i++] != ')') return null;
                return new App(t1, t2);
            }
            default:
                return null;
        }
    }
}