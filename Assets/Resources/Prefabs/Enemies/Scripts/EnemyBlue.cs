using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : Enemy
{	    
    public GameObject duplicatePrefab;//解体后的怪物预制体
    
    void Start(){
       if(name=="")name="EnemyBlue";//如果没有名字，就给一个名字
       base.Start();
    }
    
    override public void Die()
    {
        //在这里写自己的死亡逻辑
        //蓝色怪解体
        Duplicate();

        //调用父类的Die方法
        base.Die();
   }



   	void Duplicate ()
	{   
        Debug.Log("Duplicate");
		GameObject e1 = Instantiate(duplicatePrefab, transform.position + (transform.up * -2), Quaternion.identity,Regedit.r.EnemiesParent);
		GameObject e2 = Instantiate(duplicatePrefab, transform.position + (transform.right * 2),  Quaternion.identity,Regedit.r.EnemiesParent);
		GameObject e3 = Instantiate(duplicatePrefab, transform.position + (transform.right * -2), Quaternion.identity,Regedit.r.EnemiesParent);
	}

   
}

