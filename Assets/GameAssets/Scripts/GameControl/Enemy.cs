﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	/*敌人母类*/
	public string name = "";//敌人名字
	public int health = 5;//敌人血量
	public float moveSpeed = 10;//敌人移动速度
	public int damage = 5;//敌人伤害
	public bool stunned = false;//敌人是否被眩晕
	public GameObject deathParticleEffect;//死亡粒子效果
	public SpriteRenderer sr;//本敌人贴图
	public float dis;//临时变量，用于记录敌人的距离
	public float dropRate = .5f;//掉落率

	public virtual void Start(){
		Regedit.Enemies.Add(this);//注册当前敌人到全局list中
		if(sr==null)sr=gameObject.GetComponent<SpriteRenderer>();//如果贴图未绑定，则绑定自己
	}

	virtual public void Awake(){
		//如果没起名，就用对象名作为名字
		if(name == "")name = gameObject.name;
	}

	public void Update ()
	{	
		//不同于start和awake，因为涉及到时序，不采用c#的base直接继承，而是规定好生命周期，然后在子类中对不同生命周期对应的方法进行重写，这样就可以实现多态了。
		//如果未激活，直接返回
		if(!gameObject.activeSelf)return;
		if(dieRule())Die();//如果血量小于等于0，就死亡
		if(!stunned && Game.g.gameActive){
			//如果没有被眩晕，就移动
			 moveRule();
		}

	}

	virtual public bool dieRule(){
		//死亡规则，每个敌人都有自己的死亡规则,可以对这个方法进行拓展
		return (health<=0) ;
	}

	virtual public void moveRule(){
		//移动规则，每个敌人都有自己的移动规则,可以对这个方法进行拓展
		transform.position = Vector3.MoveTowards(transform.position, Hero.r.transform.position, moveSpeed * Time.deltaTime);//移动到玩家的位置	
		LookAtHero();//始终面向玩家
	}

	virtual public void LookAtHero ()
	{	//使敌人始终面向玩家。
		Vector3 dir = transform.position.normalized;
		float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
		if( moveSpeed==0)ang=0;
		transform.eulerAngles = new Vector3(0, 0, ang);
	}

	
	virtual public void TakeDamage (int dmg)
	{
		//当子弹击中敌人时调用
		if(health - dmg <= 0)
			Die();
		else
		{	health-=dmg;
			new Vector3(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime,   moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0);;
			Game.g.Stun(this);
			AudioManager.am.PlayEnemyHit();
		}

		Game.g.SpriteFlash(sr);//贴图闪烁
	}



	
	virtual public void Die ()
	{
		//当敌人血量小于等于0时即死亡时调用
		GameObject pe = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);
		Destroy(pe, 2.0f);
		if(dropRate > Random.Range(0f, 1f))Instantiate( Pickup.GenerateRandomPickup(), transform.position, Quaternion.identity);//掉落物品，先随便写，回来改
		AudioManager.am.PlayEnemyDeath();//播放敌人死亡音效
		Regedit.Enemies.Remove(this);//从全局list中移除
		Destroy(gameObject);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		//当敌人碰到玩家时调用
		if(col.gameObject.tag == "Hero")
		{
				Hero.r.TakeDamage(damage);
				health -= 9999 ;
				Debug.Log("撞到主角");
		}
	}


}
