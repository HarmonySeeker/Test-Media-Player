using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class PlaylistLogic : MonoBehaviour
{
    private PlaylistMediaPlayer pmp;
    [SerializeField]
    private TMP_Text videoName;

    private Dictionary<int, List<string>> videoInfo = new Dictionary<int, List<string>> {
        {0, new List<string> {"1. Свинка Пеппа: 40. Папа решил стать стройным",
            "https://storage.yandexcloud.net/viavr.global/CARTOONS/Peppa_Pig/Peppa_Pig_s01e40_Daddy_Gets_Fit.mp4",
            "https://storage.yandexcloud.net/viavr.global/assets/content_card/bfdebabe-a264-4b97-ae74-e6abb2bc5ce3/images/47631a5d39d3.jpg"} },
        {1, new List<string> {"2. Свинка Пеппа: 41. Супермаркет",
            "https://storage.yandexcloud.net/viavr.global/CARTOONS/Peppa_Pig/Peppa_Pig_s01e41_Shopping.mp4",
            "https://storage.yandexcloud.net/viavr.global/assets/content_card/a0a9a962-9510-40b0-bad6-897722e010a4/images/f6ab6c7ffc58.jpg"} },
        {2, new List<string> {"3. Свинка Пеппа: 38. Спящая принцесса",
            "https://storage.yandexcloud.net/viavr.global/CARTOONS/Peppa_Pig/Peppa_Pig_s01e38_Sleepy_Princess.mp4",
            "https://storage.yandexcloud.net/viavr.global/assets/content_card/f91aaa3e-d4b9-4118-975a-6091e8ea29b7/images/eab2500465ed.jpg"} }
    };

    private void Awake()
    {
        pmp = GetComponent<PlaylistMediaPlayer>();

        pmp.LoopMode = PlaylistMediaPlayer.PlaylistLoopMode.None;
        pmp.AutoCloseVideo = true;
        pmp.AutoProgress = false;
        pmp.DefaultTransition = PlaylistMediaPlayer.Transition.Zoom;
        pmp.DefaultTransitionDuration = 2.0f;
        pmp.DefaultTransitionEasing = PlaylistMediaPlayer.Easing.Preset.InOutCubic;

        // Query the playlist
        int playlistSize = pmp.Playlist.Items.Count();
        MediaPlaylist.MediaItem currentItem = pmp.PlaylistItem;

        if (currentItem != null)
        {
            Debug.Log("Current media is at " + currentItem.mediaPath.GetResolvedFullPath());
        }

        int playlistItemIndex = pmp.PlaylistIndex;

        // Build the playlist

        foreach (KeyValuePair<int, List<string>> infoEntry in videoInfo)
        {
            MediaPlaylist.MediaItem item = new MediaPlaylist.MediaItem();
            
            item.mediaPath = new MediaPath(infoEntry.Value[1], MediaPathType.AbsolutePathOrURL);
            item.startMode = PlaylistMediaPlayer.StartMode.Manual;
            item.name = infoEntry.Value[0];

            pmp.Playlist.Items.Add(item);

            if (videoName.text == "")
            {
                videoName.text = item.name;
            }

            Debug.Log(infoEntry.Value[0]);
        }
    }

    private void SwitchTo(int idx)
    {
        if (pmp.CanJumpToItem(idx))
        {
            pmp.JumpToItem(idx);
            videoName.text = pmp.PlaylistItem.name;
        }
    }

    private void Next()
    {

    }

    private void Previous()
    {

    }

    private void LoadText()
    {

    }

    private void Custom()
    {
        pmp.Pause();
        pmp.Play();

        if (pmp.CanJumpToItem(2))
        {
            pmp.JumpToItem(2);
        }
        if (!pmp.NextItem())
        {
            Debug.Log("Can't change item");
        }
        if (!pmp.PrevItem())
        {
            Debug.Log("Can't change item");
        }
    }
}
