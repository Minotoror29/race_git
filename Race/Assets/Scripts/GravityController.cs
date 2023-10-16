using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityController : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private Transform _gravityCenter;
    [SerializeField] private float gravityForce = 1f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    private void Gravity()
    {
        _rb.AddForce(-transform.up * gravityForce);
    }

    private void Rotate()
    {
        Vector3 targetDir = (_gravityCenter.position + transform.position).normalized;
        Vector3 bodyUp = transform.up;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(bodyUp, targetDir) * transform.rotation, 5);
    }
}
