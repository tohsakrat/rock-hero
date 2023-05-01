using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLigado : Bullet
{
	
	
	public float heroSize;//圆心大小
	
	public float r;//射程（外半径）
	
	public CircleCollider2D circleCollider;//子碰撞体
	
	public drawCircle circle1;
	public drawCircle circle2;

	float totalTime;//总时间
	public float r3;
	override public void Start ()
	{
		
		//扇形攻击有明确的路程:r-heroSize，因此子弹生命周期为路程除以速度
		//用户在面板设置的lifeTime会失效！！
		r3=heroSize;
		lifeTime = (r-heroSize)/bulletSpeed;

		Destroy(gameObject, lifeTime);//子弹生命时间到了就销毁
	}


	public void Awake(){
		circle1.R=heroSize;
		circle2.R=heroSize;
	}

	
	override public void moveRule(){
		totalTime+=Time.deltaTime;
		//Debug.Log("moveRule");
		bulletSpeed -= Time.deltaTime/lifeTime*bulletSpeed;//子弹速度随时间减小
		//圆圈的缩放规则
		

		if(totalTime<lifeTime/1.5){
			r3=Mathf.Lerp(r3,r,Time.deltaTime*bulletSpeed*0.2f);
		}else{ 
			if(totalTime<lifeTime/1.2){
			r3=Mathf.Lerp(r3,r,Time.deltaTime*bulletSpeed*1f);
			}else{
				r3=Mathf.Lerp(r3,r,Time.deltaTime*bulletSpeed*10f);
			}
		}

		if(totalTime<lifeTime/2){
			circle2.R=Mathf.Lerp(circle2.R,r*1.1f,Time.deltaTime*bulletSpeed*0.25f);
		}else{
			circle2.R=Mathf.Lerp(circle2.R,r*1.1f,Time.deltaTime*bulletSpeed*2f);
		}
		circle2.width=circle2.R-r3;

		circle1.R=Mathf.Lerp(circle1.R,r,Time.deltaTime*bulletSpeed*4);
		//circle1转圈
		circle1.transform.Rotate(0,0,Time.deltaTime*bulletSpeed);
		circleCollider.radius=circle2.R;


	}


	override  public void OnTriggerEnter2D (Collider2D col)
	{
		//真群攻不因为打中敌人而消失

		//如果打中敌人，就造成伤害
		if(col.gameObject.tag == "Enemy")
		{
			col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
			SpawnParticleEffect();
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




