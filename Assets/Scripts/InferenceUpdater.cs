using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Syntax;
using Semantics;
using Theory;

public class InferenceUpdater : MonoBehaviour {
    void Start() {
        gameObject.GetComponent<Text>().text = "";
    }

    void Update() {
        gameObject.GetComponent<Text>().text = "";
    }
}