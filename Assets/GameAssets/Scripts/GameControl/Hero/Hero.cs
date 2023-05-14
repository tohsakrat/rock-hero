using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour 
{	
	public float health;//剩余生命值

	[Header("Prefabs")]
	public GameObject deathParticleEffect;
	public GameObject healParticleEffect;

	[Header("视觉参数")]

	public Vector4 flashColor;//闪烁颜色
	public  Material mat;//主角材质



	[Header("主角状态*数值策划看这里")]
	//主角状态，用于初始化主角的各种状态，只是为了在unity编辑器中调试。如果要引用主角的实时状态，用Hero.r.BaseStatus.moveSpeed这种形式
		public float maxHealth;//生命上限
		public float attack;//攻击力
		public float BeatsPerMinute;//每分钟拍数
		public float attackRate;//攻击速度，每拍打几下
		public float moveSpeed;//移动速度
		public float pickupRadius;//拾起半径
		public float luck;//幸运值，影响掉落率
		public float healthRecover;//生命恢复
		public float healthSteal;//吸血率
		public float bulletSpeed;//子弹速度
		public float bulletRange ;//射程
		

		public enum heroState{
			//枚举属性，主角是否移动或者待机
			idle,
			move
		}

		heroState state=heroState.idle;

	[Header("设置")]
		public bool mouseMode=true;



	public status baseStatus;//基础状态
	public status customPoints;//自定义加点
	public status buffedStatus;//Buff和Debuff叠加之和
	public status currentStatus;//实时状态

	//控制变量
	
	public static Hero r;
	public bool canMove;
	[Header("临时变量")]
	public bool canHoldFire; //是否可以射击，用来阻塞技能
   	public float timer = 0;//计时器
   	public int beatsLeft = 0;//剩余拍数
	public  List<Skill> Skills= new List<Skill>();//持有技能列表
	public  List<Pickup> items= new List<Pickup>();//持有道具列表
	

	//待重构，这个数据不应该放在这里，鼠标位置
	public GameObject mousePositionWorld;

	public class status //主角状态类，用于存储主角的各种状态，比如移动速度，攻击力等等
	{	
		public float maxHealth;//生命上限
		public float attack;//攻击力
		public float BeatsPerMinute;//每分钟拍数
		public float attackRate;//攻击速度，每拍打几下
		public float moveSpeed;//移动速度
		public float pickupRadius;//拾起半径
		public float luck;//幸运值，影响掉落率
		public float healthRecover;//生命恢复
		public float healthSteal;//吸血率
		public float bulletSpeed;//子弹速度，是一个倍率，用于乘以具体子弹的速度
		public float bulletRange ;//射程

		//构造函数
		public status(float maxHealth,float attack,float BeatsPerMinute,float attackRate,float moveSpeed,float pickupRadius,float luck,float healthRecover,float healthSteal,float bulletSpeed,float bulletRange){
			this.maxHealth=maxHealth;
			this.attack=attack;
			this.BeatsPerMinute=BeatsPerMinute;
			this.attackRate=attackRate;
			this.moveSpeed=moveSpeed;
			this.pickupRadius=pickupRadius;
			this.luck=luck;
			this.healthRecover=healthRecover;
			this.healthSteal=healthSteal;
			this.bulletSpeed=bulletSpeed;
			this.bulletRange=bulletRange;
		}

		//重定义加法运算符
		public static status operator +(status a,status b){
			status c=new status(
				a.maxHealth+b.maxHealth,
				a.attack+b.attack,
				a.BeatsPerMinute+b.BeatsPerMinute,
				a.attackRate+b.attackRate,
				a.moveSpeed+b.moveSpeed,
				a.pickupRadius+b.pickupRadius,
				a.luck+b.luck,
				a.healthRecover+b.healthRecover,
				a.healthSteal+b.healthSteal,
				a.bulletSpeed+b.bulletSpeed,
				a.bulletRange+b.bulletRange
			);
			return c;
		}

		

	}


	void Start () { 

		//初始化状态
		baseStatus=new status(
			maxHealth,
			attack,
			BeatsPerMinute,
			attackRate,
			moveSpeed,
			pickupRadius,
			luck,
			healthRecover,
			healthSteal,
			bulletSpeed,
			bulletRange
		);
		customPoints=new status(0,0,0,0,0,0,0,0,0,0,0);
		buffedStatus=new status(0,0,0,0,0,0,0,0,0,0,0);



		r = this; 
		//技能列表，现在只是把技能注册表里的所有技能都加进来了
		//取SkillDic中每个值，加入到Skills列表中
		foreach (KeyValuePair<string, Skill> kvp in Regedit.r.SkillDic)
		{
			Skills.Add(kvp.Value);
		}
		//打印skill列表
		Debug.Log("主角skill列表");
		foreach (Skill s in Skills)
		{
			Debug.Log(s.name);
		}

	}

	void Update ()
	{	


		if(Game.g.gameActive){
		
		currentStatus=baseStatus+customPoints+buffedStatus;

		transform.position= new Vector3(transform.position.x,transform.position.y,0);

		if(canMove){
			//摇滚英雄不是飞机，不用掉头
			//RotateHero();

			movHero();
		}

		timer += Time.deltaTime;
		if(canHoldFire){
		
		if(
			timer>60f/BeatsPerMinute
			){
				
			timer=0;
			beatsLeft--;
			if(beatsLeft<=0)Shoot();
		}
		}


		}


		
	}


	/*--------------------------
			主角移动与旋转
	----------------------------*/

	void RotateHero ()
	{	Vector3 dirPosition = mousePositionWorld.transform.position;
		dirPosition.z=transform.position.z;
		transform.up = dirPosition - transform.position;
		//transform.eulerAngles += new Vector3(0, 0, (-angularSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime);
		
	}

	void movHero ()
	{	
		if(mouseMode){
			transform.position = Vector3.MoveTowards(transform.position, mousePositionWorld.transform.position, moveSpeed * Time.deltaTime);
			heroState state=heroState.move;
		}else{
			state=heroState.idle;
			//键盘模式
			if(Input.GetKey(KeyCode.W)){
				transform.position+=new Vector3(0,moveSpeed*Time.deltaTime,0);
				heroState state=heroState.move;
			}
			if(Input.GetKey(KeyCode.S)){
				transform.position+=new Vector3(0,-moveSpeed*Time.deltaTime,0);
				heroState state=heroState.move;
			}
			if(Input.GetKey(KeyCode.A)){
				transform.position+=new Vector3(-moveSpeed*Time.deltaTime,0,0);
				heroState state=heroState.move;
			}
			if(Input.GetKey(KeyCode.D)){
				transform.position+=new Vector3(moveSpeed*Time.deltaTime,0,0);
				heroState state=heroState.move;
			}

		}
		
	}

	/*--------------------------
			播放技能
	----------------------------*/
	public void Shoot ()
	{	
		//flashSr();
		//打印
		//Debug.Log("主角射击");
		//每次触发，随机tigger一个skill中的技能
		Skill s=Skills[Random.Range(0,Skills.Count)];
		//Debug.Log(s);
		s.trigger();
		beatsLeft=s.Beats;

	}



	//受到伤害
	public void TakeDamage (float dmg)
		{
			//闪避判定
			if(Random.Range(0.0f, 1.0f) < luck)
			{
				//Game.g.SpriteFlash(sr);
				return;
			}
			//If the health is less than or equal to 0, then end the game.
			if(health - dmg <= 0)
			{
				Game.g.EndGame();

				//Create the explosion particle effect.
				GameObject pe = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);
				Destroy(pe, 2.0f);
			}
			else
				health -= dmg;

			CameraController.c.Shake(0.3f, 0.5f, 50.0f);

			//Game.g.SpriteFlash(sr);
			getDmgAnimate ();
			UI.ui.SetPlanetHealthBarValue((int)health);
		}
	


	
	//阻塞射击
	public void ActivateSpeedFire () { if(!canHoldFire) StartCoroutine(SpeedFireTimer()); }

	IEnumerator SpeedFireTimer ()
	{
		canHoldFire = true;
		yield return new WaitForSeconds(5.0f);
		canHoldFire = false;
	}
		/*--------------------------
			各种动画
	----------------------------*/
	
	//治疗
	public void healAnimate ()
	{
		GameObject pe = Instantiate(healParticleEffect, transform.position, Quaternion.identity,Hero.r.transform);
		Destroy(pe, 2.0f);
		//一秒钟后执行flashSr
		Invoke("flashSr",0.5f);
		flashColor=new Vector4(1.1f,2f,1.1f,1.1f);

	}


	//受伤
		public void getDmgAnimate ()
	{

		//一秒钟后执行flashSr
		flashSr();
		flashColor=new Vector4(2f,2f,2f,2f);

	}

	//捡起


	public void flashSr(){
		
		//定义一个全1的vector4
		Vector4 v=new Vector4(1f,1f,1f,1f);
		Vector4 v1 = flashColor;
		//Debug.Log(mat.GetVector("_brightness"));
		//获取用户定义的shader input要加上下划线

		//如果当前亮度是全1的vector4，就设置为全1.5的vector4，否则设置为全1的vector4
		if(mat.GetVector("_brightness")==v){
			mat.SetVector("_brightness",v1);
			Invoke("flashSr",0.2f);//0.2秒钟后执行flashSr,把调高的亮度调回来
		}else{
			mat.SetVector("_brightness",v);
		}

	}

}


