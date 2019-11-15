using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverListener : MonoBehaviour {

    public Color defaultcolor = Color.white;
    public Color highlightcolor = Color.red;

    void Start()
    {
        GetComponent<Renderer>().material.color = defaultcolor;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = highlightcolor;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = defaultcolor;
    }
}
