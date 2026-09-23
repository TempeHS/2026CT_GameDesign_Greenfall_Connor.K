using UnityEngine;

public class RobotEnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerMovement Player;
    [SerializeField] private Animator animator;

    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDir = 1;
    [SerializeField] private bool stayOnLedges = true;
    [SerializeField] private ParticleSystem RobotDeath;
    [SerializeField] private ParticleSystem Sparks;
    [SerializeField] private ParticleSystem DamageSparks;
    [SerializeField] private GameObject HealItem;
    [SerializeField] private GameObject SpeedItem;
    private Vector2 attackCheckSize = new Vector2(1.5f, 1.4f);


    private int curentDir;
    private float halfWidth;
    private float halfHeight;
    private Vector2 movement;
    private bool isFacingRight = false;
    private bool isGrounded;
    private bool seePlayer;
    private float atkChargeTime;
    public GameObject robotAttackBox;
    public float enemyHealth = 3.0f;
    public float enemyKBTime = 0.0f;
    public bool isAlive = true;
    private bool hasPlayedSound = false;
    private Vector2 itemSpawnPos;
    private float iFrames;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
        curentDir = startDir;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (enemyHealth<=0.0f){
            RobotDeath.transform.position = rb.transform.position;
            RobotDeath.Play();  
            Sparks.transform.position = rb.transform.position;
            Sparks.Play();

            if (Random.Range(1, 100) > 70)
            {
                if(Random.Range(0,3) == 1)
                {
                    itemSpawnPos = new Vector2(transform.position.x, transform.position.y + 1);
                    Instantiate(HealItem, itemSpawnPos, transform.rotation);
                }
                else
                {
                    itemSpawnPos = new Vector2(transform.position.x, transform.position.y + 1);
                    Instantiate(SpeedItem, itemSpawnPos, transform.rotation);
                }
                
            }
            
            isAlive = false;
            gameObject.SetActive(false); 
            

        }
        atkChargeTime -= Time.deltaTime;
        enemyKBTime -= Time.deltaTime;
        iFrames -= Time.deltaTime;
        if (atkChargeTime < 0 && enemyKBTime<=0.0f)
        {
            movement.x = speed * curentDir;
        }
        else
        {
            movement.x = 0.0f;
        }
        
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity =  movement;
        SetDir();

        if (atkChargeTime<0.2f && atkChargeTime >0.1f)
        {
            if (!hasPlayedSound) 
            {
                SoundEffectManager.Play("RobotAttack", true);
                hasPlayedSound = true;
            }

            

            robotAttackBox.SetActive(true);
        }
        else
        {
            robotAttackBox.SetActive(false);
        }
        
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("OneWayPlatform"))
        {
            isGrounded=true;

        }
        else{
            //isGrounded=false;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        
        //isGrounded=false;

    }
    private void SetDir()
    {
        if(!isGrounded) return;
        if (atkChargeTime>0)
        {
            return;
        }
        Vector2 rightPos = transform.position;
        Vector2 leftPos = transform.position;
        rightPos.x += halfWidth;
        leftPos.x -= halfWidth;


        if (CheckEnemies())
        {
            if(transform.position.x > Player.transform.position.x)
            {
                curentDir = -1;
                atkChargeTime = 0.7f;
                animator.SetTrigger("attack");
                hasPlayedSound=false;
            }
            if (transform.position.x < Player.transform.position.x)
            {
                curentDir = 1;
                atkChargeTime = 0.7f;
                animator.SetTrigger("attack");
                hasPlayedSound = false;

            }
            //stayOnLedges = false;

        }
        else
        {
            stayOnLedges = true;
        }
        if (rb.linearVelocity.x > 0){

            //if (Physics2D.Raycast(transform.position, Vector2.right, 0.7f, LayerMask.GetMask("Player")))
            //{
            //    atkChargeTime = 0.7f;
            //    animator.SetTrigger("attack");

            //}
            if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground"))){
            curentDir *=-1;
            // spriteRenderer.flipx = true;
            }
            else if(stayOnLedges && !Physics2D.Raycast(rightPos, Vector2.down, halfHeight +0.1f, LayerMask.GetMask("Ground"))){
                curentDir *=-1;
            }
            

        } 
        else if(rb.linearVelocity.x < 0){

            //if (Physics2D.Raycast(transform.position, Vector2.left, 0.7f, LayerMask.GetMask("Player")))
            //{
            //    atkChargeTime = 0.7f;
            //    animator.SetTrigger("attack");
            //}
            if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")) ){
            curentDir *=-1;
            // spriteRenderer.flipx = false;
            }
            else if(stayOnLedges && !Physics2D.Raycast(leftPos, Vector2.down, halfHeight +0.1f, LayerMask.GetMask("Ground"))){
                curentDir *=-1;
            }
            

        }
        flip();

    }
     private void flip()
    {
        if ((isFacingRight && curentDir < 0f || !isFacingRight && curentDir > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }
    private bool CheckEnemies()
    {
        return Physics2D.OverlapBox(transform.position, attackCheckSize, 0.0f, LayerMask.GetMask("Player"));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 contactPoint = other.ClosestPoint(transform.position);
        Vector2 pushDirection = ((Vector2)transform.position - contactPoint);
        pushDirection.x = GetDirection(contactPoint);
        PlayerDamageTags player = other.gameObject.GetComponent<PlayerDamageTags>();
        if (player != null)
        {
            if (iFrames <= 0.0f)
            {
                enemyHealth -= player.damage;
                if (player.flashRed)
                {
                    DamageSparks.transform.position = rb.transform.position;
                    DamageSparks.Play();
                    animator.SetTrigger("flashRed");
                }

                if (player.kbAmount > 0)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.AddForce(pushDirection * player.kbAmount * 1.5f, ForceMode2D.Impulse);
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + 2);
                }
                if (player.willStun)
                {
                    enemyKBTime = 0.3f;
                }
                iFrames = 0.5f;
            }

        }
    }
    private int GetDirection(Vector2 collider)
    {
        if(transform.position.x > collider.x)
        {
            return 1;
        }
        if (transform.position.x < collider.x)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
