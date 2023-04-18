using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setZToZero : MonoBehaviour
{   public RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   Debug.Log(rectTransform.position);
        rectTransform.position= new Vector3(rectTransform.position.x,rectTransform.position.y,0);
    }
}
 