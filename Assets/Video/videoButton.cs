using UnityEngine;
using UnityEngine.UI;   // Needed for Image / Graphic

public class videoButton : MonoBehaviour
{
    public enum ActionType
    {
        Play,
        Pause,
        Replay
    }

    public ActionType action;

    [Header("Gaze Settings")]
    public float gazeTime = 1.5f;
    private float timer = 0f;

    private Camera cam;
    public float maxDistance = 3f;
    public LayerMask gazeLayer;

    [Header("Auto Assign")]
    public LanguageVideoPlayer targetPlayer;

    // UI reference to change Button color
    private Graphic buttonGraphic;

    void Start()
    {
        cam = Camera.main;

        // Auto find if missing
        if (targetPlayer == null)
            targetPlayer = FindObjectOfType<LanguageVideoPlayer>();

        if (targetPlayer == null)
            Debug.LogError("[GazeButton] No LanguageVideoPlayer found in scene!");

        // Get any UI graphic component (Image, RawImage, Text, etc.)
        buttonGraphic = GetComponent<Graphic>();

        Debug.Log("[GazeButton] Ready → " + gameObject.name + " | Action: " + action);
    }

    void Update()
    {
        if (targetPlayer == null) return;

        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        Debug.DrawRay(cam.transform.position, cam.transform.forward * maxDistance, Color.green);

        bool isLooking = false;

        if (Physics.Raycast(ray, out hit, maxDistance, gazeLayer))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isLooking = true;
            }
        }

        if (isLooking)
        {
            timer += Time.deltaTime;

            if (timer >= gazeTime)
            {
                TriggerAction();
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
    }

    void TriggerAction()
    {
        if (targetPlayer == null)
        {
            Debug.LogError("[GazeButton] No targetPlayer assigned!");
            return;
        }

        Debug.Log("[GazeButton] Triggered: " + action);

        // --- PERFORM ACTION ---
        switch (action)
        {
            case ActionType.Play:
                targetPlayer.PlayVideo();
                break;

            case ActionType.Pause:
                targetPlayer.PauseVideo();
                break;

            case ActionType.Replay:
                targetPlayer.ReplayVideo();
                break;
        }

        // --- CHANGE BUTTON COLOR TO GREEN ---
        if (buttonGraphic != null)
        {
            buttonGraphic.color = Color.green;
        }
    }
}
