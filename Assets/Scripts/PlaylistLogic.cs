using RenderHeads.Media.AVProVideo;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using static RenderHeads.Media.AVProVideo.MediaPlaylist;

public class PlaylistLogic : MonoBehaviour
{
    private PlaylistMediaPlayer pmp;
    private int vidCounter;

    [SerializeField]
    private VideoGalleryManager videoGalleryManager;
    [SerializeField]
    private TMP_Text videoName;

    private void Awake()
    {
        List<JSONVideo> videoInfo = initDict();
        Dictionary<int, string> previewLinks = new Dictionary<int, string>();
        vidCounter = 0;

        pmp = GetComponent<PlaylistMediaPlayer>();

        pmp.LoopMode = PlaylistMediaPlayer.PlaylistLoopMode.None;
        pmp.AutoCloseVideo = true;
        pmp.AutoProgress = false;
        pmp.DefaultTransition = PlaylistMediaPlayer.Transition.Zoom;
        pmp.DefaultTransitionDuration = 2.0f;
        pmp.DefaultTransitionEasing = PlaylistMediaPlayer.Easing.Preset.InOutCubic;

        // Query the playlist
        int playlistSize = pmp.Playlist.Items.Count();
        MediaItem currentItem = pmp.PlaylistItem;

        if (currentItem != null)
        {
            Debug.Log("Current media is at " + currentItem.mediaPath.GetResolvedFullPath());
        }

        int playlistItemIndex = pmp.PlaylistIndex;

        // Build the playlist

        foreach (JSONVideo infoEntry in videoInfo)
        {
            MediaItem item = new MediaItem
            {
                mediaPath = new MediaPath(infoEntry.vidUrl, MediaPathType.AbsolutePathOrURL),
                startMode = PlaylistMediaPlayer.StartMode.Manual,
                name = infoEntry.vidName
            };

            pmp.Playlist.Items.Add(item);

            if (videoName.text == "")
            {
                videoName.text = item.name;
            }

            previewLinks.Add(vidCounter++, infoEntry.thumbnailUrl);
        }

        videoGalleryManager.SetLinks(previewLinks, SwitchTo);
    }

    private List<JSONVideo> initDict()
    {
        List<JSONVideo> parsedInfo = new List<JSONVideo>();

        var info = Directory.GetFiles(@"Assets\!configs\", "*.json");
        foreach (string file in info)
        {
            string line = File.ReadAllText(file);
            parsedInfo.Add(JsonUtility.FromJson<JSONVideo>(line));
        }

        return parsedInfo;
    }

    private void SwitchTo(int idx)
    {
        if (pmp.CanJumpToItem(idx) && pmp.PlaylistIndex != idx)
        {
            pmp.JumpToItem(idx);
            videoName.text = pmp.PlaylistItem.name;
        }
    }
}
