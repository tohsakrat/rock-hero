using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public int damage; //子弹伤害
	public GameObject hitParticleEffect;//击中特效
	public GameObject anchor;//击中特效
	public float bulletSpeed;//子弹速度
	public float lifeTime;//子弹生命时间

	virtual public void Start ()
	{
		Destroy(gameObject, lifeTime);//子弹生命时间到了就销毁
		bulletSpeed=bulletSpeed*Hero.r.currentStatus.bulletSpeed;//子弹速度随英雄属性变化
	}

	public void  Update(){
		//不同于start和awake，因为涉及到时序，不采用c#的base直接继承，而是规定好生命周期，然后在子类中对不同生命周期对应的方法进行重写，这样就可以实现多态了。
		//如果子弹对象未激活，则不执行
		if(!gameObject.activeSelf)return;
		//如果子弹对象激活，则执行以下代码
		if(Game.g.gameActive){
			//子弹移动
			 moveRule();
		}
		if(anchor!=null)transform.position=anchor.transform.position;
		
	}

	virtual public void  moveRule(){
		//子弹移动规则
		transform.position += transform.forward * bulletSpeed * Time.deltaTime;
	}



	virtual  public void OnTriggerEnter2D (Collider2D col)
	{
		//如果打中敌人，就造成伤害
		if(col.gameObject.tag == "Enemy")
		{
			
			col.gameObject.GetComponent<EnemyCollider>().root.TakeDamage(damage);
			//获取碰撞位置，并将2d碰撞点转换为z为0的vetror3
        	//Vector3 hitPoint = col.contacts[0].point;
			//hitPoint.z = 0;
			SpawnParticleEffect();
			Destroy(gameObject);
		}

		//如果打中盾牌，就造成伤害
		else if(col.gameObject.tag == "Shield")
		{
			col.gameObject.GetComponent<ShipShield>().ShieldHit();
			SpawnParticleEffect();
			Destroy(gameObject);
		}

		//如果打中道具，就造成伤害
		else if(col.gameObject.tag == "Pickup")
		{
			col.gameObject.GetComponent<Pickup>().TakeDamage(damage);
			SpawnParticleEffect();
			Destroy(gameObject);
		}
	}






	virtual  public void SpawnParticleEffect ()
	{
		GameObject pe = Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
		pe.transform.LookAt(Hero.r.transform);
		Destroy(pe, 2.0f);
	}


}
