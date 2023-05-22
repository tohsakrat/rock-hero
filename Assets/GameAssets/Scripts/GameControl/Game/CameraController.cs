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
	public delegate void MyDelegateVoid();
	public bool transformedToGameView;//是否已经开场

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
	public void TransitionToGameView (MyDelegateVoid callback = null)
	{
		//Debug.Log("镜头移动到主角，然后放大视野");
		//if(transformedToGameView)return;
		StartCoroutine(gv(callback));
	}

	//开场镜头协程
	IEnumerator gv (MyDelegateVoid callback = null)
	{
		//镜头移动到主角
		//Debug.Log("进入协程");
		//距离主角超过1前
		while(Mathf.Abs(Camera.main.transform.position.x - Hero.r.transform.position.x+ Camera.main.transform.position.y - Hero.r.transform.position.y)> 1)
		{
			Camera.main.transform.position = 
				Vector3.MoveTowards(
					Camera.main.transform.position, 
					new Vector3(Hero.r.transform.position.x, Hero.r.transform.position.y, -10), 
					
			(transformedToGameView ?100:500) * Time.deltaTime);
			yield return null;
		}


		//yield return new WaitForSeconds(0.3f);

		//放大视野
		while(Mathf.Abs(Camera.main.orthographicSize - gameOrthoSize)>0.1)
		{
			Camera.main.orthographicSize = Mathf.MoveTowards(
				Camera.main.orthographicSize, 
				gameOrthoSize, 20 * Time.deltaTime);

			yield return null;
		}

		//等待五秒
		yield return new WaitForSeconds(0.50f);
		
		transformedToGameView = true;
		
		callback();
		
	}

	public void moveTo (Vector4 pos,float speed =25, MyDelegateVoid callback = null)
	{
		StartCoroutine(moveToEnumerator(pos,speed,callback));
	
	}

		IEnumerator moveToEnumerator (Vector4 pos,float speed =25, MyDelegateVoid callback = null)
	{
		//镜头移动到目标
		while(Mathf.Abs(Camera.main.transform.position.x - pos.x+ Camera.main.transform.position.y - pos.y)> 0.1)
		{
			Camera.main.transform.position = 
				Vector3.MoveTowards(
					Camera.main.transform.position, 
					new Vector3(pos.x, pos.y, -10), 
					speed * Time.deltaTime);
			yield return null;
		}

		callback();
		
	}

	public void changeGraphicSize (float target,float speed =20, MyDelegateVoid callbacK = null)
	{
		StartCoroutine(changeGraphicSizeEnumerator(target,speed,callbacK));
	
	}

	IEnumerator changeGraphicSizeEnumerator (float target,float speed =20, MyDelegateVoid callback = null)
	{

		//放大视野，当绝对值大于0.1时
		while(Mathf.Abs(Camera.main.orthographicSize - target)> 0.1f)
		{
			Camera.main.orthographicSize = Mathf.MoveTowards(
				Camera.main.orthographicSize, 
				target, speed * Time.deltaTime);

			yield return null;
		}

		
		callback();
		
	}



	
	IEnumerator waitThenDoEnumerator ( float time,MyDelegateVoid callback = null)
	{	Debug.Log("waitThenDo");
		yield return new WaitForSeconds(time);
		Debug.Log("waitFinished");
		callback();
		
	}
	public void waitThenDo (float time, MyDelegateVoid callback = null)
	{
		
		StartCoroutine(waitThenDoEnumerator(time,callback));
	
	}

	public void closeUpShot(Vector3 pos, float speed =20f, float shotTime=1f,MyDelegateVoid callbacK = null){

		//镜头移动到目标
		moveTo(pos,speed,
		 () =>{
			//放大视野
			changeGraphicSize(15f,20f,() =>{
				//等待特写时间
				waitThenDo(shotTime,() =>{

					TransitionToGameView(callbacK); 
				
				});
				


			});
			
			});
		
	}

	
	

}
