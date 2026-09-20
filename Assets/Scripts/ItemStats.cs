using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item Things")]
public class NewItem : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public float itemHeal = 0.0f;
    public float itemAddMaxHPAmount = 0.0f;
    public float itemDamage = 0.0f;
    public float speedBoostTime = 0.0f;
    public Sprite itemOutlineSprite;

}