using UnityEngine;

public class ItemBob : MonoBehaviour
{
    private Vector2 startPosition;
    [SerializeField] private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(startPosition.x, startPosition.y + (0.33f * Mathf.Sin(2.0f*Time.realtimeSinceStartup)));
    }
}
