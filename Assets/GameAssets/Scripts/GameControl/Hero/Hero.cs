using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour 
{	
	public int health;//生命值
	//Prefabs
	public GameObject deathParticleEffect;
	//Components
	public SpriteRenderer sr;//贴图
	public float moveSpeed;//移动速度
	public float bulletSpeed;//子弹速度
	
	public float attack;//攻击力
	public float bulletSpread = 1.0f;
	public bool canMove;
	public bool canHoldFire; //是否可以射击
   	public float timer = 0;//计时器
	public static List<Skill> Skills= new List<Skill>();//技能列表
	
	public static Hero r;
	//Prefabs
	//public GameObject Camera;
	public GameObject mousePositionWorld;
	void Start () { 
		r = this; 
		//技能列表，现在只是把技能注册表里的所有技能都加进来了
		//取SkillDic中每个值，加入到Skills列表中
		foreach (KeyValuePair<string, Skill> kvp in Regedit.SkillDic)
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
		
		transform.position= new Vector3(transform.position.x,transform.position.y,0);

		if(canMove){
			RotateHero();
			movHero();
		}

		timer += Time.deltaTime;
		if(canHoldFire){
		
		if(
			timer>60f/Game.g.BeatsPerMinute/Game.g.attackRate//每拍打几下
			){
				
			timer=0;
			Shoot();
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
	{	transform.position = Vector3.MoveTowards(transform.position, mousePositionWorld.transform.position, moveSpeed * Time.deltaTime);
		//transform.position += new Vector3(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime,   moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0);
		//rocketSprite.localEulerAngles = new Vector3(0, 0, Input.GetAxis("Horizontal") * -30);
	}

	/*--------------------------
			播放技能
	----------------------------*/
	public void Shoot ()
	{
		//每次触发，随机tigger一个skill中的技能
		Skills[Random.Range(0,Skills.Count)].trigger();

	}



	//受到伤害
	public void TakeDamage (int dmg)
		{
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
			Game.g.SpriteFlash(sr);
			UI.ui.SetPlanetHealthBarValue(health);
		}
	//EFFECTS

	//Allows the player to hold down the fire button.
	public void ActivateSpeedFire () { if(!canHoldFire) StartCoroutine(SpeedFireTimer()); }

	IEnumerator SpeedFireTimer ()
	{
		canHoldFire = true;
		yield return new WaitForSeconds(5.0f);
		canHoldFire = false;
	}


}
