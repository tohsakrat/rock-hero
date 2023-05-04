using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour 
{
	public GameObject menuUI;
	public GameObject settingUI;
	public GameObject DisplaySettingUI;
	public GameObject SoundSettingUI;
	public GameObject ControlSettingUI;
	public GameObject CreditsUI;
	public GameObject gameUI;
	public GameObject gameOverUI;
	public Game gameCtrl;

	//Game UI
	public Text timeElapsed;
	public Slider planetHealthBar;

	//Game Over UI
	public Text goDefendTime;
	public Text goHighscoreTime;

	public static UI ui;

	void Awake () { ui = this; }

	void Start ()
	{
		planetHealthBar.maxValue = (int)Hero.r.maxHealth;
		planetHealthBar.value = (int)Hero.r.health;
		AudioManager.am.PlayManual();
	}

	void Update ()
	{
		if(Game.g.gameActive)
		{
			SetTimeElapsed();
		}
	}
	public void onPause(){
		gameCtrl.gameActive=false;
	}
	public void onResume(){
		gameCtrl.gameActive=true;
	}
	//On the menu screen, when the "Play" button gets pressed.
	public void OnPlayButton ()
	{
		CameraController.c.TransitionToGameView();
		menuUI.SetActive(false);
		
		AudioManager.am.PlayBattle();
	}

	//On the menu or game over screen, when the "Quit" button gets pressed.
	public void OnQuitButton ()
	{
		Application.Quit();
	}

	//点击设置菜单按钮
	public void OnSettingButton()
	{
		menuUI.SetActive(false);
		settingUI.SetActive(true);
		DisplaySettingUI.SetActive(true);
	}

	//点击返回主菜单
	public void ReturnMainMenu()
	{
		menuUI.SetActive(true);
		settingUI.SetActive(false);
		CreditsUI.SetActive(false);
	}

	//点击制作人名单
	public void OnCreditsButton()
	{
		menuUI.SetActive(false);
		CreditsUI.SetActive(true);
	}

	//点击画面设置
	public void OnDisplaySettingButton()
	{
		DisplaySettingUI.SetActive(true);
		SoundSettingUI.SetActive(false);
		ControlSettingUI.SetActive(false);
		settingUI.SetActive(true);
	}

	//点击声音设置
	public void OnSoundSettingButton()
	{
		DisplaySettingUI.SetActive(false);
		SoundSettingUI.SetActive(true);
		ControlSettingUI.SetActive(false);
		settingUI.SetActive(true);
	}

	//点击控制设置
	public void OnControlSettingButton()
	{
		DisplaySettingUI.SetActive(false);
		SoundSettingUI.SetActive(false);
		ControlSettingUI.SetActive(true);
		settingUI.SetActive(true);
	}

	//On the game over screen, when the "Menu" button gets pressed.
	public void OnMenuButton ()
	{
		Application.LoadLevel(0);
	}

	//Enables the menu UI game object.
	public void SetMenuUI ()
	{
		gameOverUI.SetActive(false);
		gameUI.SetActive(false);
		menuUI.SetActive(true);
	}

	//Enables the game UI game object.
	public void SetGameUI ()
	{
		menuUI.SetActive(false);
		gameOverUI.SetActive(false);
		gameUI.SetActive(true);
	}

	//Enables the game over UI game object.
	public void SetGameOverUI ()
	{	
		menuUI.SetActive(false);
		gameUI.SetActive(false);
		gameOverUI.SetActive(true);

		//Setting text values.
		goDefendTime.text = "You defended the planet for...\n<size=50>" + GetTimeAsString(Game.g.gameTime) + "</size>  minutes";

		//Set highscore text.
		if(Game.g.gameTimeHighscore == 0)
		{
			goHighscoreTime.text = "Your highscore is...\n<size=50>" + GetTimeAsString(Game.g.gameTime) + "</size>  minutes";
			Game.g.SetTimeAsHighscore();
		}
		else
		{
			goHighscoreTime.text = "Your highscore is...\n<size=50>" + GetTimeAsString(Game.g.gameTimeHighscore) + "</size>  minutes";

			//If the current time is higher than the highscore, set that as the highscore.
			if(Game.g.gameTime > Game.g.gameTimeHighscore)
				Game.g.SetTimeAsHighscore();
		}
	}

	//Sets the value of the planet health bar. Called when the planet takes damage.
	public void SetPlanetHealthBarValue (int value)
	{
		planetHealthBar.value = value;
		StartCoroutine(PlanetHealthBarFlash());
	}

	//Flashes the health bar red quickly.
	IEnumerator PlanetHealthBarFlash ()
	{
		Image fill = planetHealthBar.transform.Find("Fill Area/Fill").GetComponent<Image>();

		if(fill.color != Color.red)
		{
			Color dc = fill.color;
			fill.color = Color.red;

			yield return new WaitForSeconds(0.05f);

			fill.color = dc;
		}
	}

	//Sets the text that shows how long the game has been going for.
	void SetTimeElapsed ()
	{
		timeElapsed.text = "TIME ELAPSED\n<size=55>" + GetTimeAsString(Game.g.gameTime) + "</size>";
	}

	//Converts a number to a MINS:SECS time format.
	string GetTimeAsString (float t)
	{
		string mins = Mathf.FloorToInt(t / 60).ToString();

		if(int.Parse(mins) < 10)
			mins = "0" + mins;

		string secs = ((int)(t % 60)).ToString();

		if(int.Parse(secs) < 10)
			secs = "0" + secs;

		return mins + ":" + secs;
	}
}
