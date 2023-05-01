using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ligado : Skill
{
    public void Start(){
         
       if(name=="")this.name="Ligado";//如果没有名字，就给一个名字
        //在这里写自己的初始化逻辑
        //调用父类的Start方法
       base.Start();
    }

    override public void trigger(){
        
         //主动释放真群攻，跟随主角

        GameObject bullet = Instantiate(bulletPrefab, Hero.r.transform.position, Hero.r.transform.rotation,Hero.r.transform);

        bullet.SetActive(true);

            
        }

}
