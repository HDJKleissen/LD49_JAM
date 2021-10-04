using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutPanel : MonoBehaviour
{
    float fade = 1;
    public Image panel;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fade -= Time.deltaTime * 0.33f;
        panel.color = new Color(0, 0, 0, fade);
            if(fade <= 0)
        {
            Destroy(canvas);
        }
    }
}
