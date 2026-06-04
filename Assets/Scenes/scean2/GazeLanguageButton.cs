using UnityEngine;

public class GazeLanguageButton : MonoBehaviour
{
    public enum ActionType
    {
        English,
        Hindi,
        Tamil,
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
    public LanguageAudioPlayer targetPlayer;

    void Start()
    {
        cam = Camera.main;

        // Auto find if missing
        if (targetPlayer == null)
            targetPlayer = FindObjectOfType<LanguageAudioPlayer>();

        if (targetPlayer == null)
            Debug.LogError("[GazeButton] No LanguageAudioPlayer found in scene!");

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

        switch (action)
        {
            case ActionType.English:
                targetPlayer.SelectEnglish();
                break;

            case ActionType.Hindi:
                targetPlayer.SelectHindi();
                break;

            case ActionType.Tamil:
                targetPlayer.SelectTamil();
                break;

            case ActionType.Play:
                targetPlayer.PlayAudio();
                break;

            case ActionType.Pause:
                targetPlayer.PauseAudio();
                break;

            case ActionType.Replay:
                targetPlayer.ReplayAudio();
                break;
        }
    }
}
