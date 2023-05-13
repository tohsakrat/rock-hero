/*
UniGif
Copyright (c) 2015 WestHillApps (Hironari Nishioka)
This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Texture Animation from GIF image
/// </summary>
[RequireComponent(typeof(UniGifImageAspectController))]


public class UniGifImage : MonoBehaviour
{
    /// <summary>
    /// This component state
    /// </summary>
    public enum State
    {
        None,
        Loading,
        Ready,
        Playing,
        Pause,
    }

    //修改原包，读取指定地址
    public string gifResourcePath;

    // Target row image
    [SerializeField]
    public RawImage m_rawImage;
    // Image Aspect Controller
    [SerializeField]
    private UniGifImageAspectController m_imgAspectCtrl;
    // Textures filter mode
    [SerializeField]
    private FilterMode m_filterMode = FilterMode.Point;
    // Textures wrap mode
    [SerializeField]
    private TextureWrapMode m_wrapMode = TextureWrapMode.Clamp;
    // Load from url on start
    
    [HideInInspector]
    private bool m_loadOnStart;
    // GIF image url (WEB or StreamingAssets path)
    
    [HideInInspector]
    private string m_loadOnStartUrl;
    // Rotating on loading
    
    [HideInInspector]
    private bool m_rotateOnLoading=false;
    // Debug log flag
    [SerializeField]
    private bool m_outputDebugLog;
    public string path;//不用设置，纯粹为了调试好看
    // Decoded GIF texture list
    private List<UniGif.GifTexture> m_gifTextureList;
    // Delay time
    private float m_delayTime;
    // Texture index

    [SerializeField]
    public int m_gifTextureIndex;
    
    // loop counter
    private int m_nowLoopCount;

    /// <summary>
    /// Now state
    /// </summary>
    public State nowState
    {
        get;
        private set;
    }

    /// <summary>
    /// Animation loop count (0 is infinite)
    /// </summary>
    public int loopCount
    {
        get;
        private set;
    }

    /// <summary>
    /// Texture width (px)
    /// </summary>
    public int width
    {
        get;
        private set;
    }

    /// <summary>
    /// Texture height (px)
    /// </summary>
    public int height
    {
        get;
        private set;
    }

    private void Start()
    {   
        gifResourcePath = Regedit.r.gifDir;
        if (m_rawImage == null)
        {
            m_rawImage = GetComponent<RawImage>();
        }

        if (m_loadOnStart)
        {
            SetGifFromUrl(m_loadOnStartUrl);

        }
    }

    private void OnDestroy()
    {
        Clear();
    }

    private void Update()
    {
        switch (nowState)
        {
            case State.None:
                break;

            case State.Loading:
                if (m_rotateOnLoading)
                {
                    transform.Rotate(0f, 0f, 30f * Time.deltaTime, Space.Self);
                }
                break;

            case State.Ready:
                break;

            case State.Playing:
                if (m_rawImage == null || m_gifTextureList == null || m_gifTextureList.Count <= 0)
                {
                    return;
                }
                if (m_delayTime > Time.time)
                {
                    return;
                }
                // Change texture
                m_gifTextureIndex++;
                if (m_gifTextureIndex >= m_gifTextureList.Count)
                {
                    m_gifTextureIndex = 0;

                    if (loopCount > 0)
                    {
                        m_nowLoopCount++;
                        if (m_nowLoopCount >= loopCount)
                        {
                            Stop();
                            return;
                        }
                    }
                }
                m_rawImage.texture = m_gifTextureList[m_gifTextureIndex].m_texture2d;
                m_delayTime = Time.time + m_gifTextureList[m_gifTextureIndex].m_delaySec;
                break;

            case State.Pause:
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Set GIF texture from url
    /// </summary>
    /// <param name="url">GIF image url (WEB or StreamingAssets path)</param>
    /// <param name="autoPlay">Auto play after decode</param>
    public void SetGifFromUrl(string url, bool autoPlay = true)
    {
        StartCoroutine(SetGifFromUrlCoroutine(url, autoPlay));
    }

    /// <summary>
    /// Set GIF texture from url
    /// </summary>
    /// <param name="url">GIF image url (WEB or StreamingAssets path)</param>
    /// <param name="autoPlay">Auto play after decode</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator SetGifFromUrlCoroutine(string url, bool autoPlay = true)
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogError("URL is nothing.");
            yield break;
        }

        if (nowState == State.Loading)
        {
            Debug.LogWarning("Already loading.");
            yield break;
        }
        nowState = State.Loading;

        
        if (url.StartsWith("http"))
        {
            // from WEB
            path = url;
        }
        else
        {
            // from StreamingAssets
            
            if(Game.g.Debuging){
                path = Path.Combine( Application.streamingAssetsPath, url);
            }else{
                path=Path.Combine(Application.dataPath+gifResourcePath,url);
            }
            //Debug.Log(Application.dataPath+"Resource/Images");
        }
        //如果全局请求字典没有当前path,
        UnityWebRequest request;
        if(!Regedit.r.RequestDic.ContainsKey(path)){
           
           
           
            request = UnityWebRequest.Get(path);//new UnityWebRequest(path, UnityWebRequest.kHttpVerbGET);}
             request.SendWebRequest();
            Regedit.r.RequestDic.Add(path,request);
        }else{
            request=Regedit.r.RequestDic[path];
        }

        //Resources.Load(path) as UnityWebRequest;

        while (!request.isDone)
        {   
            
            yield return null;
        }

        if (string.IsNullOrEmpty(request.error) == false)
        {
            Debug.LogError("File load error.\n" + request.error + "\npath: " + path);
            nowState = State.None;
            yield break;
        }

        Clear();
        nowState = State.Loading;

        // Get GIF textures
        yield return StartCoroutine(
        UniGif.GetTextureListCoroutine(request.downloadHandler.data, (gifTexList, loopCount, width, height) =>
        {  
             


          // request.Dispose();
            if (gifTexList != null)
            {   
                m_gifTextureList = gifTexList;
                this.loopCount = loopCount;
                this.width = width;
                this.height = height;
                nowState = State.Ready;

                m_imgAspectCtrl.FixAspectRatio(width, height);

                if (m_rotateOnLoading)
                {
                    transform.localEulerAngles = Vector3.zero;
                }

                if (autoPlay)
                {
                    Play();
              m_rawImage.color= new Color(1f, 1f, 1f, 1f);
             //  Debug.Log(m_rawImage.color.GetType()); 
                }
            }
            else
            {
                Debug.LogError("Gif texture get error.");
                nowState = State.None;
            }
        },
        m_filterMode, m_wrapMode, m_outputDebugLog));

    }

    /// <summary>
    /// Clear GIF texture
    /// </summary>
    public void Clear()
    {
        if (m_rawImage != null)
        {
            m_rawImage.texture = null;
        }

        if (m_gifTextureList != null)
        {
            for (int i = 0; i < m_gifTextureList.Count; i++)
            {
                if (m_gifTextureList[i] != null)
                {
                    if (m_gifTextureList[i].m_texture2d != null)
                    {
                        Destroy(m_gifTextureList[i].m_texture2d);
                        m_gifTextureList[i].m_texture2d = null;
                    }
                    m_gifTextureList[i] = null;
                }
            }
            m_gifTextureList.Clear();
            m_gifTextureList = null;
        }

        nowState = State.None;
    }

    /// <summary>
    /// Play GIF animation
    /// </summary>
    public void Play()
    {
        if (nowState != State.Ready)
        {
            Debug.LogWarning("State is not READY.");
            return;
        }
        if (m_rawImage == null || m_gifTextureList == null || m_gifTextureList.Count <= 0)
        {
            Debug.LogError("Raw Image or GIF Texture is nothing.");
            return;
        }
        nowState = State.Playing;
        m_rawImage.texture = m_gifTextureList[0].m_texture2d;
        m_delayTime = Time.time + m_gifTextureList[0].m_delaySec;
        m_gifTextureIndex = 0;
        m_nowLoopCount = 0;
    }

    /// <summary>
    /// Stop GIF animation
    /// </summary>
    public void Stop()
    {
        if (nowState != State.Playing && nowState != State.Pause)
        {
            Debug.LogWarning("State is not Playing and Pause.");
            return;
        }
        nowState = State.Ready;
    }

    /// <summary>
    /// Pause GIF animation
    /// </summary>
    public void Pause()
    {
        if (nowState != State.Playing)
        {
            Debug.LogWarning("State is not Playing.");
            return;
        }
        nowState = State.Pause;
    }

    /// <summary>
    /// Resume GIF animation
    /// </summary>
    public void Resume()
    {
        if (nowState != State.Pause)
        {
            Debug.LogWarning("State is not Pause.");
            return;
        }
        nowState = State.Playing;
    }
}