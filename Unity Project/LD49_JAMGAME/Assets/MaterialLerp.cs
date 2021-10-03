using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialLerp : MonoBehaviour
{
    public Material material;
    public float duration = 2.0f;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = material;
    }

    void Update()
    {
        // ping-pong between the materials over the duration
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.SetFloat("_Blend", lerp);
        Debug.Log(lerp);
    }
}
