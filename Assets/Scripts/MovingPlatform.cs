using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MovementType
    {
        LeftRight,
        ForwardBackward,
        UpDown,
        RotateClockwise,
        RotateCounterClockwise,
        Diagonal,
        Circle
    }

    [Header("Movement Settings")]
    public MovementType movementType = MovementType.LeftRight;
    public float speed = 1f;
    public float distance = 5f;

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the platform
        timer += Time.deltaTime * speed;

        switch (movementType)
        {
            case MovementType.LeftRight:
                transform.position = startPosition + Vector3.right * Mathf.Sin(timer) * distance;
                break;

            case MovementType.ForwardBackward:
                transform.position = startPosition + Vector3.forward * Mathf.Sin(timer) * distance;
                break;

            case MovementType.UpDown:
                transform.position = startPosition + Vector3.up * Mathf.Sin(timer) * distance;
                break;

            case MovementType.RotateClockwise:
                transform.Rotate(Vector3.up, speed * 10f * Time.deltaTime);
                break;

            case MovementType.RotateCounterClockwise:
                transform.Rotate(Vector3.up, -speed * 10f * Time.deltaTime);
                break;

            case MovementType.Diagonal:
                float x = Mathf.Sin(timer) * distance;
                float z = Mathf.Cos(timer) * distance;
                transform.position = startPosition + new Vector3(x, 0, z);
                break;

            case MovementType.Circle:
                float circleX = Mathf.Cos(timer) * distance;
                float circleZ = Mathf.Sin(timer) * distance;
                transform.position = startPosition + new Vector3(circleX, 0, circleZ);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Make player child of platform
            other.transform.SetParent(transform);
            Debug.Log("Player ON platform: " + gameObject.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Remove parent
            other.transform.SetParent(null);
            Debug.Log("Player OFF platform: " + gameObject.name);
        }
    }
}