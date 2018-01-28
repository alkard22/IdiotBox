using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphKeyLineItem : MonoBehaviour {

    public Color color;
    public string title;

    public Image image;
    public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        image.color = color;
        text.text = title;
	}
}
