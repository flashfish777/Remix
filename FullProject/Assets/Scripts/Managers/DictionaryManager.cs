﻿using System;
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
    public List<int> ids;//TODO list不能直接序列化，需要处理一下
}

public class DictionaryManager : MonoBehaviour
{
    public static DictionaryManager Instance;

    public List<List<int>>  dictionary = new List<List<int>>();//衣柜列表，每个的List里有19个int，代表19个部件的id
    public TextAsset jsonFile;//json文件（需要接一下）


    ////服装顺序规则
    //20个部件，按层级排列，每个部件按金木水火土排序
    //层级：
    //背景        0-4,
    //中景摆件    5-9,
    //后头发      10-14，
    //身体        15-39,
    //纹身        40-44,
    //袜子        45-49,
    //鞋子        50-54,
    //腿饰        55-59,
    //下衣        60-64,
    //手部        65-69,
    //上衣        70-74,
    //腰饰        75-79,
    //项链        80-84,
    //妆容        85-89,
    //耳饰        90-94,
    //前头发      95-99,
    //头饰       100-104,
    //宠物       105-109,
    //特效       110-114
    //一般部件按金木水火土占用5个id，身体占25个id, 按丰均壮瘦胖排，每个体型再按金木水火土排
    [Tooltip("服装字典，顺序看代码注释")]
    [SerializeField]
    private Sprite[] Cloth_CatelogList = new Sprite[115];

    public Dictionary<int, Sprite> ClothesCatelog = new Dictionary<int, Sprite>();


    public List<List<int>> GetAllCollection()
    {
        return dictionary;
    }

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < Cloth_CatelogList.Length; i++) {
            ClothesCatelog.Add(i, Cloth_CatelogList[i]);
        }


        //此处要读取存档更新dictionary
        ReadItemSODate();
    }

    private void Start()
    {
    }


    //读json数据
    public List<ItemData> ReadItemSODate()
    {
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            List<ItemData> itemDataList = JsonConvert.DeserializeObject<List<ItemData>>(json);

            return itemDataList;
        }
        else {
            return new List<ItemData>();
        }
        
    }

    public void SaveDictionary()
    {
        //TODO 保存成Json
    }


    public void AddCollection(List<int> collection) {
        dictionary.Insert(0, collection);//最新的显示在第一个
    }
}
