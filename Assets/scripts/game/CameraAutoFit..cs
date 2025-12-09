using UnityEngine;

public class CameraAutoFit : MonoBehaviour
{
    public float baseOrthographicSize = 5f;   
    public float baseAspect = 16f / 9f;       

    void Start()
    {
        AdjustCamera();
    }

    void Update()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        Camera.main.orthographicSize = baseOrthographicSize * (baseAspect / currentAspect);
    }
}
