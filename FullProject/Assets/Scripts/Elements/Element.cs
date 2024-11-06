using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementType { 
    Gold,
    Wood,
    Water,
    Fire,
    Earth
}
public class Element : MonoBehaviour
{
    public ElementType elementType;
    public Sprite smallSprite;
    public Sprite middleSprite;
    public Sprite largeSprite;

    public int amount; // 1,2,3 �ֱ���� С���У���ͼ�꣬0��ʱ����ʧ��3+1��ʱ��ը��ֵ��Ϊ0��


    //��ը����1�����򷵻�0.
    public int AddElement(ElementType incomingType) {
        if (amount == 0)
        {
            return -1;//��Ӧ�ó����������
        }

        switch (elementType)
        {
            case ElementType.Wood:
                if (incomingType == ElementType.Water || incomingType == ElementType.Wood)
                    amount++;
                else if (incomingType == ElementType.Gold)
                    amount--;
                break;

            case ElementType.Fire:
                if (incomingType == ElementType.Wood || incomingType == ElementType.Fire)
                    amount++;
                else if (incomingType == ElementType.Water)
                    amount--;
                break;

            case ElementType.Earth:
                if (incomingType == ElementType.Fire || ElementType.Earth == incomingType)
                    amount++;
                else if (incomingType == ElementType.Wood)
                    amount--;
                break;

            case ElementType.Gold:
                if (incomingType == ElementType.Earth || incomingType == ElementType.Gold)
                    amount++;
                else if (incomingType == ElementType.Fire)
                    amount--;
                break;

            case ElementType.Water:
                if (incomingType == ElementType.Gold || incomingType == ElementType.Water)
                    amount++;
                else if (incomingType == ElementType.Earth)
                    amount--;
                break;
        }
       
        if (amount == 4)//���ˣ����Ա�ը
        {
            amount = 0;
            return 1;
        }
        else {
            return 0;
        }

    }

    public Sprite GetSprite() {
        if (amount == 1) {
            return smallSprite;
        }
        else if (amount == 2)
        {
            return middleSprite;
        }
        else if (amount == 3)
        {
            return largeSprite;
        }
        return Resources.Load<Sprite>("Icon/test_transparent");
    }
}
