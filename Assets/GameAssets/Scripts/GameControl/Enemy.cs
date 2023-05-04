using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	/*敌人母类*/

	[Header("怪物基本属性")]
	public string name = "";//敌人名字


	
	[Header("怪物四维")]
	public float health = 100;//敌人血量
	public float damage = 5;//敌人伤害（创人）
	public float moveSpeed = 10;//敌人移动速度
	public float defense = 1;//敌人护甲

	
	[Header("怪物设置")]
	public float angleSpeed = 10;//敌人移动速度
	public bool lockAngle;//是否锁定左右角度
	public bool isBoundary;//是否是伪装成障碍物的敌人发对象
	public bool canFocus=true;//是否能被自瞄技能锁定
	public float dropRate = .5f;//掉落率
	public GameObject deathParticleEffect;//死亡粒子效果
	public GameObject bodyTexture;//贴图所在子对象
	public float angleOffset;//子对象角度偏移
	public GameObject colliderTriger;//实际触发事件的碰撞体,一般是子对象



	
	[Header("临时变量")]
	public bool stunned = false;//敌人是否被眩晕
	public bool isKilled = false;//敌人是否被击杀
	public float angle;

	//public SpriteRenderer sr;//本敌人贴图
	public float dis;//临时变量，用于记录敌人的距离

	public virtual void Start(){
		if(deathParticleEffect==null)deathParticleEffect=Regedit.r.deathParticleEffect;
		Regedit.Enemies.Add(this);//注册当前敌人到全局list中
		//if(sr==null)sr=gameObject.GetComponent<SpriteRenderer>();//如果贴图未绑定，则绑定自己

		//如果colliderTigger没有绑定，则绑定自己
		if(colliderTriger==null)colliderTriger=gameObject;
		//为enermyTrigger添加EnemyCollider组件，并且把自己赋值给root
		colliderTriger.AddComponent<EnemyCollider>().root = this;
		

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

		if(!isKilled && dieRule())Die();//如果血量小于等于0，就死亡
		if(isBoundary)return;//如果是伪装成障碍物的敌人发对象，直接返回

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
	

		//Debug.Log("常规移动");
		
	}

	virtual public void LateUpdate()
	{
		if(lockAngle){
			LookAtHeroLR ();//仅左右面向玩家
		}else{
			LookAtHeroAngle();//始终面向玩家
		}
		
		//this.transform.rotation=anchor.transform.rotation;
	}

	virtual public float LookAtHeroAngle ( )
	{	//使敌人始终面向玩家。
		//offset是偏移量，可以用来调整敌人的面向角度
		//按照angleSpeed的速度旋转
		

		Vector3 dir = Hero.r.transform.position - transform.position;
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+ angleOffset;
		if(transform.localScale.x<0)angle=180-angle;
		if(angle<0)angle=360+angle;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * angleSpeed);

		//当敌人旋转超过一百八十度，翻转，否则不翻转
		//Debug.Log(angle-angleOffset);
		
		//判断angle值是否处在第二象限或第三象限
	
		
		if((angle > 90 && angle<270 ) && transform.localScale.y > 0)transform.localScale = 
		new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
		
		if(( ((angle>0 && angle < 90 ) || angle>270)  ) && transform.localScale.y < 0)transform.localScale = 
		new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
		
		LookAtHeroLR ();
		return angle;
	}

	virtual public void LookAtHeroLR ()
	{	
		
		//当敌人在玩家左侧，水平翻转当前组件，否则不翻转
		if(transform.position.x < Hero.r.transform.position.x && transform.localScale.x > 0)transform.localScale = 
		new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		
		if(transform.position.x > Hero.r.transform.position.x && transform.localScale.x < 0)transform.localScale = 
		new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		

		
	}
	
	virtual public void TakeDamage (float dmg)
	{


			Game.g.Stun(this);
			AudioManager.am.PlayEnemyHit();
			health -= calculateDamage(dmg);


		//Game.g.SpriteFlash(sr);//贴图闪烁
	}


	virtual public float calculateDamage(float dmg){
		//计算伤害，每个敌人都有自己的计算伤害方法，可以对这个方法进行拓展
		//需要数值策划来调整计算公式。
		dmg= Hero.r.attack+dmg-defense;
		if(dmg<0)dmg=0;
		return dmg;
	}
	
	virtual public void Die ()
	{
		isKilled=true;
		//当敌人血量小于等于0时即死亡时调用
		GameObject pe = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);
		Destroy(pe, 2.0f);
		if(dropRate > Random.Range(0f, 1f))Instantiate( Pickup.GenerateRandomPickup(), transform.position, Quaternion.identity,Regedit.r.ViewLayer);//掉落物品，先随便写，回来改
		AudioManager.am.PlayEnemyDeath();//播放敌人死亡音效
		Regedit.Enemies.Remove(this);//从全局list中移除
		Destroy(gameObject);
	}

	virtual public void collision (Collision2D col)
	{
		//因为这个脚本必须绑定在体积碰撞盒，而实际碰撞事件由更精细的碰撞体来完成
		//所以将碰撞事件从更精细的碰撞体向上传递到这里，而不写在本物体的碰撞事件中
		//当敌人碰到玩家时调用
		if(col.gameObject.tag == "Hero")
		{
				Hero.r.TakeDamage(damage);
				health -= 9999f ;
				Debug.Log("撞到主角");
		}
	}


}
