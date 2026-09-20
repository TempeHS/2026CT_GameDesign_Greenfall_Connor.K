using UnityEngine;
using UnityEngine.UI;

public class ItemInteractable : MonoBehaviour , IInteractable
{
    public NewItem itemData;
    public GameObject interactOutline;
    private Sprite sprite;
    private Sprite outlineSprite;
    private float healAmount;
    private float maxHPIncreaseAmount;
    private float damageAmount;
    private string itemName;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer outlineSpriteRenderer;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private InteractionDetector id;
    [SerializeField] private HeldItemManager itemManager;
    private void Awake()
    {
        sprite = itemData.itemSprite;
        healAmount = itemData.itemHeal;
        maxHPIncreaseAmount = itemData.itemAddMaxHPAmount;
        itemName= itemData.itemName;
        damageAmount = itemData.itemDamage;
        outlineSprite = itemData.itemOutlineSprite;
        outlineSpriteRenderer = interactOutline.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        outlineSpriteRenderer.sprite = outlineSprite;



    }
    public bool canInteract()
    {
        return true;
    }

    public void interact()
    {
        if (!canInteract()) return;

        if (itemManager.hasItem == true)
        {
            NewItem tempItemData = itemData;
            itemData = itemManager.heldItemData;

            
            sprite = itemData.itemSprite;
            healAmount = itemData.itemHeal;
            maxHPIncreaseAmount = itemData.itemAddMaxHPAmount;
            itemName = itemData.itemName;
            damageAmount = itemData.itemDamage;
            outlineSpriteRenderer.sprite = itemData.itemOutlineSprite;
            spriteRenderer.sprite = sprite;

            
            itemManager.sprite = tempItemData.itemSprite;
            itemManager.healAmount = tempItemData.itemHeal;
            itemManager.maxHPIncreaseAmount = tempItemData.itemAddMaxHPAmount;
            itemManager.damageAmount = tempItemData.itemDamage;

            itemManager.heldItemData = tempItemData;
            itemManager.newItem();
            id.interactableInRange = null;
            id.interactableObject = null;
            id.interactableInRangeDist = 999999.9999f;
            

        }
        else
        {

            itemManager.sprite = sprite;
            itemManager.healAmount = healAmount;
            itemManager.maxHPIncreaseAmount = maxHPIncreaseAmount;
            itemManager.damageAmount = damageAmount;
            itemManager.heldItemData = itemData;
            itemManager.newItem();
            id.interactableInRange = null;
            id.interactableObject = null;
            id.interactableInRangeDist = 999999.9999f;
            unoutline();
            Destroy(gameObject);
        }

    }

    public void switchItems(NewItem newData)
    {

    }

    public void outline()
    {
        interactOutline.SetActive(true);
    }
    public void unoutline()
    {
        interactOutline.SetActive(false);
    }

}
