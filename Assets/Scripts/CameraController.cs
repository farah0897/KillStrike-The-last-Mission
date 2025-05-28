using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target; 

    private float StartFov, targetFov; 
        
        public float zoomSpeed =1f;

    public Camera theCam;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartFov = theCam.fieldOfView;
        targetFov = StartFov; 
    }

    // Update is called once per frame
    
   void LateUpdate()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;

        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFov, zoomSpeed * Time.deltaTime);
        }

    public void ZoomIN(float newZoom)
    {
        targetFov = newZoom;
    }
    
    public void ZoomOUT()
    {
        targetFov = StartFov; 
    }



}

