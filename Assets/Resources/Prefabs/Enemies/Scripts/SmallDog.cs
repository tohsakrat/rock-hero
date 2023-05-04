using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDog : Enemy
{	  void Start(){
       if(name=="")this.name="EnemySmallDog";//如果没有名字，就给一个名字
        base.Start();
    }

    public override void collision(Collision2D col)
    {
        Debug.Log("SmallDog collision");
        base.collision(col);
    }


}

