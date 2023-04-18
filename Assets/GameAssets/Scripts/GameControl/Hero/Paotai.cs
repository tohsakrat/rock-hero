using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paotai : MonoBehaviour
{
    public Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = mousePosition;
        mouseWorldPosition.z = Mathf.Abs(Camera.main.transform.position.z);
        mouseWorldPosition = Camera.main.ScreenToWorldPoint (mouseWorldPosition);
        //此处我用上方为指向方向,可以改成别的方向.
        transform.up = mouseWorldPosition - transform.position;
    }
}
