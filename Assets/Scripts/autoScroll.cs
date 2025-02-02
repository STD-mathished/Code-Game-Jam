using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auto_Scroll : MonoBehaviour
{
    private float _speed=1f;
    Vector2 offset;
    

    // Update is called once per frame
    private void Update()
    {
        offset = new Vector2(0,Time.time * _speed) ;
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
