using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLayEgg : Bullet
{	public Vector3 baseSpeed;
	override public void Start(){
		base.Start();
		baseSpeed = baseSpeed * Hero.r.currentStatus.bulletSpeed;
	}
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
			else{
				//如果敌人还活着，就计算距离
				//如果敌人不能被锁定，跳过
				if(!Regedit.Enemies[i].canFocus)continue;
				Regedit.Enemies[i].dis = Vector3.Distance(
					Regedit.Enemies[i].transform.position,transform.position);
				if(Regedit.Enemies[i].dis<Regedit.Enemies[MinDis].dis)MinDis=i;
				}
		}
		
		baseSpeed -= Time.deltaTime/lifeTime*10 *baseSpeed;//子弹初速度随时间减小
		transform.position =baseSpeed * Time.deltaTime+
		Vector3.MoveTowards(transform.position, Regedit.Enemies[MinDis].transform.position, bulletSpeed * Time.deltaTime);//子弹追踪最近敌人
		//头旋转向敌人
		Vector3 dir = Regedit.Enemies[MinDis].transform.position - transform.position;
		//后天旋转
		float angle1 = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
		//初速度旋转
		float angle2 = Mathf.Atan2(baseSpeed.y,baseSpeed.x)*Mathf.Rad2Deg;
		//加权平均
		float angle = (angle1*bulletSpeed+angle2*baseSpeed.magnitude)/(baseSpeed.magnitude+bulletSpeed);
		transform.rotation = Quaternion.AngleAxis(angle-91,Vector3.forward);

		
		

		}



	}



}
