using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class HeldItemManager : MonoBehaviour
{
    public Sprite sprite = null;
    public float healAmount = 0;
    public float maxHPIncreaseAmount = 0;
    public float damageAmount = 0;
    public Image heldItemImage;
    private bool hasItem = false;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private InteractionDetector interact;

    void Start()
    {
        heldItemImage.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.performed && hasItem)
        {
            player.playerHealth += healAmount;
            player.playerMaxHealth += maxHPIncreaseAmount;
            player.playerHealth -= damageAmount;
            sprite = null;
            healAmount = 0;
            maxHPIncreaseAmount = 0;
            damageAmount = 0;
            hasItem = false;
            heldItemImage.enabled = false;
            interact.interactableInRange = null;
            interact.interactableObject = null;
            interact.interactableInRangeDist = 999999.9999f;
        }
    }
    public void newItem()
    {
        heldItemImage.enabled = true;
        heldItemImage.sprite = sprite;
        hasItem= true;
    }
}
