using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPosition : MonoBehaviour
{
    public Transform anchor;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset=transform.position-anchor.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position=anchor.position+offset;
    }
}
     