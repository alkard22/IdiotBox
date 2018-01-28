using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour {

    public Color[] colors;
    public string[] labels;
    public GraphKeyLineItem rowPrefab;

    private void Start()
    {
        Draw();
    }

    public void Draw()
    {
        for (int i = 0; i < colors.Length; i ++)
        {
            GraphKeyLineItem row = Instantiate(rowPrefab);
            row.title = labels[i];
            row.color = colors[i];
            row.GetComponent<RectTransform>().SetParent(this.transform);
        }
    }
}
