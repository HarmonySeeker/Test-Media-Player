using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaylistSetup : MonoBehaviour
{
    private PlaylistMediaPlayer pmp;

    private List<string> videoLinks = new List<string> {
        "https://storage.yandexcloud.net/viavr.global/CARTOONS/Peppa_Pig/Peppa_Pig_s01e40_Daddy_Gets_Fit.mp4",
        "https://storage.yandexcloud.net/viavr.global/CARTOONS/Peppa_Pig/Peppa_Pig_s01e41_Shopping.mp4",
        "https://storage.yandexcloud.net/viavr.global/CARTOONS/Peppa_Pig/Peppa_Pig_s01e38_Sleepy_Princess.mp4"
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
        
        foreach (string link in videoLinks)
        {
            MediaPlaylist.MediaItem item = new MediaPlaylist.MediaItem();
            
            item.mediaPath = new MediaPath(link, MediaPathType.AbsolutePathOrURL);
            pmp.Playlist.Items.Add(item);
        }
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
