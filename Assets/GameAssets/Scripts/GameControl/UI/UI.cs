﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour 
{
	public GameObject menuUI;
	public GameObject settingUI;
	public GameObject CreditsUI;
	public GameObject CharacterChooseUI;
	public GameObject DifficultyUI;
	public GameObject ShopUI;
	public GameObject gameUI;
	public GameObject gameOverUI;
	public Game gameCtrl;

	//Game UI
	public Text timeElapsed;
	public Image HealthBar;

	[Header("Skill")]
	public Image[] SkillList;

	//Game Over UI
	public Text goDefendTime;
	public Text goHighscoreTime;

	public static UI ui;

	void Awake () { ui = this; }

	void Start ()
	{
		//HealthBar.maxValue = (int)Hero.r.maxHealth;
		//HealthBar.value = (int)Hero.r.health;
		AudioManager.am.PlayManual();
	}

	void Update ()
	{
		if(Game.g.gameActive)
		{
			GetSkillList();
			SetTimeElapsed();
			RefreshHealthBar();
		}
	}
	public void onPause(){
		gameCtrl.gameActive=false;
		AudioManager.am.Pause();
	}
	public void onResume(){
		ShopUI.SetActive(false);
		gameCtrl.gameActive=true;
		AudioManager.am.Resume();
	}
	//On the menu screen, when the "Play" button gets pressed.
	public void OnPlayButton ()
	{
		CameraController.c.TransitionToGameView(Game.g.StartGame);
		menuUI.SetActive(false);
		settingUI.SetActive(false);
		CreditsUI.SetActive(false);
		CharacterChooseUI.SetActive(false);
		
	}

	//角色选择UI
	public void OnCharacterChoose()
    {
		CharacterChooseUI.SetActive(true);
		DifficultyUI.SetActive(false);
	}

	//难度选择UI
	public void OnDifficulty()
	{
		DifficultyUI.SetActive(true);
	}

	//返回角色选择
	public void ReturnCharacterChoose()
    {
		DifficultyUI.SetActive(false);
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
	}

	//应用并返回
	public void ApplyReturn()
	{
		settingUI.SetActive(false);
	}

	//打开商店
	public void OpenShop()
	{
		//暂停音频
		
		ShopUI.SetActive(true);
		onPause();
	}

	//点击返回主菜单
	public void ReturnMainMenu()
	{
		menuUI.SetActive(true);
		settingUI.SetActive(false);
		CreditsUI.SetActive(false);
		CharacterChooseUI.SetActive(false);
	}

	//点击制作人名单
	public void OnCreditsButton()
	{
		menuUI.SetActive(false);
		CreditsUI.SetActive(true);
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
		float Health;
		Health = value / (int)Hero.r.maxHealth;
		HealthBar.fillAmount = Health;
		//StartCoroutine(PlanetHealthBarFlash());
	}

	//Flashes the health bar red quickly.
	/*IEnumerator PlanetHealthBarFlash ()
	{
		Image fill = HealthBar.transform.Find("FillArea/Fill").GetComponent<Image>();
		
		if(fill.color != Color.red)
		{
			Color dc = fill.color;
			fill.color = Color.red;

			yield return new WaitForSeconds(0.05f);

			fill.color = dc;
		}
		yield return null;
	}*/

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

	void RefreshHealthBar()
    {
		float PlayerHealthBar = Hero.r.health;
		float MaxHealth = Hero.r.maxHealth;

		float HealthRatio = PlayerHealthBar / MaxHealth;
		HealthBar.fillAmount = HealthRatio;
	}

	void GetSkillList()
    {
		for(int i = 0; i < 8; i++)
        {
			SkillList[i].sprite = Hero.r.playingSkills[i].icon;
			Debug.Log("技能音轨数量");
			Debug.Log(Hero.r.playingSkills.Count);
		}
	}
}
