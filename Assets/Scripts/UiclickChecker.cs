using UnityEngine;
using UnityEngine.EventSystems;


public class UiclickChecker : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; 
            }

        }

    }
}//im lazy so i wont make this
