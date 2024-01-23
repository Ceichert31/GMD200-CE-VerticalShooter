using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;

    [SerializeField] private Transform endPoint;

    [SerializeField] private bool isEnabled;
    private void Update()
    {
        if (!isEnabled)
            return;

        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, scrollSpeed);
    }
}
