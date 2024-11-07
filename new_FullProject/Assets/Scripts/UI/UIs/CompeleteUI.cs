using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 展示UI
/// </summary>
public class CompeleteUI : UIBase
{
    public GameObject ImageFather;

    private void Awake()
    {
        // 返回主菜单
        Register("quitBtn").onClick = onQuitBtn;
        // 继续游戏
        Register("continueBtn").onClick = onContinueBtn;
    }

    private void onQuitBtn(GameObject @object, PointerEventData data)
    {
        Close();

        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        AudioManager.Instance.PlayBGM("LoginBgm");
    }

    private void onContinueBtn(GameObject @object, PointerEventData data)
    {
        Close();
        GamingManager.Instance.Init();
        GamingManager.Instance.ChangeType(GamingType.Init);
    }

    private void OnEnable()
    {
        ImageFather = GameObject.Find("ImageFather");
        List<int> clothesList = GridManager.Instance.GetCurrentClothesList();
       
        for (int i = 0; i < clothesList.Count; i++)
        {
            ImageFather.transform.GetChild(i).GetComponent<Image>().sprite =
            DictionaryManager.Instance.ClothesCatelog[clothesList[i]];
        }

        // 测试,每当按下完成就存一次
        DictionaryManager.Instance.AddCollection(clothesList);
        DictionaryManager.Instance.SaveDictionary();
    }
}
