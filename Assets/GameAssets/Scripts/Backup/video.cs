using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//开启流文件读取
using UnityEngine.Video;

using UnityEngine.UI;
public class video : MonoBehaviour
{
    public VideoPlayer mVideoPlayer;
    public Button mBtn_Skip;
 
    public void WUIStartVideoPanel()
    {
        // 清除Raw Image的残留帧
        mVideoPlayer.targetTexture.Release();
        // 监听视频播放结束
        mVideoPlayer.loopPointReached += EndReached;
        mBtn_Skip.onClick.AddListener(OnSkipBtnClick);
    }
 
    private void EndReached(VideoPlayer source)
    {
        // 隐藏当前脚本对象
        gameObject.SetActive(false);
    }
 
    // 外部调用播放
    public void PlayVideo()
    {
        mVideoPlayer.Play();
    }
 
    // 跳过视频
    private void OnSkipBtnClick()
    {
        mVideoPlayer.Stop();
        EndReached(mVideoPlayer);
    }
}
