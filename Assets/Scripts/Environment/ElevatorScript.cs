using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] private float maxHeight = 5f;
    [SerializeField] private float minHeight = 0f;
    [SerializeField] private float speed = 1f;

    private Rigidbody rb;
    private int direction = 0;
    private bool isMoving = false;
    private Vector3 targetPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveElevator();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isMoving)
            {
                direction = 1;
                isMoving = true;
                rb.isKinematic = false;
                StartCoroutine(MoveToHeight());
            }

            targetPosition.y = maxHeight;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isMoving)
            {
                direction = -1;
                isMoving = true;
                StartCoroutine(MoveToHeight());
            }

            targetPosition.y = minHeight;
        }
    }

    private void MoveElevator()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition)
        {
            isMoving = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    private IEnumerator MoveToHeight()
    {
        float step = speed * Time.deltaTime;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            yield return null;
        }

        isMoving = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
}
