using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;//启用该插件
using System.IO;//开启流文件读取
using System;
using System.Text;
using System.Reflection;

public class json : MonoBehaviour
{
	public static json j;
    
    /// <summary>台词类，API字段表具体可以商量</summary>
    public class dialogue
    {   
        public float delay;
        public float speed;
        public string content;
        public string charactor;
        public string action;
        public string type;
        public string anim;
        public bool autoPlay;
    
    
        public dialogue(float d, float s, string co,string ch,string ac = "",string t = "info",string am = "",bool au = true){
            delay=d;
            speed=s;
            content=co;
            charactor=ch;
            action=ac;
            type=t;
            anim=am;
            autoPlay=au;
        }
    }
    public List<dialogue> list = new List<dialogue>();

	void Awake () { 
        j = this; 
        }

    // Start is called before the first frame update



    //list写入试例//
    //除了生成调试数据，写功能一般用不上         
    /// <summary>本段台词及演出为：你妈走进房间，停顿了一秒，用常规语速说“666”，同时打了你</summary>

     void Start()
    {
     /*  list.Add(new dialogue(1f, 1f, "你妈走进了房间", "system","normal","info","fadeInLeft","fadeOutLeft"));
        list.Add(new dialogue(0f, 1f, "666", "mom","fight","perform","fadeIn","fadeOut"));
        list.Add(new dialogue(0f, 1f, "", "perform","sanOff","perform","fadeIn","fadeOut"));//演出效果，san值降低
        list.Add(new dialogue(0f, 1f, "你默默地蛄蛹进了被窝", "perform","sanOff","perform","fadeIn","fadeOut"));
        write("1.json",list);*/
    }



    public List<dialogue> load(string filename)
    {   List<dialogue> readList;
      string path = "./Assets/jsonData/"+filename;
        string data= File.ReadAllText(path);
        readList = JsonConvert.DeserializeObject<List<dialogue>>(data);
        return  readList;
        
    }

    
    public void write(string filename, List<dialogue> writeList)
    {
        string path = "./Assets/jsonData/"+filename;
        CreateDirectory(path);//若不存在，则创建；
        string str =JsonConvert.SerializeObject(writeList);
        //string str = JsonUtility.ToJson(writeList);//实测JsonUtility对数组效果并不好。
         //Debug.Log((string)JsonUtility.ToJson(writeList));
          //Debug.Log(JsonConvert.SerializeObject(writeList));
        File.WriteAllText(path,str);
        
    }


     private static void CreateDirectory(string jsonPath)
    {
        FileInfo fileInfo = new FileInfo(jsonPath);
        if (!Directory.Exists(fileInfo.Directory.ToString()))
        {
            Directory.CreateDirectory(fileInfo.Directory.ToString());
        }

        if (string.IsNullOrEmpty(jsonPath) || !File.Exists(jsonPath))
        {
            File.Create(jsonPath).Dispose();
        }
    }


}
