using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class HeldItemManager : MonoBehaviour
{
    public NewItem heldItemData = null;
    public Sprite sprite = null;
    public float healAmount = 0;
    public float maxHPIncreaseAmount = 0;
    public float damageAmount = 0;
    public Image heldItemImage;
    public bool hasItem = false;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private InteractionDetector interact;
    [SerializeField] private ParticleSystem useParticle;

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
            ParticleSystem.TextureSheetAnimationModule textureSheetModule = useParticle.textureSheetAnimation;
            textureSheetModule.RemoveSprite(0);
            textureSheetModule.AddSprite(sprite);
            useParticle.Play();

            player.speedBoostTime = heldItemData.speedBoostTime;
            player.playerHealth += healAmount;
            player.playerMaxHealth += maxHPIncreaseAmount;
            player.playerHealth -= damageAmount;
            if (healAmount > 0 || maxHPIncreaseAmount > 0)
            {
                SoundEffectManager.Play("HeartRegen", true);
            }
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
