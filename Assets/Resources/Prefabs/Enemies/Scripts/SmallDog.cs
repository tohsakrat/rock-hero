using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDog : Enemy
{	  void Start(){
       if(name=="")this.name="EnemyRed";//如果没有名字，就给一个名字
        base.Start();
    }



}

