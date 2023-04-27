using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Syntax;
using Semantics;
using Theory;

public class RootUpdater : MonoBehaviour {
    void Start() {
        GlobalState.root = gameObject.transform.GetChild(0).gameObject;
    }

    void Update() {
        gameObject.transform.GetChild(0).gameObject.GetComponent<InferenceDisplay>().infer = GlobalState.root?.GetComponent<InferenceDisplay>()?.infer;
    }
}