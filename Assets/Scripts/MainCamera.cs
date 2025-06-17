using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class MainCamera : MonoBehaviour
{
    CinemachineCamera cam;
    
    [SerializeField] Transform targetTr;
    [SerializeField] float posX, posY, posZ;

    private void Awake()
    {
        cam = GetComponent<CinemachineCamera>();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        this.transform.position = targetTr.position + new Vector3(posX,posY,posZ);
    }


}
