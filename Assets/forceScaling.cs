using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceScaling : MonoBehaviour
{   
    public float referWidth;
    public float referHeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.localScale=new Vector3(Screen.width/referWidth,Screen.height/referHeight,1);
       // GetComponent<RectTransform>().sizeDelta=new Vector2( referWidth*referWidth/Screen.width,referHeight*referHeight/Screen.height);
    }
}
