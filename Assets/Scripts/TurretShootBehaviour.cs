using UnityEngine;

public class TurretShootBehaviour : MonoBehaviour
{
    public float shootcd;
    private float shootcdReal;
    public GameObject bulletObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootcdReal -= Time.deltaTime;
        if (shootcdReal <= 0.0f)
        {
            GameObject clone = Instantiate(bulletObject, transform.position, transform.rotation * Quaternion.Euler(0, 0, 180f));
            BulletMovement cloneScript = clone.GetComponent<BulletMovement>();
            cloneScript.isActive = true;
            cloneScript.chargeTime = shootcd/2;
            cloneScript.maxChargeTime = shootcd / 2;

            shootcdReal = shootcd;
        }

    }
}
