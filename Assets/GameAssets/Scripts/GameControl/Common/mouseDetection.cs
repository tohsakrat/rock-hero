using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseDetection : MonoBehaviour
{
    public static Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { if(Game.g.gameActive){
        float step = 10 * Time.deltaTime;  
         
           // print(Camera.main.ScreenToWorldPoint(Input.mousePosition));

           // gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition), step); 
           Vector3 dirPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           dirPosition.z=0;
           //Debug.Log(dirPosition);
            pos = dirPosition;
           this.transform.position =   dirPosition;
           // this.transform.position.z =   0;
    }
    }
}
