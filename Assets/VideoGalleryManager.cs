using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VideoGalleryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject videoUnitPrefab;

    public void SetLinks(Dictionary<int, string> previewLinks, Action<int> switchVideoFunc)
    {
        foreach(var link in previewLinks)
        {
            GameObject vid = Instantiate(videoUnitPrefab, transform);
            VideoPreviewUnit unit = vid.GetComponent<VideoPreviewUnit>();

            StartCoroutine(DownloadImage(link.Value, unit.videoThumbnail));
            unit.GetComponent<Button>().onClick.AddListener(delegate { switchVideoFunc(link.Key); });
            unit.videoIdx = link.Key;
        }
    }

    IEnumerator DownloadImage(string MediaUrl, Image videoThumbnail)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
            videoThumbnail.sprite = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture, new Rect(0, 0, 512, 256), new Vector2());
    }

    public void InitializeVideo((int, Sprite) value)
    {
        GameObject vid = Instantiate(videoUnitPrefab, transform);
        VideoPreviewUnit unit = vid.GetComponent<VideoPreviewUnit>();

        unit.SetVideo(value.Item1, value.Item2);
    }


    public void InitializeVideos(List<(int, Sprite)> values)
    {
        foreach (var v in values)
        {
            GameObject vid = Instantiate(videoUnitPrefab, transform);
            VideoPreviewUnit unit = vid.GetComponent<VideoPreviewUnit>();

            unit.SetVideo(v.Item1, v.Item2);
        }
    }
}
