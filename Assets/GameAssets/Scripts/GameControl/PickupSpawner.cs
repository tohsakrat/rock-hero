using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour 
{
	//Prefabs
	public GameObject speedFirePrefab;
	public GameObject planetShieldPrefab;
	public GameObject turretPrefab;

	public float pickupMinSpawnTime;
	public float pickupMaxSpawnTime;
	private float pickupSpawnTime;
	private float timer;

	void Start ()
	{
		pickupSpawnTime = Random.Range(pickupMinSpawnTime, pickupMaxSpawnTime);
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= pickupSpawnTime && Game.g.gameActive)
		{
			SpawnPickup();
			timer = 0.0f;
			pickupSpawnTime = Random.Range(pickupMinSpawnTime, pickupMaxSpawnTime);
		}
	}

	//Spawns a pickup in the game.
	void SpawnPickup ()
	{
		GameObject pickup = Instantiate(GetRandomPickup(), GetRandomPositon(), Quaternion.identity);
		pickup.SetActive(true);
	}

	//Returns a random positon behind the player.
	Vector3 GetRandomPositon ()
	{
		Vector3 dir = Hero.r.transform.position.normalized;
		return -dir * Random.Range(8.0f, 15.0f);
	}

	//Returns a random pickup prefab.
	GameObject GetRandomPickup ()
	{

		Debug.Log("随机抽取道具");
		//在全局道具图鉴字典里随机抽一个元素返回
		//全局道具图鉴为Regedit.r.PickupDic : Dictionary<string,GameObject>();
		//下一行正式开始代码
		 List<string> Keys = new  List<string>();//获取所有key

		foreach (var item in Regedit.r.PickupDic)
		{
			Keys.Add(item.Key);
		}

		 int index = Random.Range(0,Keys.Count);//随机抽一个key
		 string key = Keys[index];//获取key
		 Debug.Log(key);
		 return Regedit.r.PickupDic[key];//返回对应的道具
	}
}
