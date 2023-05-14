using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBaseShoot : Bullet
{
	public Vector3 Target;//目标
	public void Awake(){
		//找到最近的敌人，为自己施加初速度
		//Debug.Log("打印敌人注册表长度");
		//Debug.Log(Regedit.Enemies.Count);
		if(Regedit.Enemies.Count>0){
		int MinDis=0;
		for(int i =0;i<Regedit.Enemies.Count;i++){
			if(Regedit.Enemies[i]==null){
				Regedit.Enemies.RemoveAt(i);
			}
			else{//如果敌人还活着，就计算距离
				
				if(!Regedit.Enemies[i].canFocus)continue;
				Regedit.Enemies[i].dis = Vector3.Distance(
					Regedit.Enemies[i].transform.position,transform.position);
				if(Regedit.Enemies[i].dis<Regedit.Enemies[MinDis].dis)MinDis=i;
				}
		}
		Target = Regedit.Enemies[MinDis].gameObject.transform.position;//找到最近的敌人

		}
	}
	override public void moveRule(){

		//打印敌人注册表长度
		
		
		transform.position = 

		Vector3.MoveTowards(transform.position, Target, Time.deltaTime*bulletSpeed);//子弹按照最初的方向运动
		//

		}



	}




