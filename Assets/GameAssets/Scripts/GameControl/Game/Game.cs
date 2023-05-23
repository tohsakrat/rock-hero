using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour 
{
	public float gameTime;
	public float gameTimeHighscore;
	public bool gameActive;
	public bool Debuging;

	public GameObject planetShield;
	public GameObject turret;
	public GameObject Boss1;
	
	public static Game g;

	public bool isBoss;
	

	void Awake ()
	{
		
		g = this;
		//Setting stuff up so the camera is set to menu view.
		//CameraController.c.SetMenuView();
	}
	void Start()
	{
		UI.ui.SetMenuUI();
		UI.ui.gameUI.SetActive(false);

	//Getting the highscore.
		gameTimeHighscore = PlayerPrefs.GetFloat("Highscore");
	} 


	void Update ()
	{

		if(gameActive)
			gameTime += Time.deltaTime;

		//游戏生存时间超过一分钟，进入boss战，对所有场上敌人造成9999伤害清场
		
		if(gameTime >  120 && !isBoss)
		{	
			g.gameActive = false;

			EnemySpawner.s.maxEnemies=0;
			isBoss = true;

			//对所有场上敌人造成9999伤害清场
			foreach(Enemy enemy in Regedit.r.EnemiesParent.GetComponentsInChildren<Enemy>())
			{
				enemy.TakeDamage(9999);
			}
			AudioManager.am.PlayBoss();
			//boss出生
			Boss1 = Instantiate(Boss1, new Vector3(0, 0, 0), Quaternion.identity, Regedit.r.EnemiesParent.transform);
			
			//镜头移动到boss
			//(Vector4 pos,float speed =25, MyDelegateVoid callbacK = null)
			CameraController.c.closeUpShot(new Vector4(0, 0, 0, 0), 25,2,()=>{g.gameActive = true;});



		}





		
		//enemy父母对象下的所有子对象为Regedir.r.EnemiesParent.transform.GetComponentsInChildren<Transform>(true)
		List <Transform> enemies = new List<Transform>(Regedit.r.EnemiesParent.transform.GetComponentsInChildren<Transform>(true));
		//把enemies按照y轴排序
		enemies.Sort(delegate(Transform a, Transform b)
		{
			return (a.position.y).CompareTo(b.position.y);
		});

		//把enemies在hierarchy面板中的顺序按照y轴排序
		for(int i = 0; i < enemies.Count; i++)
		{
			enemies[i].SetSiblingIndex(i);
		}

		
	}

	//Called when the game begins.
	public void StartGame ()
	{
		gameActive = true;
		gameTime = 0.0f;
		Hero.r.canMove = true;
		UI.ui.SetGameUI();
		//Camera.main.GetComponent<CameraController>().TransitionToGameView(Game.g.StartGame);
		
		AudioManager.am.PlayNormal ();
	}

	//Called when the planet's health reaches 0. Ends the game.
	public void EndGame ()
	{
		if(gameActive)
		{
			gameActive = false;
			Hero.r.canMove = false;
			UI.ui.gameUI.SetActive(false);
			UI.ui.SetGameOverUI();

			//Disabling the planet and rocket sprites.
			//Hero.r.rocketSprite.gameObject.SetActive(false);
			//如果主角生命大于0
			if(Hero.r.health > 0)
			{
				//如果游戏时间大于最高纪录
			//朝着主角放大镜头
				CameraController.c.closeUpShot(new Vector4(Hero.r.transform.position.x, Hero.r.transform.position.y, 0, 0), 25,5,()=>{g.gameActive = true;});
					AudioManager.am.PlayWin();
				
			}else{
				Hero.r.gameObject.SetActive(false);

					AudioManager.am.PlayLose();
			}
			
		}
	}



	//Sets the gameTime as the highscore.
	public void SetTimeAsHighscore ()
	{
		PlayerPrefs.SetFloat("Highscore", gameTime);
	}

	//Calls a coroutine to flash the sent sprite renderer white.
	public void SpriteFlash (SpriteRenderer sr)
	{
		StartCoroutine(sf(sr));
	}

	//Flashes the sent sprite renderer white for 0.05 seconds.
	IEnumerator sf (SpriteRenderer sr)
	{
		if(sr.color != Color.white)
		{
			Color defaultColour = sr.color;
			sr.color = Color.white;

			yield return new WaitForSeconds(0.05f);

			if(sr != null)
				sr.color = defaultColour;
		}
	}

	//Calls a coroutine to stun the sent enemy.
	public void Stun (Enemy enemy)
	{
		StartCoroutine(st(enemy));
	}

	//Stuns the sent enemy for 0.02 seconds.
	IEnumerator st (Enemy enemy)
	{
		enemy.stunned = true;

		yield return new WaitForSeconds(0.02f);

		if(enemy != null)
			enemy.stunned = false;
	}

	//Enables the planet shield for a few seconds.
	public void SetTempPlanetShield ()
	{
		planetShield.SetActive(true);
		StartCoroutine(ps());
	}

	IEnumerator ps ()
	{
		yield return new WaitForSeconds(8.0f);
		planetShield.SetActive(false);
	}

	

	//Enables the turret object.
	public void SetTurret ()
	{
		if(!turret.activeInHierarchy)
		{
			turret.SetActive(true);
			turret.transform.eulerAngles = Vector3.zero;
		}
	}
}
