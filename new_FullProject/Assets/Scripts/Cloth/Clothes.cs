using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个顺序是按照合成图片的层级循序定的，不变，后面会根据这个生成id
//最后的color不会合成图片，但因为要有，所以丢在最后
public enum ClothesType{
    Cloth_Back, Cloth_Mid, Cloth_Hair_Back, 
    Cloth_Body, Cloth_Tattoo, Cloth_Socks, 
    Cloth_Shoes1, Cloth_Ankle, Cloth_Pants, 
    Cloth_Hand, Cloth_Upper, Cloth_Belt, 
    Cloth_Neck, Cloth_Makeup, Cloth_Ear, 
    Cloth_Hair_Fore, Cloth_Head, Cloth_Pet, 
    Cloth_Effect,Cloth_Color
}


public class Clothes : MonoBehaviour
{
    public Sprite Icon_Gold;
    public Sprite Icon_Wood;
    public Sprite Icon_Water;
    public Sprite Icon_Fire;
    public Sprite Icon_Earth;
    public Sprite Icon_Default;

    public int clothesID;
    public ClothesType clothesType;

    public ElementType ElementType;
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
            ElementType = incomingType;
            amount += 1;
            return;
        }
        if (IsOvercoming(incomingType, ElementType))
        {
            amount -= 1;
            if (amount < 0)
            {
                amount = 0;
            }
        }
        else if (IsGenerating(ElementType, incomingType) || ElementType == incomingType)
        {
            amount += 1;
        }
    }


    //ID规则
    //20个部件，按层级排列，每个部件按 金木水火土排序
    //每个部件占用5个id，身体占25个id, 按丰均壮瘦胖排，每个体型按金木水火土
    // 丰均壮瘦胖=金木水火土,也就是01234
    //type也是按层级顺序排的，直接转化为Int即可

    //结算时调用，参数bodytype是结算时的体型
    public int GetCurrentClothesID(ElementType bodyType) {
        if ((int)clothesType > (int)ClothesType.Cloth_Body)
        {
            clothesID = (int)clothesType * 5 + (int)ElementType+ 20;
        }
        else if ((int)clothesType < (int)ClothesType.Cloth_Body)
        {
            clothesID = (int)clothesType * 5 + (int)ElementType;
        }
        else if ((int)clothesType == (int)ClothesType.Cloth_Body)
        {
            clothesID = (int)clothesType * 5 + (int)bodyType * 5 + (int)ElementType ;
        }
        return clothesID;
    }

    public Sprite GetSprite() {
        if(amount == 0)
        {
            return Icon_Default;
        }
        switch (ElementType)
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
