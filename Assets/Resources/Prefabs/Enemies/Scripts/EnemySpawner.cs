using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{

	public Rect spawnBoundry; //The edges of which the enemies can spawn on.

	public float timeBetweenEnemySpawn;
	private float spawnTimer;


	void Start ()
	{

	}

	void Update ()
	{
		spawnTimer += Time.deltaTime;

		//Spawn an enemy every 'timeBetweenSpawn' seconds, if the game is running.
		if(spawnTimer >= timeBetweenEnemySpawn && Game.g.gameActive)
		{
			spawnTimer = 0.0f;
			SpawnEnemy();
		}
	}

	//Called when an enemy needs to be spawned.
	void SpawnEnemy ()
	{
		float spawnDirection = Random.Range(1, 5); //Left, Up, Right, Down.
		Vector3 spawnPos = Vector3.zero;

		//Get spawn pos based of screen direction.
		if(spawnDirection == 1)
			spawnPos = new Vector3(spawnBoundry.xMin, Random.Range(spawnBoundry.yMin, spawnBoundry.yMax), 0);
		else if(spawnDirection == 2)
			spawnPos = new Vector3(Random.Range(spawnBoundry.xMin, spawnBoundry.xMax), spawnBoundry.yMax, 0);
		else if(spawnDirection == 3)
			spawnPos = new Vector3(spawnBoundry.xMax, Random.Range(spawnBoundry.yMin, spawnBoundry.yMax), 0);
		else
			spawnPos = new Vector3(Random.Range(spawnBoundry.xMin, spawnBoundry.xMax), spawnBoundry.yMin, 0);

		//Spawn the enemy.
		GameObject enemy = Instantiate(GetRandomEnemy(), spawnPos, Quaternion.identity, Regedit.r.EnemiesParent.transform);
		enemy.SetActive(true);
	}

	//随机返回一个敌人预制体
	GameObject GetRandomEnemy()
	{

		//在全局敌人字典 Regedit.EnemyDic<string,GameObject>中随机抽取一个元素
		int randomIndex = Random.Range(0, Regedit.r.EnemyDic.Count);
		int i = 0;
		foreach (KeyValuePair<string, GameObject> enemy in Regedit.r.EnemyDic)
		{
			if (i == randomIndex)
			{
				return enemy.Value;
			}
			i++;
		}
		return null;
	}


}
