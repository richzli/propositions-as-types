using System;
using System.Collections;
using System.Collections.Generic;

using Syntax;
using Semantics;
using Theory;

public static class GlobalState {
    public static Inference? root = null;
    public static List<Inference> goals = new List<Inference>();

    public static void SetRoot(Inference i) {
        root = i;
        goals = new List<Inference>() { root };
    }

    public static Inference? GetCurrentGoal() {
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

        Inference goal = GetCurrentGoal()!;

        if (!(goal.conclusion.Filled() && goal.premises != null)) {
            return false;
        }
        
        goals.Remove(goal);
        foreach (Inference i in goal.premises!) {
            if (!i.conclusion.Filled()) {
                goals.Add(i);
            }
        }

        return true;
    }
}