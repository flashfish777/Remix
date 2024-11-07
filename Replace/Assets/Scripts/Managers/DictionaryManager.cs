using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using Newtonsoft.Json;
// using static System.Net.Mime.MediaTypeNames;
/// <summary>
/// 就我想的是，那个服装（还是啥来着，反正一个Sprite），先给所有的都给个id和缩略图储存成ItemSO的数据结构，
/// 游戏完成后得到的id存文件的“衣柜”按钮的时候更新一次
/// 还有还有，被替换的那个原始预制体挂了个button，这里点击后来展示成品图的UI
/// </summary>

//要存进去的
[System.Serializable]
public class ItemData
{
    public int id;
}

public class DictionaryManager
{
    public static DictionaryManager Instance;

    public GameObject smallImagePrefab; // 缩略图UI元素预制体

    public GameObject bigImageUIPrefab;//成品图UI元素预制体

    public Image smallImage;//缩略图

    public Image bigImage;//成品图

    public Transform contentPanel; // 下滑视图的内容容器

    public Transform uI;//跟DictionaryUI同一级

    public List<ItemSO> dictioinarySO;//列表存储所有ItemSO的集合

    private Dictionary<int, ItemSO> assetDictionary = new Dictionary<int, ItemSO>();//字典把id和image对应起来

    public TextAsset jsonFile;//json文件（需要接一下）

    private void Awake()
    {
        Instance = this;

        //准备把成品图UI挂在和DictionaryUI同一级的位置
        uI = GameObject.Find("UI").transform;

        // 初始化字典
        foreach (var asset in dictioinarySO)
        {
            assetDictionary.Add(asset.id, asset);
        }
    }

    private void Start()
    {
        FindAllAssets();
    }

    //找所有ItemSO资产并存入字典
    [ContextMenu("Find Assets")]
    void FindAllAssets()
    {
        string[] guids = AssetDatabase.FindAssets("t:ItemSO");
        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            ItemSO asset = AssetDatabase.LoadAssetAtPath<ItemSO>(assetPath);

            if (asset != null)
            {
                dictioinarySO.Add(asset);
            }
        }
    }
    //读json数据
    public List<ItemData> ReadItemSODate()
    {
        string json = jsonFile.text;

        List<ItemData> itemDataList = JsonConvert.DeserializeObject<List<ItemData>>(json);

        return itemDataList;
    }


    //根据id找对应ItemSO
    public ItemSO GetAssetById(int id)
    {
        if (assetDictionary.TryGetValue(id, out ItemSO asset))
        {
            return asset;
        }
        return null;
    }

    public void SaveDictionary()
    {
        //保存回去
    }


    //更新衣柜内部
    public void UpdateDictionary()
    {
        List<ItemData> itemdata = ReadItemSODate();

        foreach (var itemSO in itemdata)
        {
            if (itemSO != null)
            {
                //根据id找对应ItemSO
                ItemSO currentItemSO = GetAssetById(itemSO.id);

                // 实例化缩略、成品预制
                GameObject newsmallImage = GameObject.Instantiate(smallImagePrefab) as GameObject;

                GameObject newbigImageUI = GameObject.Instantiate(bigImageUIPrefab) as GameObject;

                //挂位置
                newsmallImage.transform.SetParent(contentPanel);

                newbigImageUI.transform.SetParent(uI);

                //找image组件
                Image smallimageComponent = newsmallImage.GetComponent<Image>();
                //image在bigimage的子物体上
                Image bigimageComponent = newbigImageUI.GetComponentInChildren<Image>();

                //更新sprite
                if (smallimageComponent != null && bigimageComponent != null)
                {
                    smallimageComponent.sprite = currentItemSO.smallImage;

                    bigimageComponent.sprite = currentItemSO.bigImage;
                }
            }
        }
    }
}
