using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMatChange : MonoBehaviour
{
    public Material Clear;
    public Material Mirror;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetMatClear()
    {
        GetComponent<Renderer>().material = Clear;
    }

    public void SetMatMirror()
    {
        GetComponent<Renderer>().material = Mirror;
    }
}
