using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlaylistLogic : MonoBehaviour
{
    private PlaylistMediaPlayer pmp;
    [SerializeField]
    private TMP_Text videoName;

    private Dictionary<string, string> videoInfo = new Dictionary<string, string> {
        { "https://storage.yandexcloud.net/viavr.global/CARTOONS/Peppa_Pig/Peppa_Pig_s01e40_Daddy_Gets_Fit.mp4", "1. Свинка Пеппа: 40. Папа решил стать стройным"},
        { "https://storage.yandexcloud.net/viavr.global/CARTOONS/Peppa_Pig/Peppa_Pig_s01e41_Shopping.mp4", "2. Свинка Пеппа: 41. Супермаркет" },
        { "https://storage.yandexcloud.net/viavr.global/CARTOONS/Peppa_Pig/Peppa_Pig_s01e38_Sleepy_Princess.mp4", "3. Свинка Пеппа: 38. Спящая принцесса" }
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

        foreach (KeyValuePair<string, string> infoEntry in videoInfo)
        {
            MediaPlaylist.MediaItem item = new MediaPlaylist.MediaItem();
            
            item.mediaPath = new MediaPath(infoEntry.Key, MediaPathType.AbsolutePathOrURL);
            item.startMode = PlaylistMediaPlayer.StartMode.Manual;
            item.name = infoEntry.Value;
            pmp.Playlist.Items.Add(item);

            if (videoName.text == "")
            {
                videoName.text = item.name;
            }

            Debug.Log(infoEntry.Value);
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
