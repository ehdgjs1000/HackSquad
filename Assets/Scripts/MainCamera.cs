using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class MainCamera : MonoBehaviour
{
    CinemachineCamera cam;
    
    [SerializeField] Transform targetTr;
    [SerializeField] float posX, posY, posZ;

    [SerializeField] float scrollSpeed = 2000f;

    private void Awake()
    {
        cam = GetComponent<CinemachineCamera>();
    }
    private void Update()
    {
        ScrollCam();
    }
    private void FixedUpdate()
    {
        this.transform.position = targetTr.position + new Vector3(posX,posY,posZ);
    }
    private void ScrollCam()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if(cam.Lens.FieldOfView > 25.0f && scrollWheel <= 0.0f)
        {
            cam.Lens.FieldOfView += scrollWheel * Time.deltaTime * scrollSpeed;
        }else if (cam.Lens.FieldOfView < 80.0f && scrollWheel >= 0.0f)
        {
            cam.Lens.FieldOfView += scrollWheel * Time.deltaTime * scrollSpeed;
        }
    }


}
