using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverListener : MonoBehaviour {

    //public Color defaultcolor = Color.white;
    public Color highlightcolor = Color.red;
    private Color colcache;

    void Start()
    {
        //GetComponent<Renderer>().material.color = defaultcolor;
    }

    void OnMouseEnter()
    {
        this.colcache = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = highlightcolor;
    }

    void OnMouseExit()
    {
        if(GetComponent<Renderer>().material.color == this.highlightcolor)
            GetComponent<Renderer>().material.color = this.colcache;
    }
}
