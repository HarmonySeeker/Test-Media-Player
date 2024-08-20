using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VideoPreviewUnit : MonoBehaviour
{
    public int videoIdx;
    public Image videoThumbnail;
    public Image loadingRing;
        
    public void SetVideo(int idx, Sprite thumbnail)
    {
        videoIdx = idx;
        videoThumbnail.sprite = thumbnail;
    }

    public void OnClick()
    {

    }

    public int GetIdx() { return videoIdx; }
}
