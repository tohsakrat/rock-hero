using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGlissando : Bullet
{
	
	public float length = 10f;//子弹长度
	public float baseSize = 0.33333f;//贴图大小
	
	public float heroSize;//圆心大小
	public SpriteRenderer spriteRenderer;//子弹的渲染器
	
	public BoxCollider2D boxCollider;//碰撞体
	
	public ParticleSystem particleSystem;//粒子系统
	public Transform Target;//目标
	public float angle;//子弹的角度
	override public void Start ()
	{
		
		//扇形攻击有明确的路程:180deg，因此子弹生命周期为路程除以速度
		//用户在面板设置的lifeTime会失效
		lifeTime = 3.15f/bulletSpeed;
		Destroy(particleSystem.gameObject, lifeTime);//子弹生命时间到了就销毁
		Destroy(gameObject, lifeTime);//子弹生命时间到了就销毁
	}


	public void Awake(){

		
		//把x方向缩放设置为长度
		spriteRenderer.transform.localScale = new Vector3(length/baseSize,1,1);
		//把棍子头部置为原点
		spriteRenderer.transform.localPosition = new Vector3(transform.localPosition.x-length/2-heroSize,0,0);
		
		//把粒子系统的半径设置为长度的一半，并对齐
		var shape = particleSystem.shape;
		shape.radius = length/2;
		shape.position= new Vector3(transform.localPosition.x-length/2,0,0);
		
		

		//找到最近的敌人
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


		//计算子弹的方向（角度）
		Target = Regedit.Enemies[MinDis].gameObject.transform;//找到最近的敌人
		Vector3 dir = Regedit.Enemies[MinDis].transform.position - transform.position;
		angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
		//打印
		//Debug.Log("打印角度");
		//Debug.Log(angle);
		//把子弹旋转到这个方向
		transform.rotation = Quaternion.AngleAxis(angle-91,Vector3.forward);


		}
	}

	
	override public void moveRule(){
		//-----------------在这里写自己的逻辑--------------------
		//把棍子头部置为原点

		spriteRenderer.transform.localPosition = new Vector3(transform.localPosition.x-length/2-heroSize,0,0);

		bulletSpeed -= Time.deltaTime/lifeTime*bulletSpeed;//子弹速度随时间减小
		//打印敌人注册表长度
		//Debug.Log("打印敌人注册表长度");
		//Debug.Log(Regedit.Enemies.Count);
		
		transform.rotation = 
		
		Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle+91,Vector3.forward),Time.deltaTime*bulletSpeed);//子弹按照最初的方向运动
	

		}

	override  public void OnTriggerEnter2D (Collider2D col)
	{
		//真群攻不因为打中敌人而消失
		//如果打中敌人，就造成伤害
		if(col.gameObject.tag == "Enemy")
		{
			col.gameObject.GetComponent<EnemyCollider>().root.TakeDamage(damage);
			//SpawnParticleEffect();
		}

		//如果打中盾牌，就造成伤害
		else if(col.gameObject.tag == "Shield")
		{
			col.gameObject.GetComponent<ShipShield>().ShieldHit();
			SpawnParticleEffect();
		}

		//如果打中道具，就造成伤害
		else if(col.gameObject.tag == "Pickup")
		{
			col.gameObject.GetComponent<Pickup>().TakeDamage(damage);
			SpawnParticleEffect();
		}

	}



	}




