using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    public Transform anchor;
    public Quaternion offset;
    // Start is called before the first frame update
    void Start()
    {
        offset=transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        //在offset基础上，朝anchor反向旋转
        transform.rotation=Quaternion.Inverse(anchor.rotation);
    }
}
     