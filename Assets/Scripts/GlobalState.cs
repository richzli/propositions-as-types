using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Syntax;
using Semantics;
using Theory;

public static class GlobalState {
    public static GameObject? root = null;
    public static List<GameObject> goals = new List<GameObject>();
    public static List<GameObject> checks = new List<GameObject>();

    public static void SetRoot(Inference i) {
        foreach (Transform child in root.transform.GetChild(0)) {
            GameObject.Destroy(child.gameObject);
        }
        root.GetComponent<InferenceDisplay>().infer = i;
        goals = new List<GameObject>() { root };
        checks = new List<GameObject>();
    }

    public static Inference? GetCurrentGoal() {
        if (goals.Count == 0) {
            return null;
        }
        return goals[goals.Count - 1]?.GetComponent<InferenceDisplay>()?.infer;
    }

    public static GameObject? GetCurrentGoalObject() {
        if (goals.Count == 0) {
            return null;
        }
        return goals[goals.Count - 1];
    }

    public static Context? GetCurrentContext() {
        return GetCurrentGoal()?.conclusion.L;
    }

    public static bool Resolve() {
        if (GetCurrentGoal() == null) {
            return true;
        }

        GameObject goal = GetCurrentGoalObject()!;
        Inference g = goal.GetComponent<InferenceDisplay>()!.infer;

        if (!(g.conclusion.Filled() && g.premises != null)) {
            return false;
        }
        
        goals.Remove(goal);
        foreach (Inference i in g.premises!) {
            GameObject child = goal.GetComponent<InferenceDisplay>()!.MakeChild(i, goal.transform.GetChild(0));
            child.GetComponent<InferenceDisplay>().infer = i;
            if (!i.conclusion.Filled()) {
                goals.Add(child);
            } else {
                checks.Add(child);
            }
        }

        return true;
    }

    public static bool Check() {
        while (checks.Count != 0) {
            GameObject g = checks[checks.Count - 1];
            Inference i = g.GetComponent<InferenceDisplay>()!.infer;

            if (!TypeChecker.Check(i)) {
                return false;
            }
            checks.Remove(g);

            foreach (Inference j in i.premises!) {
                GameObject child = g.GetComponent<InferenceDisplay>()!.MakeChild(i, g.transform.GetChild(0));
                child.GetComponent<InferenceDisplay>().infer = j;
                checks.Add(child);
            }
        }

        return true;
    }
}