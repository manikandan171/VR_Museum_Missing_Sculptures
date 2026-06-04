using UnityEngine;
using UnityEngine.SceneManagement;

public class GazeSceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public float gazeTime = 1.5f;
    private float timer = 0f;

    private Camera cam;
    public float maxDistance = 2f;
    public LayerMask gazeLayer;

    void Start()
    {
        cam = Camera.main;
        Debug.Log("[GazeSceneLoader] Initialized. Watching object: " + gameObject.name);
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        // DEBUG: See the ray in Scene view
        Debug.DrawRay(cam.transform.position, cam.transform.forward * maxDistance, Color.red);

        bool isLooking = false;

        if (Physics.Raycast(ray, out hit, maxDistance, gazeLayer))
        {
            Debug.Log("[GazeSceneLoader] Ray HIT: " + hit.collider.name);

            if (hit.collider.gameObject == gameObject ||
                hit.collider.transform.IsChildOf(transform))
            {
                Debug.Log("[GazeSceneLoader] LOOKING AT BUTTON: " + gameObject.name);
                isLooking = true;
            }
            else
            {
                Debug.Log("[GazeSceneLoader] Ray hit something else: " + hit.collider.name);
            }
        }
        else
        {
            Debug.Log("[GazeSceneLoader] RAY HITTING NOTHING");
        }

        if (isLooking)
        {
            timer += Time.deltaTime;
            Debug.Log("[GazeSceneLoader] Timer: " + timer.ToString("F2"));

            if (timer >= gazeTime)
            {
                Debug.Log("[GazeSceneLoader] Gaze triggered! Loading scene → " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else
        {
            timer = 0f;
        }
    }
}
