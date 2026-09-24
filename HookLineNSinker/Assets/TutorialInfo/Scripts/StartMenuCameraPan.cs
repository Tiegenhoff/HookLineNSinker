using UnityEngine;

public class StartMenuCameraPan : MonoBehaviour
{
    public Transform mainCamera;
    public float panSpeed = 100f;
    public GameObject sideHUD;
    private Vector3 targetPosition;
    private bool isPanning = false;
    void Start()
    {
        // Remember the camera's gameplay position.
        targetPosition = mainCamera.position;
        // Move the camera to the underwater menu position.
        mainCamera.position = transform.position;
        // Keep the gameplay HUD hidden during the menu.
        sideHUD.SetActive(false);
    }
    void Update()
    {
        if (!isPanning)
            return;

        mainCamera.position = Vector3.MoveTowards(
            mainCamera.position,
            targetPosition,
            panSpeed * Time.deltaTime
        );
        // The camera has finished moving.
        if (Vector3.Distance(mainCamera.position, targetPosition) < 0.01f)
        {
            mainCamera.position = targetPosition;
            isPanning = false;
            sideHUD.SetActive(true);
        }
    }
    public void StartPan()
    {
        isPanning = true;
    }
}