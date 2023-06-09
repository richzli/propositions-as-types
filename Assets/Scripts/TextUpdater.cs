using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Syntax;
using Semantics;
using Theory;

public class TextUpdater : MonoBehaviour {
    void Start() {
        gameObject.GetComponent<Text>().text = "";
    }

    void Update() {
        gameObject.GetComponent<Text>().text =
$@"{GlobalState.goals.Count} subgoal(s)
{FormatContext()}
----------
{((GlobalState.GetCurrentGoal() == null) ? "No goals remaining" : GlobalState.GetCurrentGoal().conclusion.GetGoal())}

----------
{((GlobalState.root?.GetComponent<InferenceDisplay>()?.infer == null) ? "No program formed" : GlobalState.root.GetComponent<InferenceDisplay>().infer.conclusion.GetTerm())}

{((GlobalState.root.GetComponent<InferenceDisplay>()?.infer != null && GlobalState.GetCurrentGoal() == null && GlobalState.checks.Count == 0) ? "Type checked successfully!" : "")}";
    }

    static String FormatContext() {
        if (GlobalState.GetCurrentGoal() == null) {
            return "";
        }

        Context L = GlobalState.GetCurrentContext();
        String ret = "";
        foreach (KeyValuePair<Var, Term> entry in L.c) {
            ret += $"{entry.Key}: {entry.Value}\n";
        }

        return ret;
    }
}