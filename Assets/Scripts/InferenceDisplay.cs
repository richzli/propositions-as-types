using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Syntax;
using Semantics;
using Theory;

public class InferenceDisplay : MonoBehaviour {
    public GameObject prefab;

    bool lastFrameNull;
    public Inference? infer;
    List<GameObject> children;

    void Start() {
        this.lastFrameNull = true;
        this.infer = null;
        this.children = new List<GameObject>();
    }

    public void UpdateChildren() {
        foreach (GameObject child in children) {
            GameObject.Destroy(child);
        }
        this.children = new List<GameObject>();

        if (infer.premises == null) return;

        foreach (Inference i in infer.premises) {
            GameObject child = Instantiate(prefab);
            child.transform.setParent(gameObject.GetComponentInChildren<Canvas>().transform);
            child.GetComponent<InferenceDisplay>().infer = i;
            this.children.Add(child);
        }
    }

    public void Update() {
        if (infer == null) {
            return;
        }

        Text text = gameObject.GetComponentInChildren<Text>();
        text.text = infer.conclusion.ToString();

        if (lastFrameNull != (this.infer.premises == null)) {
            UpdateChildren();
        }

        lastFrameNull = (this.infer.premises == null);
    }
}