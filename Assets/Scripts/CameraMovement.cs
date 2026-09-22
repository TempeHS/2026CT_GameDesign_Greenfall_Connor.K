using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame

    void LateUpdate()
    {
        float offsetX = player.transform.position.x - transform.position.x;
        float offsetY = player.transform.position.y - transform.position.y;
        
        transform.Translate(Vector3.right * offsetX * Time.deltaTime*7);
        transform.Translate(Vector3.up * offsetY * Time.deltaTime *7);
        //if(transform.position.y < 2.0f)
        //{
        //    transform.position = new Vector3(transform.position.x, 2.0f, transform.position.z);
        //}
        float clampedX = Mathf.Clamp(transform.position.x, -1000, 1000);
        float clampedY = Mathf.Clamp(transform.position.y, -2, 1000);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
