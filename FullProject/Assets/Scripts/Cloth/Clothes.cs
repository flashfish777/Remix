using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���˳���ǰ��պϳ�ͼƬ�Ĳ㼶ѭ�򶨵ģ����䣬���������������id
//����color����ϳ�ͼƬ������ΪҪ�У����Զ������
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
    
    // ����������˹���
    private bool IsGenerating(ElementType source, ElementType target)
    {
        // ����������������ľ���𣬻������������𣬽���ˮ��ˮ��ľ
        return (source == ElementType.Wood && target == ElementType.Fire) ||
               (source == ElementType.Fire && target == ElementType.Earth) ||
               (source == ElementType.Earth && target == ElementType.Gold) ||
               (source == ElementType.Gold && target == ElementType.Water) ||
               (source == ElementType.Water && target == ElementType.Wood);
    }

    private bool IsOvercoming(ElementType source, ElementType target)
    {
        // ����������˹��򣺽��ľ��ľ����������ˮ��ˮ�˻𣬻�˽�
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


    //ID����
    //20�����������㼶���У�ÿ�������� ��ľˮ��������
    //ÿ������ռ��5��id������ռ25��id, �����׳�����ţ�ÿ�����Ͱ���ľˮ����
    // ���׳����=��ľˮ����,Ҳ����01234
    //typeҲ�ǰ��㼶˳���ŵģ�ֱ��ת��ΪInt����

    //����ʱ���ã�����bodytype�ǽ���ʱ������
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
