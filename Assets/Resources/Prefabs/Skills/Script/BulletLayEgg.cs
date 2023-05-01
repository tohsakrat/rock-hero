using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLayEgg : Bullet
{	public Vector3 baseSpeed;

	override public void moveRule(){
		//-----------------在这里写自己的逻辑--------------------

		bulletSpeed -= Time.deltaTime/lifeTime*bulletSpeed;//子弹速度随时间减小
		//打印敌人注册表长度
		//Debug.Log("打印敌人注册表长度");
		//Debug.Log(Regedit.Enemies.Count);
		if(Regedit.Enemies.Count>0){
		int MinDis=0;
		for(int i =0;i<Regedit.Enemies.Count;i++){
			if(Regedit.Enemies[i]==null){
				Regedit.Enemies.RemoveAt(i);
			}
			else{//如果敌人还活着，就计算距离
				Regedit.Enemies[i].dis = Vector3.Distance(
					Regedit.Enemies[i].transform.position,transform.position);
				if(Regedit.Enemies[i].dis<Regedit.Enemies[MinDis].dis)MinDis=i;
				}
		}
		
		baseSpeed -= Time.deltaTime/lifeTime*baseSpeed;//子弹初速度随时间减小
		transform.position =baseSpeed * Time.deltaTime+
		Vector3.MoveTowards(transform.position, Regedit.Enemies[MinDis].transform.position, bulletSpeed * Time.deltaTime);//子弹追踪最近敌人
		//

		}



	}



}
