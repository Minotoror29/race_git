using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera _currentCam;

    public void CameraTransition(CinemachineVirtualCamera nextCam)
    {
        _currentCam?.gameObject.SetActive(false);
        _currentCam = nextCam;
        _currentCam.gameObject.SetActive(true);
    }
}
