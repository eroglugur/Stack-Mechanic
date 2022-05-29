using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    void Update()
    {
        transform.position = player.position + offset;
    }
}
