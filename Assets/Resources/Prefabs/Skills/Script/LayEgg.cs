using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayEgg : Skill
{   
    public Vector3 baseSpeed;
    public void Start(){
         
       if(name=="")this.name="LayEgg";//如果没有名字，就给一个名字
        //在这里写自己的初始化逻辑
        //调用父类的Start方法
       base.Start();
    }

    override public void trigger(){
        

        GameObject bullet1 = Instantiate(bulletPrefab, Hero.r.transform.position, Hero.r.transform.rotation);

        bullet1.SetActive(true);

        bullet1.GetComponent< BulletLayEgg>().baseSpeed = baseSpeed;
        GameObject bullet2 = Instantiate(bulletPrefab, Hero.r.transform.position, Hero.r.transform.rotation);
        bullet2.GetComponent< BulletLayEgg>().baseSpeed = -baseSpeed;
        bullet2.SetActive(true);

}

}