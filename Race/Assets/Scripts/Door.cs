using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ShipController>().CurrentSpeed >= 100f)
        {
            Debug.Log("Activate");
        }
    }
}
