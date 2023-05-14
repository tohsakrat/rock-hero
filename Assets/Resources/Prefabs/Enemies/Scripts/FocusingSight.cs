using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusingSight : Enemy
{	 
    public GameObject father;
    void Start(){ 
       if(name=="")this.name="FocuingSight";//如果没有名字，就给一个名字
        base.Start();
    }
    override public void moveRule(){
        if(father==null){
            Destroy(gameObject);
        }
        base.moveRule();
    }


}

