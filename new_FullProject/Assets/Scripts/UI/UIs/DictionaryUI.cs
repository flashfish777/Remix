using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DictionaryUI : UIBase
{

    public GameObject DictionaryContent;
    public GameObject smallImagePrefab;

    private void Awake()
    {
        // 返回
        Register("BackButton").onClick = onBackBtn;

        DictionaryContent = GameObject.Find("DictionaryContent");
        smallImagePrefab = Resources.Load("UI/SmallImage") as GameObject;
    }

    private void Start()
    {
        
    }

    private void onBackBtn(GameObject @object, PointerEventData data)
    {
        Close();
    }

    private void OnEnable()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        List<List<int>> itemData = DictionaryManager.Instance.GetAllCollection();
        ClearAllCollections(); 

        foreach (List<int> collection in itemData)
        {
            GameObject instantiatedObject = Instantiate(smallImagePrefab);
            instantiatedObject.transform.SetParent(DictionaryContent.transform);
            
            if (smallImagePrefab != null)
            {
                for (int i = 0;i<collection.Count;i++)
                {
                    instantiatedObject.transform.GetChild(i).GetComponent<Image>().sprite =
                        DictionaryManager.Instance.ClothesCatelog[collection[i]];
                }
            }
        }
    }

    private void ClearAllCollections()
    {
        Transform[] allChildren = DictionaryContent.GetComponentsInChildren<Transform>();

        // 遍历所有子组件，除了根组件本身
        foreach (Transform child in allChildren)
        {
            // 跳过根组件本身
            if (child == transform)
                continue;

            // 销毁子组件
            Destroy(child.gameObject);
        }

    }
}
