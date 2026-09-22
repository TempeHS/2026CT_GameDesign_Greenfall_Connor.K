using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    private List<GameObject> currentOneWayPlatforms = new List<GameObject>();
    private List<GameObject> oldCurrentOneWayPlatforms = new List<GameObject>();
    private GameObject currentOneWayPlatform;
    [SerializeField] private CapsuleCollider2D playerCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            if (currentOneWayPlatforms.Count != 0) 
            {
                StartCoroutine(DisableCollision());
            }

        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        { 
            currentOneWayPlatform = collision.gameObject;
            currentOneWayPlatforms.Add(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            if (currentOneWayPlatforms.Contains(collision.gameObject))
            {
                int indexNumber = currentOneWayPlatforms.IndexOf(collision.gameObject);
                GameObject currentObject = currentOneWayPlatforms[indexNumber];
                BoxCollider2D platformCollider = currentObject.GetComponent<BoxCollider2D>();
                currentOneWayPlatforms.Remove(collision.gameObject);
                //Physics2D.IgnoreCollision(playerCollider, platformCollider, false);

            }

            currentOneWayPlatform = null;

        }
    }

    private IEnumerator DisableCollision()
    {
        oldCurrentOneWayPlatforms = new List<GameObject>(currentOneWayPlatforms);
        //BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        for (int i = 0; i < currentOneWayPlatforms.Count; i++)
        {
            GameObject currentObject = currentOneWayPlatforms[i];
            BoxCollider2D platformCollider = currentObject.GetComponent<BoxCollider2D>();
            


            Physics2D.IgnoreCollision(playerCollider, platformCollider);
        }

        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < oldCurrentOneWayPlatforms.Count; i++)
        {
            GameObject currentObject = oldCurrentOneWayPlatforms[i];
            BoxCollider2D platformCollider = currentObject.GetComponent<BoxCollider2D>();


            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
        

    }
}
