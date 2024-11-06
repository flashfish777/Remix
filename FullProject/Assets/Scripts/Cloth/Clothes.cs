using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clothes : MonoBehaviour
{
    public Sprite Icon_Gold;
    public Sprite Icon_Wood;
    public Sprite Icon_Water;
    public Sprite Icon_Fire;
    public Sprite Icon_Earth;
    public Sprite Icon_Default;

    public int ClothesPositionID;

    public ElementType Type;
    public int amount = 0;
    
    // 五行相生相克规则
    private bool IsGenerating(ElementType source, ElementType target)
    {
        // 根据五行相生规则：木生火，火生土，土生金，金生水，水生木
        return (source == ElementType.Wood && target == ElementType.Fire) ||
               (source == ElementType.Fire && target == ElementType.Earth) ||
               (source == ElementType.Earth && target == ElementType.Gold) ||
               (source == ElementType.Gold && target == ElementType.Water) ||
               (source == ElementType.Water && target == ElementType.Wood);
    }

    private bool IsOvercoming(ElementType source, ElementType target)
    {
        // 根据五行相克规则：金克木，木克土，土克水，水克火，火克金
        return (source == ElementType.Gold && target == ElementType.Wood) ||
               (source == ElementType.Wood && target == ElementType.Earth) ||
               (source == ElementType.Earth && target == ElementType.Water) ||
               (source == ElementType.Water && target == ElementType.Fire) ||
               (source == ElementType.Fire && target == ElementType.Gold);
    }

    public void addElement(ElementType incomingType)
    {
        if (amount == 0)
        {
            Type = incomingType;
            amount += 1;
            return;
        }
        if (IsOvercoming(incomingType, Type))
        {
            amount -= 1;
            if (amount < 0)
            {
                amount = 0;
            }
        }
        else if (IsGenerating(Type, incomingType) || Type == incomingType)
        {
            amount += 1;
        }
    }

    public Sprite GetSprite() {
        if(amount == 0)
        {
            return Icon_Default;
        }
        switch (Type)
        {
            case ElementType.Wood:
                return Icon_Wood;

            case ElementType.Fire:
                return Icon_Fire;

            case ElementType.Earth:
                return Icon_Earth;

            case ElementType.Gold:
                return Icon_Gold;

            case ElementType.Water:
                return Icon_Water;

            default:
                return Icon_Default;
        }
    }
}
