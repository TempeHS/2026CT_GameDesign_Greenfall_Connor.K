using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public bool isActive;

    public float lifetime = 5.0f;
    public Vector3 realDirection;
    public Rigidbody2D rb;
    public float bulletSpeed = 5;
    public bool hasMoved = false;

    void Start()
    {
        

        rb = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {

        if (!isActive) return;
        if (!hasMoved)
        {
            rb.linearVelocity = transform.right * bulletSpeed;
            hasMoved = true;
        }
        lifetime -= Time.deltaTime;

        if(lifetime<= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return;

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
