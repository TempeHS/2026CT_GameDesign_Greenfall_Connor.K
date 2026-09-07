using UnityEngine;

public class MenuParralax : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float length;
    private Vector2 screenPos = Vector2.zero;
    private float screenRes;
    private float screenResX;
    private float screenResY;
    private RectTransform rectTransform;
    private Vector2 startPos;


    public float parralaxEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        //startPosX =transform.position.x;
        //startPosY = transform.position.y;
        //screenPos = Vector2.zero;
        //screenResX = Screen.width;
        //screenResY = Screen.height;
        //screenRes = Screen.width + Screen.height;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 mousePosition = Input.mousePosition;

        float mouseX = (mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (mousePosition.y / Screen.height) - 0.5f;

        float moveX = mouseX * Screen.width * parralaxEffect;
        float moveY = mouseY * Screen.height * parralaxEffect;

        rectTransform.anchoredPosition = new Vector2(
            startPos.x + moveX,
            startPos.y + moveY
        );
        //    Debug.Log(screenRes + (Screen.width + Screen.height));
        //    if(screenRes != (Screen.width + Screen.height))
        //    {
        //        //transform.position = new Vector3((((startPosX/screenResX)*Screen.width) +(Screen.width / 2))/100,(((startPosY / screenResY) * Screen.height) + (Screen.height / 2))/100, transform.position.z);
        //        //screenPos = new Vector2((startPosX / screenResX) * Screen.width ,(startPosY / screenResY) * Screen.height );
        //        startPosX = (startPosX / screenResX) * Screen.width/100;
        //        startPosY = (startPosY / screenResY) * Screen.height/100;
        //        screenPos = Vector2.zero;
        //        screenRes = Screen.width + Screen.height;

        //    }


        //    //vv- 0=move with cam / 1=wont move / 0.5=half 
        //    screenPos = Input.mousePosition;
        //    float distX = (screenPos.x - (Screen.width/2) )* parralaxEffect;
        //    float distY = (screenPos.y - (Screen.height / 2) ) * parralaxEffect;
        //    float movement = screenPos.x * (1 - parralaxEffect);


        //    transform.position = new Vector3(startPosX + distX, startPosY + distY,transform.position.z);

        //    // if (movement > startPosX + length)
        //    // {
        //    //     startPosX += length;
        //    // }
        //    // else if (movement < startPosX - length) 
        //    // {
        //    //     startPosX -= length;
        //    // }
        }
    }
