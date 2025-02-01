using UnityEngine;

public class HealthBarAligner_sc : MonoBehaviour
{
    void Start()
    {
        HealthBarFaceCamera();
    }

    private void HealthBarFaceCamera()
    {
        // Calculate the rotation to face the camera
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            var rotation = Quaternion.LookRotation(Camera.main.transform.up, -Camera.main.transform.forward);
            transform.rotation = rotation;
        }
    }
}
