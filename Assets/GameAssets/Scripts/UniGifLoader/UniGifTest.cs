using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// gif显示测试
/// </summary>
public class UniGifTest : MonoBehaviour
{
    public string gifUrlInput;
    public UniGifImage gifImage;

    /// <summary>
    /// 锁
    /// </summary>
    private bool m_mutex;

    private void Start()
    {

            if (m_mutex || gifImage == null || string.IsNullOrEmpty(gifUrlInput))
            {
                return;
            }
            m_mutex = true;
          //  StartCoroutine(ViewGifCoroutine());
    }

    private IEnumerator ViewGifCoroutine()
    {
        yield return StartCoroutine(gifImage.SetGifFromUrlCoroutine(gifUrlInput));
        m_mutex = false;
    }
}