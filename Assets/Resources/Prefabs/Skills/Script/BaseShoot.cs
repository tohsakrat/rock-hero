using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : Skill
{
    public void Start(){
         
       if(name=="")this.name="BaseShoot";//如果没有名字，就给一个名字
        //在这里写自己的初始化逻辑
        //调用父类的Start方法
       base.Start();
    }

}
