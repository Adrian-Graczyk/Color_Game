using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] private float maxHeight = 5f;
    [SerializeField] private float minHeight = 0f;
    [SerializeField] private float speed = 1f;

    private bool isPlayerOnElevator = false;
    private Vector3 targetPosition;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isPlayerOnElevator)
        {
            MoveElevator(maxHeight);
        }
        else
        {
            MoveElevator(minHeight);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnElevator = false;
        }
    }

    private void MoveElevator(float targetHeight)
    {
        float step = speed * Time.deltaTime;
        targetPosition.y = targetHeight;
        rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, step));
    }
}
