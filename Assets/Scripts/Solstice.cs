using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solstice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(-1545f,0,1545f),Vector3.right, 0.1f*Time.deltaTime);
    }
}
