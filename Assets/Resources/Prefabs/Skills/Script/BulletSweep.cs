using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSweep : Bullet
{
	public Vector3 v;
	
	
	override public void moveRule(){

	transform.position = transform.position + v*Time.deltaTime*bulletSpeed;//子弹按照最初的方向运动

	}

	public void setSpeed(Vector3 v1){
		//为了避免面板误操作，设置速度单独封装一个方法
		this.v=v1;
	}

}






