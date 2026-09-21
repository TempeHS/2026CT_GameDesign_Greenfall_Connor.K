using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public bool isActive;

    public float lifetime = 5.0f;
    public Vector3 realDirection;
    public Rigidbody2D rb;
    public float bulletSpeed = 5;
    public bool hasMoved = false;
    public float ballDeathCd;
    private bool hasDied = false;
    public float maxChargeTime;
    public float chargeTime;
    private Vector2 size = new Vector2(0f, 0f);
    private Vector2 maxSize = new Vector2(1.5f, 1.5f);
    [SerializeField] private Animator animator;

    void Start()
    {
        

        rb = GetComponent<Rigidbody2D>();
        


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(chargeTime);
        if (!isActive) return;
        if (chargeTime > 0)
        {
            float progress = 1f - (chargeTime / maxChargeTime);


            Vector2 current2DSize = Vector2.Lerp(Vector2.zero, maxSize, progress);

            
            transform.localScale = new Vector3(current2DSize.x, current2DSize.y, 1f);


            chargeTime -= Time.deltaTime;
            return;
            //size = Vector2.Lerp(size, maxSize, 1 - (chargeTime / maxChargeTime));
            //chargeTime -= Time.deltaTime;
            //transform.localScale = size;
            //return;
        }
        

        
        if (!hasMoved)
        {
            rb.linearVelocity = transform.right * bulletSpeed;
            hasMoved = true;
        }
        lifetime -= Time.deltaTime;
        ballDeathCd -= Time.deltaTime;
        if (ballDeathCd <= 0 && hasDied)
        {
            Destroy(gameObject);
        }else if(ballDeathCd <= 2 && hasDied)
        {
            GetComponent<HazardTagApplier>().enabled = false;
        }
        if (lifetime<= 0)
        {
            
            
            if (!hasDied) 
            {
                ballDeathCd = 0.3f;
                rb.linearVelocity = Vector2.zero;
                animator.SetTrigger("BallDeath");
                hasDied = true;
            }
            
            
            
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.CompareTag("Ground"))
            {
                if (!hasDied)
                {
                    ballDeathCd = 0.3f;
                    rb.linearVelocity = Vector2.zero;
                    animator.SetTrigger("BallDeath");
                    hasDied = true;
                }
            }
        



    }
}
