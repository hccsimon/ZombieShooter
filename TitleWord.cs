using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleWord : MonoBehaviour
{
    public RawImage image;


    void Update()
    {
        var r = image.color.r;
        var g = image.color.g;
        var b = image.color.b;
        var alpha = Mathf.PingPong(Time.time / 3f, 1);
        image.color = new Color(r, g, b, alpha);
    }
}
