using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 格子管理器
/// </summary>
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;


    private static Element[] elements = new Element[25];  //逻辑层，25个格子
    private static Clothes[] clothes = new Clothes[20]; //逻辑层，按左、上、右、下顺序排列20个衣服
    //详细顺序
    List<string> prefabNames = new List<string> {
        "0Cloth_Upper", "1Cloth_Neck", "2Cloth_Head", "3Cloth_Ear", "4Cloth_Hand",
        "5Cloth_Pants", "6Cloth_Shoes1", "7Cloth_Belt", "8Cloth_Ankle", "9Cloth_Socks",
        "10Cloth_Body", "11Cloth_Color", "12Cloth_Hair_Fore", "13Cloth_Hair_Back", "14Cloth_Makeup",
        "15Cloth_Back", "16Cloth_Tattoo", "17Cloth_Mid", "18Cloth_Pet", "19Cloth_Effect" };
 

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        //TODO:读取配置表，生成初始关卡。现在以下部分是随机数据代替。
        for (int i = 0; i < 25; i++)
        {
            int random = UnityEngine.Random.Range(0, 5);
            ElementType randomElement = (ElementType)random;
            string elementName = randomElement.ToString();
            elements[i] = Instantiate(Resources.Load<GameObject>("Elements/" + elementName)).GetComponent<Element>();
            elements[i].amount = UnityEngine.Random.Range(1, 4);
        }

        for (int i = 0; i < 20; i++)
        {
            clothes[i] = Instantiate(Resources.Load<GameObject>("Clothes/" + prefabNames[i])).GetComponent<Clothes>();
        }
    }

    public void Rotate(int[] rotateOder)
    {
        Element[] tempLogicArray = new Element[rotateOder.Length];
        for (int i = 0; i < rotateOder.Length; i++)
        {
            tempLogicArray[i] = elements[rotateOder[i]];
        }
        for (int i = 0; i < rotateOder.Length; i++)
        {
            int nextIndex = (i + 1) % rotateOder.Length;
            elements[rotateOder[nextIndex]] = tempLogicArray[i];
        }
    }

    public bool IsElementCleared()
    {
        foreach (Element element in elements) {
            if (element.amount != 0) {
                return false;
            }
        }
        return true;
    }

    public bool IsClothesComplete()
    {
        foreach (Clothes cloth in clothes)
        {
            if (cloth.amount == 0)
            {
                return false;
            }
        }
        return true;
    }

    public Element getElement(int index) {
        return elements[index];
    }

    //寻找爆炸后会射到的元素Index，返回所在格子的index数组
    public List<int> FindNeighbourElement(int index)
    {
        List<int> elementIndexs = new List<int>();

        int row = index / 5;
        int column = index % 5;

        //上
        for (int i = row - 1; i >= 0; i--)
        {

            if (elements[i * 5 + column].amount != 0)
            {
                elementIndexs.Add(i * 5 + column);
                break;
            }
        }
        //下
        for (int i = row + 1; i < 5; i++)
        {
            if (elements[i * 5 + column].amount != 0)
            {
                elementIndexs.Add(i * 5 + column);
                break;
            }
        }
        //左
        for (int i = column - 1; i >= 0; i--)
        {
            if (elements[row * 5 + i].amount != 0)
            {
                elementIndexs.Add(row * 5 + i);
                break;
            }
        }
        //右
        for (int i = column + 1; i < 5; i++)
        {
            if (elements[row * 5 + i].amount != 0)
            {
                ;
                elementIndexs.Add(row * 5 + i);
                break;
            }
        }
        return elementIndexs;
    }

    //寻找爆炸后会射到的最终衣服格子Index，返回所在格子的index数组
    public List<int> FindClothCellIndex(int index)
    {
        List<int> clothIndexs = new List<int>();//按左上右下，没有命中为-1，有命中的从左至右（从上到下）分别为0-4

        int row = index / 5;
        int column = index % 5;
        bool isfind = false;

        //左
        for (int i = column - 1; i >= 0; i--)
        {
            if (elements[row * 5 + i].amount != 0)
            {
                isfind = true;
                break;
            }
        }
        clothIndexs.Add(isfind ? -1 : row);
        isfind = false;

        //上
        for (int i = row - 1; i >= 0; i--)
        {
            if (elements[i * 5 + column].amount != 0)
            {
                isfind = true;
                break;
            }
        }
        clothIndexs.Add(isfind ? -1 : column);
        isfind = false;

        //右
        for (int i = column + 1; i < 5; i++)
        {
            if (elements[row * 5 + i].amount != 0)
            {
                isfind = true;
                break;
            }
        }
        clothIndexs.Add(isfind ? -1 : row);
        isfind = false;

        //下
        for (int i = row + 1; i < 5; i++)
        {
            if (elements[i * 5 + column].amount != 0)
            {
                isfind = true;
                break;
            }
        }
        clothIndexs.Add(isfind ? -1 : column);

        return clothIndexs;
    }

    public void UpdateClothes(List<int> clothCellIndexs, ElementType bombingType) {
        if (clothCellIndexs[0] != -1)
        {
            clothes[clothCellIndexs[0]].addElement(bombingType);
        }
        if (clothCellIndexs[1] != -1)
        {
            clothes[5 + clothCellIndexs[1]].addElement(bombingType);
        }
        if (clothCellIndexs[2] != -1)
        {
            clothes[10 + clothCellIndexs[2]].addElement(bombingType);
        }
        if (clothCellIndexs[3] != -1)
        {
            clothes[15 + clothCellIndexs[3]].addElement(bombingType);
        }
    }

    public Sprite GetClothesSprite(int index) {
        return clothes[index].GetSprite();
    }

    public Sprite GetElementSprite(int index)
    {
        return elements[index].GetSprite();
    }

    public List<int> GetCurrentClothesList() {
        List<int> clothesList = new List<int>();

        /* 衣服格子里的顺序如下
         * "0Cloth_Upper", "1Cloth_Neck", "2Cloth_Head", "3Cloth_Ear", "4Cloth_Hand",
        "5Cloth_Pants", "6Cloth_Shoes1", "7Cloth_Belt", "8Cloth_Ankle", "9Cloth_Socks",
        "10Cloth_Body", "11Cloth_Color", "12Cloth_Hair_Fore", "13Cloth_Hair_Back", "14Cloth_Makeup",
        "15Cloth_Back", "16Cloth_Tattoo", "17Cloth_Mid", "18Cloth_Pet", "19Cloth_Effect"
        */
        /*展示柜摆放顺序如下：
             Cloth_Back, Cloth_Mid, Cloth_Hair_Back, 
             Cloth_Body, Cloth_Tattoo, Cloth_Socks, 
             Cloth_Shoes1, Cloth_Ankle, Cloth_Pants, 
             Cloth_Hand, Cloth_Upper, Cloth_Belt, 
             Cloth_Neck, Cloth_Makeup, Cloth_Ear, 
             Cloth_Hair_Fore, Cloth_Head, Cloth_Pet, 
             Cloth_Effect
         *
         *按展示顺序依次获取当前衣服的id,展示时再从字典表里找对应的sprite
         */
        int[] clothesOrder = {
            15,17,13,10,16,9,6,8,5,4,0,7,1,14,3,12,2,18,19
        };
        for (int i = 0; i < clothesOrder.Length; i++) {
            clothesList.Add(clothes[clothesOrder[i]].GetCurrentClothesID(clothes[10].ElementType));
        }
        return clothesList;
    }


    public void ClearAll()
    {
        foreach (Element item in elements)
        {
            if (item != null)
            Destroy(item.gameObject);
        }
        
        foreach (Clothes item in clothes)
        {
            if (item != null)
            Destroy(item.gameObject);
        }
    }
}
