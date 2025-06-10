using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 offset = new Vector3(0f, 8f, -8f);
    private Quaternion fixedRotation = Quaternion.Euler(40f, 0f, 0f);

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;
        transform.rotation = fixedRotation;
    }
}
