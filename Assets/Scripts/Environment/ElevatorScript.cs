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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveElevator();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isMoving)
            {
                direction = 1;
                isMoving = true;
                rb.isKinematic = false;
                StartCoroutine(MoveToHeight(maxHeight));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isMoving)
            {
                direction = -1;
                isMoving = true;
                StartCoroutine(MoveToHeight(minHeight));
            }
        }
    }

    private void MoveElevator()
    {
        float targetHeight = direction == 1 ? maxHeight : minHeight;
        float step = speed * Time.deltaTime;
        Vector3 targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position.y == targetHeight)
        {
            isMoving = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    private IEnumerator MoveToHeight(float targetHeight)
    {
        float step = speed * Time.deltaTime;
        Vector3 targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
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
