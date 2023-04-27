using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Syntax;
using Semantics;
using Theory;

public class RootUpdater : MonoBehaviour {
    void Update() {
        gameObject.GetComponent<InferenceDisplay>().infer = GlobalState.root;
    }
}