using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class Regedit : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<Enemy> Enemies =  new  List<Enemy>();//敌人
    public static List<Pickup> Bag =  new  List<Pickup>();//背包
    public static List<Pickup> Pickups =  new  List<Pickup>();//补给(还没被捡起来)
    public Dictionary<string,Skill> SkillDic = new Dictionary<string,Skill>();//全局技能图鉴,只有技能不需要被实例化，所以类型不是GameObject
    public Dictionary<string,GameObject> EnemyDic = new Dictionary<string,GameObject>();//全局敌人图鉴
    public Dictionary<string,GameObject> PickupDic = new Dictionary<string,GameObject>();//全局道具图鉴
    public string EnemyDir;//敌人文件存放目录
    public string PickupDir;//道具文件存放目录
    public string SkillDir;//技能脚本文件存放目录
    public Transform EnemiesParent;//敌人父节点
    public Transform  ModelLayer;//模型层
    public Transform  ViewLayer;//视图层

    public  Transform BulletLayer;//子弹层
    public  Transform PickupLayer;//补给层

    public GameObject deathParticleEffect;

    public static Regedit r;

    
    void Awake(){
        r=this;
        //读取敌人目录中全部预制体文件
        Object[] enemyPrefabs = Resources.LoadAll(EnemyDir, typeof(GameObject));
        foreach (Object enemyPrefab in enemyPrefabs)
        {
            GameObject enemy = Instantiate(enemyPrefab,ModelLayer) as GameObject;
            enemy.SetActive(false);
            EnemyDic.Add(enemy.name.Replace("(Clone)", ""),enemy);
        }
        //读取道具目录中全部预制体文件
        Object[] pickupPrefabs = Resources.LoadAll(PickupDir, typeof(GameObject));
        foreach (Object pickupPrefab in pickupPrefabs)
        {
            GameObject pickup = Instantiate(pickupPrefab,ModelLayer) as GameObject;
            pickup.SetActive(false);
            PickupDic.Add(pickup.name.Replace("(Clone)", ""),pickup);
        }

        //读取技能目录全部预制体文件，并将他的siill component添加到全局技能图鉴中
        Object[] skillPrefabs = Resources.LoadAll(SkillDir, typeof(GameObject));
        foreach (Object skillPrefab in skillPrefabs)
        {
            GameObject skill = Instantiate(skillPrefab,ModelLayer) as GameObject;
            skill.SetActive(false);
            Skill skillComponent = skill.GetComponent<Skill>();
            SkillDic.Add(skill.name.Replace("(Clone)", ""),skillComponent);
        }
        


        //打印敌人目录
        Debug.Log("打印敌人目录");
        Debug.Log(EnemyDic.Count);
        foreach (KeyValuePair<string,GameObject> enemy in EnemyDic)
        {
            Debug.Log(enemy.Key);
        }
        //打印道具目录
        Debug.Log("打印道具目录");
        Debug.Log(PickupDic.Count);
        foreach (KeyValuePair<string,GameObject> pickup in PickupDic)
        {
            Debug.Log(pickup.Key);
        }
        //打印技能目录 
        Debug.Log("打印技能目录");
        Debug.Log(SkillDic.Count);
        foreach (KeyValuePair<string,Skill> skill in SkillDic)
        {
            Debug.Log(skill.Key);
        }

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
