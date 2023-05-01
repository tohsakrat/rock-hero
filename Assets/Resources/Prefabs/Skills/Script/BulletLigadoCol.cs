using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLigadoSR : Bullet
{
	public Bullet father;
	public void Awake(){
		damage=father.damage;
		bulletSpeed=father.bulletSpeed;
		lifeTime=father.lifeTime;
		hitParticleEffect=father.hitParticleEffect;
	}

		override public void Start ()
	{
		
	}

	override  public void OnTriggerEnter2D (Collider2D col)
	{
		//Debug.Log("子弹碰撞到了");
		//Debug.Log(col);
		//如果打中敌人，就造成伤害

		father.OnTriggerEnter2D(col);
	}

	}




