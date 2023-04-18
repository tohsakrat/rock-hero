using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public float lerpSpeed;//镜头跟随速度

	private bool shakingCam;//是否在震动镜头

	public Vector3 menuCameraPos;//回到菜单时镜头位置
	public float menuOrthoSize;//回到菜单时镜头缩放比
	public float gameOrthoSize;//游戏时镜头缩放比

	public static CameraController c;


	void Awake () { 
		//设置全局变量Comeracontroller.c
		c = this;
	 }

	void LateUpdate ()
	{
		//镜头跟随
		if(Game.g.gameActive)
		{
			transform.position = Vector3.Lerp(
				transform.position, 
				(new Vector3(Hero.r.transform.position.x,Hero.r.transform.position.y,transform.position.z)), lerpSpeed * Time.deltaTime);	
		}
	}

	//震动镜头api
	public void Shake (float duration, float amount, float intensity)
	{		
		//震动镜头
		//震动时间，震动幅度，震动速度
		if(!shakingCam)
			StartCoroutine(ShakeCam(duration, amount, intensity));
	}

	//震动镜头协程
	IEnumerator ShakeCam (float dur, float amount, float intensity)
	{
		float t = dur;
		Vector3 originalPos = Camera.main.transform.localPosition;
		Vector3 targetPos = Vector3.zero;
		shakingCam = true;

		while(t > 0.0f)
		{
			if(targetPos == Vector3.zero)
			{
				targetPos = originalPos + (Random.insideUnitSphere * amount);
			}

			Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetPos, intensity * Time.deltaTime);

			if(Vector3.Distance(Camera.main.transform.localPosition, targetPos) < 0.02f)
			{
				targetPos = Vector3.zero;
			}

			t -= Time.deltaTime;
			yield return null;
		}

		Camera.main.transform.localPosition = originalPos;
		shakingCam = false;
	}

	//打开菜单时镜头位置
	public void SetMenuView ()
	{
		Camera.main.transform.position = menuCameraPos;
		Camera.main.orthographicSize = menuOrthoSize;
	}

	//开场镜头api
	//镜头移动到主角，然后放大视野
	public void TransitionToGameView ()
	{
		StartCoroutine(gv());
	}

	//开场镜头协程
	IEnumerator gv ()
	{
		//镜头移动到主角
		while(Camera.main.transform.position.x < 0.0f)
		{
			Camera.main.transform.position = 
				Vector3.MoveTowards(
					Camera.main.transform.position, 
					new Vector3(Hero.r.transform.position.x, Hero.r.transform.position.y, -10), 
					8 * Time.deltaTime);
			yield return null;
		}


		yield return new WaitForSeconds(0.3f);

		//放大视野
		while(Camera.main.orthographicSize < gameOrthoSize)
		{
			Camera.main.orthographicSize = Mathf.MoveTowards(
				Camera.main.orthographicSize, 
				gameOrthoSize, 20 * Time.deltaTime);

			yield return null;
		}

		//案例在这里写得不太好，有空应该帮他改一下。camera属于视图层，game属于控制层，这里的逻辑应该放在game里面。
		Game.g.StartGame();
	}
}
