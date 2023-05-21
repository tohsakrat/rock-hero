using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	//Audio Clips
	public AudioClip bgmManual;
	public AudioClip bgmNormal1;
	public AudioClip bgmNormal2;
	public AudioClip bgmBattle;
	public AudioClip bgmBoss;
	public AudioClip shoot1;
	public AudioClip shoot2;
	public AudioClip shoot;
	public AudioClip getPickup;
	public AudioClip enemyDeath;
	public AudioClip enemyHit;
	public AudioClip buttonHover;
	public AudioClip buttonClick;
	public AudioClip beat;
	
	//Audio Source
	public AudioSource audioSource;
	public AudioSource audioSource2;

	public static AudioManager am;

	void Awake () { am = this; }

	void Update () 
	{ 
		//audiosource的bpm随着主角bpm变化
		if(!Game.g.gameActive){return;}
		if(Hero.r==null){return;}
		audioSource.pitch = Hero.r.currentStatus.BeatsPerMinute/Hero.r.currentStatus.BeatsPerMinute;
		audioSource2.pitch = Hero.r.currentStatus.BeatsPerMinute/Hero.r.currentStatus.BeatsPerMinute;
	}


	public void Play (AudioClip As)
	{
		audioSource2.PlayOneShot(As);
	}
	public void Beat ()
	{
		audioSource2.PlayOneShot(beat);
	}

	public void Pause ()
	{
		audioSource.Pause();
	}

	public void Resume()
	{
		audioSource.UnPause();
	}
	public void PlayManual ()
	{

		//随机播放一首非战斗bgm
		audioSource.Stop();
		audioSource.PlayOneShot(bgmManual);
		
	}
	public void PlayBoss ()
	{
		audioSource.Stop();
		audioSource.PlayOneShot( bgmBoss);
		//音量增加
		audioSource.volume = 0.5f;
		
	}
	//Plays the "getPickup" sound effect, from the audio source.
	public void PlayNormal ()
	{
		//先停止正在播放的bgm 再换成normal音乐
		audioSource.Stop();
		audioSource.PlayOneShot(bgmNormal1);
		Debug.Log("play normal");
		


	}



	//Plays the "enemyDeath" sound effect, from the audio source.
	public void PlayBattle ()
	{audioSource.Stop();
		audioSource.PlayOneShot(bgmBattle);
	}

	//Plays the "enemyHit" sound effect, from the audio source.





	public void PlayShoot ()
	{
	audioSource.PlayOneShot(shoot1);
	/*Random rd ;
	int a = rd.Next(0,100);
	if(a>50){audioSource.PlayOneShot(shoot1);}else{
		audioSource.PlayOneShot(shoot2);
	}*/
	}

	//Plays the "getPickup" sound effect, from the audio source.
	public void PlayGetPickup ()
	{
		audioSource.PlayOneShot(getPickup);
	}

	//Plays the "enemyDeath" sound effect, from the audio source.
	public void PlayEnemyDeath ()
	{
		audioSource.PlayOneShot(enemyDeath);
	}

	//Plays the "enemyHit" sound effect, from the audio source.
	public void PlayEnemyHit ()
	{
		audioSource.PlayOneShot(enemyHit);
	}

	//Plays the "buttonHover" sound effect, from the audio source.
	public void PlayButtonHover ()
	{
		audioSource.PlayOneShot(buttonHover);
	}

	//Plays the "buttonClick" sound effect, from the audio source.
	public void PlayButtonClick ()
	{
		audioSource.PlayOneShot(buttonClick);
	}
}
