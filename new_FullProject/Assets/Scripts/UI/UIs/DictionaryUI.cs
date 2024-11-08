using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DictionaryUI : UIBase
{

    public GameObject DictionaryContent;
    public GameObject ImageBig;

    private void Awake()
    {
        // 返回
        Register("Back/BackButton").onClick = onBackBtn;

        DictionaryContent = GameObject.Find("DictionaryContent");
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
        ImageBig = GameObject.Find("FullScreenShow");
        ImageBig.SetActive(false);
    }

    public void UpdateDictionary()
    {
        List<List<int>> itemData = DictionaryManager.Instance.GetAllCollection();
        ClearAllCollections();
        foreach (List<int> collection in itemData)
        {
            Debug.Log(collection);
            GameObject instantiatedObject = Instantiate(Resources.Load<GameObject>("UI/SmallImage"));
            instantiatedObject.transform.SetParent(DictionaryContent.transform);
            instantiatedObject.GetComponent<Button>().onClick.AddListener(() => ShowBigImage(collection));
            for (int i = 0; i < collection.Count; i++)
            {
                instantiatedObject.transform.GetChild(i).GetComponent<Image>().sprite =
                DictionaryManager.Instance.ClothesCatelog[collection[i]];
            }
        }
    }

    private void ShowBigImage(List<int> collection)
    {
        ImageBig.SetActive(true);
        for (int i = 0; i < collection.Count; i++)
        {
            ImageBig.transform.GetChild(i).GetComponent<Image>().sprite =
            DictionaryManager.Instance.ClothesCatelog[collection[i]];
        }
        ImageBig.GetComponent<Button>().onClick.AddListener(() => {
            ImageBig.SetActive(false);
        });
    }

    private void ClearAllCollections()
    {
        DictionaryContent = GameObject.Find("DictionaryContent");
        Transform[] allChildren = DictionaryContent.GetComponentsInChildren<Transform>();

        // 遍历所有子组件，除了根组件本身
        foreach (Transform child in allChildren)
        {
            // 跳过根组件本身
            if (child == DictionaryContent.transform)
            {
                continue;
            }
            // 销毁子组件
            Destroy(child.gameObject);
        }

    }
}


