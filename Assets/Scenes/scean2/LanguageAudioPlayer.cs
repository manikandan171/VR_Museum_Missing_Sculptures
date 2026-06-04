using UnityEngine;
using UnityEngine.UI;

public class LanguageAudioPlayer : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Audio Clips")]
    public AudioClip englishAudio;
    public AudioClip hindiAudio;
    public AudioClip tamilAudio;

    [Header("Language Buttons")]
    public Button englishButton;
    public Button hindiButton;
    public Button tamilButton;

    [Header("Control Buttons")]
    public Button playButton;
    public Button pauseButton;
    public Button replayButton;

    private string currentLanguage = "";

    // -----------------------------------
    // RESET BUTTON COLORS (LOCAL ONLY)
    // -----------------------------------
    private void ResetAllColors()
    {
        Color defaultColor = Color.white;

        if (englishButton != null) englishButton.image.color = defaultColor;
        if (hindiButton != null) hindiButton.image.color = defaultColor;
        if (tamilButton != null) tamilButton.image.color = defaultColor;

        if (playButton != null) playButton.image.color = defaultColor;
        if (pauseButton != null) pauseButton.image.color = defaultColor;
        if (replayButton != null) replayButton.image.color = defaultColor;
    }

    private void Highlight(Button btn)
    {
        ResetAllColors();
        if (btn != null) btn.image.color = Color.green;
    }

    // -----------------------------------
    // LANGUAGE SELECTION
    // -----------------------------------
    public void SelectEnglish()
    {
        Highlight(englishButton);
        currentLanguage = "EN";
        PlaySelectedLanguage();
    }

    public void SelectHindi()
    {
        Highlight(hindiButton);
        currentLanguage = "HI";
        PlaySelectedLanguage();
    }

    public void SelectTamil()
    {
        Highlight(tamilButton);
        currentLanguage = "TA";
        PlaySelectedLanguage();
    }

    private void PlaySelectedLanguage()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("[LanguageAudioPlayer] AudioSource is null");
            return;
        }

        audioSource.Stop();

        if (currentLanguage == "EN")
            audioSource.clip = englishAudio;
        else if (currentLanguage == "HI")
            audioSource.clip = hindiAudio;
        else if (currentLanguage == "TA")
            audioSource.clip = tamilAudio;
        else
            audioSource.clip = null;

        if (audioSource.clip != null)
            audioSource.Play();
    }

    // -----------------------------------
    // AUDIO CONTROLS
    // -----------------------------------
    public void PlayAudio()
    {
        Highlight(playButton);

        if (audioSource != null && audioSource.clip != null)
            audioSource.Play();
    }

    public void PauseAudio()
    {
        Highlight(pauseButton);

        if (audioSource != null && audioSource.isPlaying)
            audioSource.Pause();
    }

    public void ReplayAudio()
    {
        Highlight(replayButton);

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Stop();
            audioSource.time = 0f;
            audioSource.Play();
        }
    }
}
   