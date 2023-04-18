using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    public bool isinterdown,isinterup;
    
    public GameObject pare_player;//父对象
    public GameObject son_myself;//子对象


    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       //if(playerMove.instance.isTouch)
       // {
           
       // }
       // Hold_pull();
    }
   
    //void Hold_pull()
    //{
    //    isinterdown = Input.GetButtonDown("Jump");
    //    isinterup = Input.GetButtonUp("Jump");

    //    if (isinterdown)
    //    {

    //        GameObject a = Instantiate(son_myself, transform.position, Quaternion.identity);//实例化物体
    //        a.transform.parent = pare_player.transform;//把实例化的物体放到父物体player之下
    //    }
    //    if (isinterup)
    //    {
    //        //把物体从父物体player之下拿出来
    //    }
    //}
}
