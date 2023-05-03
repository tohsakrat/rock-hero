using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFriedFish : Enemy
{	    
    public GameObject duplicatePrefab;//敌方导弹（实为本对象下隐藏对象，不单独保存为新的预制体）
    public float ShootSpan;//射击间隔

    
    void Start(){
       if(name=="")name="EnemyFriedFish";//如果没有名字，就给一个名字
       base.Start();
    }
    
    override public void moveRule(){
        /*
        //间隔时间到了就发射导弹（把样本复制一份）
        if(Time.time>lastShootTime+ShootSpan){
            lastShootTime=Time.time;
            GameObject duplicate = Instantiate(duplicatePrefab,duplicatePrefab.transform.position,duplicatePrefab.transform.rotation,Regedit.r.EnemiesParent);
            duplicate.getComponent<FlyingFish>().isPrefab=false;
            duplicate.SetActive(true);
        }
        */

        base.moveRule();

    }





   
}

