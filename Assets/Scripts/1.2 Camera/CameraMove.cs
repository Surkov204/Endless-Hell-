using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool moveRight = true;

    void Update()
    {
        float direction = moveRight ? 1f : -1f;
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
    }
}