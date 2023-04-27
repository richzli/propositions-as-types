using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

using Syntax;
using Semantics;
using Theory;

public class InferenceDisplay : MonoBehaviour {
    public Inference? infer;

    public GameObject MakeChild(Inference i, Transform t) {
        Debug.Log(GameObject.Find("GameManager").GetComponent<PrefabHolder>());
        GameObject child = Instantiate(
            GameObject.Find("GameManager").GetComponent<PrefabHolder>().prefab,
            t
        );
        Debug.Log($"{child}");
        return child;
    }

    public void Update() {
        if (infer == null) {
            return;
        }

        gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = infer.conclusion.ToString();
    }
}