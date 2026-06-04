using UnityEngine;
using UnityEngine.Video;

public class LanguageVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
            Debug.LogError("[LanguageVideoPlayer] No VideoPlayer found!");

        // Optional: Load first frame without auto-playing
        videoPlayer.Play();
        videoPlayer.Pause();
    }

    // ▶ PLAY
    public void PlayVideo()
    {
        if (videoPlayer == null) return;

        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
            Debug.Log("[VideoPlayer] PLAY");
        }
    }

    // ⏸ PAUSE
    public void PauseVideo()
    {
        if (videoPlayer == null) return;

        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            Debug.Log("[VideoPlayer] PAUSE");
        }
    }

    // 🔁 REPLAY
    public void ReplayVideo()
    {
        if (videoPlayer == null) return;

        videoPlayer.Stop();
        videoPlayer.time = 0f;
        videoPlayer.Play();
        Debug.Log("[VideoPlayer] REPLAY");
    }
}
