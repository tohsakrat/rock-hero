using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playItselfGif : MonoBehaviour
{
    // Start is called before the first frame update
    public string gifUrlInput;

    private SpriteRenderer m_spriteRenderer;
    private bool m_mutex;
    private RawImage m_rawImage;
    private UniGifImage gifImage;


    void Start()
    {   
        m_spriteRenderer=GetComponent<SpriteRenderer>();

        if( m_spriteRenderer!=null)Destroy ( m_spriteRenderer);//如果有spriteRenderer预览图，就删掉

        m_rawImage=GetComponent<UniGifImage>().m_rawImage;

        gifImage=GetComponent<UniGifImage>();
        if (m_mutex || gifImage == null || string.IsNullOrEmpty(gifUrlInput))
        {
            return;
        }
        m_mutex = true;
       m_rawImage.color= new Color(0f, 0f, 0f, 0f);

        StartCoroutine(ViewGifCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private IEnumerator ViewGifCoroutine()
    {   Debug.Log(gifUrlInput);
        yield return StartCoroutine(gifImage.SetGifFromUrlCoroutine(gifUrlInput));
        m_mutex = false;
    }
}
