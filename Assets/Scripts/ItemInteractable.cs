using UnityEngine;
using UnityEngine.UI;

public class ItemInteractable : MonoBehaviour , IInteractable
{
    public NewItem itemData;
    public GameObject interactOutline;
    private Sprite sprite;
    private float healAmount;
    private float maxHPIncreaseAmount;
    private float damageAmount;
    private string itemName;
    private SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

    }
    public bool canInteract()
    {
        return true;
    }

    public void interact()
    {
        if (!canInteract()) return;
        itemManager.sprite = sprite;
        itemManager.healAmount = healAmount;
        itemManager.maxHPIncreaseAmount = maxHPIncreaseAmount;
        itemManager.damageAmount = damageAmount;
        itemManager.newItem();
        unoutline();
        Destroy(gameObject);


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
