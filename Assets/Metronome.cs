using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Metronome : MonoBehaviour
{
    public GameObject SkillIcon;
    
    public GameObject great;
    
    public GameObject perfect;
    
    public GameObject miss;
    public int moetronomeID;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
       	color=GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //颜色(步进鼓机指示)
         SkillIcon.GetComponent<Image>().sprite=Hero.r.playingSkills[moetronomeID].icon;
         
         Color c=new Color(1f,1f,1f,1f);
		Color c1 = new Color(0.5f,0.5f,0.5f,0.75f);
		//color=GetComponent<Image>().color;

		if(moetronomeID==Hero.r.playingSkillID){
			GetComponent<Image>().color=c;
		}else{
			GetComponent<Image>().color=c1;
		}

        //分数判定
       if(Hero.r.nowGrades[moetronomeID]==Hero.grade.noGrade){
       		great.SetActive(false);
       		perfect.SetActive(false);
            miss.SetActive(false);
        }else if(Hero.r.nowGrades[moetronomeID]==Hero.grade.great){
            great.SetActive(true);
            perfect.SetActive(false);
            miss.SetActive(false);
        }else if(Hero.r.nowGrades[moetronomeID]==Hero.grade.perfect){
            great.SetActive(false);
            perfect.SetActive(true);
            miss.SetActive(false);
        }else if(Hero.r.nowGrades[moetronomeID]==Hero.grade.miss){
            great.SetActive(false);
            perfect.SetActive(false);
            miss.SetActive(true);
        }




    }
}
