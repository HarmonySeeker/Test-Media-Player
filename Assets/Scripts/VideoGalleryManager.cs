using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VideoGalleryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject videoUnitPrefab;
    [SerializeField]
    private GameObject thumbnailGrid;

    public void SetLinks(Dictionary<int, string> previewLinks, Action<int> switchVideoFunc)
    {
        foreach(var link in previewLinks)
        {
            GameObject vid = Instantiate(videoUnitPrefab, thumbnailGrid.transform);
            VideoPreviewUnit unit = vid.GetComponent<VideoPreviewUnit>();

            StartCoroutine(DownloadImage(link.Value, unit.videoThumbnail, unit.loadingRing));
            unit.GetComponent<Button>().onClick.AddListener(delegate { switchVideoFunc(link.Key); });
            unit.videoIdx = link.Key;
        }
    }

    IEnumerator DownloadImage(string MediaUrl, Image videoThumbnail, Image loadingThumbnail)
    {
        string filename = Path.GetFileName(new Uri(MediaUrl).AbsolutePath);
        Texture2D previewTexture = GetImage(filename, 512, 256);

        if (!previewTexture)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
                yield break;
            }
            else
            {
                previewTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                SaveImage(previewTexture, filename);
            }
        }

        videoThumbnail.sprite = Sprite.Create(previewTexture, new Rect(0, 0, 512, 256), new Vector2());
        videoThumbnail.color = Color.white;
        loadingThumbnail.enabled = false;
    }

    private Texture2D GetImage(string fileName, int width = 1, int height = 1)
    {
        string savePath = Application.persistentDataPath;

        try
        {
            if (File.Exists(Path.Combine(savePath, fileName)))
            {
                byte[] bytes = File.ReadAllBytes(Path.Combine(savePath, fileName));
                Texture2D texture = new Texture2D(width, height);

                texture.LoadImage(bytes);
                //Debug.Log($"Loading preview texture from {Path.Combine(savePath, fileName)}");

                return texture;
            }

            return null;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    private static void SaveImage(Texture2D image, string filename)
    {
        string savePath = Application.persistentDataPath;
        try
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            File.WriteAllBytes(Path.Combine(savePath, filename), image.EncodeToPNG());
            //Debug.Log($"Saving preview texture to {Path.Combine(savePath, filename)}");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
