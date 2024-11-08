using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// 格子UI
/// </summary>
public class GridUI : UIBase
{
    public float rotationDuration = 0.5f;//旋转速度
    public float splashDuration = 0.5f; // 炸裂后移动持续时间
    public Ease splashEase = Ease.Linear; // 炸裂后移动缓动类型

    private static GameObject[] elementsCells = new GameObject[25];  //表现层，25个button

    private VerticalLayoutGroup clothesIconLeft; //表现层，对应0-4
    private VerticalLayoutGroup clothesIconTop; //表现层，对应5-9
    private VerticalLayoutGroup clothesIconRight; //表现层，对应10-14   
    private VerticalLayoutGroup clothesIconButtom; //表现层，对应15-19

    //private int timeCounter = 0;
    private int runningCoroutines = 0;//爆炸动画协程计数器，当有协程运行时禁止旋转及点击
    private int rotateCounter = 0; //旋转动画计数器：外圈16个，内圈8个，每个格子的dotween动画结束时减1，为0时可以操作

    private void Awake()
    {
        // 顺时针旋转
        transform.Find("clockwise").GetComponent<Button>().onClick.AddListener(() => onClockwiseBtn());
        // 逆时针旋转
        transform.Find("unClockwise").GetComponent<Button>().onClick.AddListener(() => onUnClockwiseBtn());

        for (int i = 0; i < 25; i++)
        {
            elementsCells[i] = GameObject.Find("elementsGrid").transform.GetChild(i).gameObject;
            int index = i;
            //为每个Button添加点击方法
            elementsCells[i].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnElementClick(index));
        }

        clothesIconLeft = GameObject.Find("left").GetComponent<VerticalLayoutGroup>();
        clothesIconRight = GameObject.Find("right").GetComponent<VerticalLayoutGroup>();
        clothesIconTop = GameObject.Find("top").GetComponent<VerticalLayoutGroup>();
        clothesIconButtom = GameObject.Find("buttom").GetComponent<VerticalLayoutGroup>();

        UpdateUI();
    }

    private void Update()
    {
        //虽然判断胜负应该在逻辑层写，但是没想好怎么在动画结束后才执行结算画面，所以放到这里了
        if (runningCoroutines == 0)
        {
            bool isCleared = GridManager.Instance.IsElementCleared();
            bool isClothesComplete = GridManager.Instance.IsClothesComplete();
            bool isQiUsed = GamingManager.Instance.CurQi == 0;

            if (isCleared && isClothesComplete)
            {
                UIManager.Instance.CloseAllUI();
                GamingManager.Instance.ChangeType(GamingType.Complete);
                //DictionaryManager.Instance.AddCollection(GridManager.Instance.GetCurrentClothesList());
                //DictionaryManager.Instance.SaveDictionary();
            }
            else if ((isQiUsed && (!isClothesComplete || !isCleared))
                || (isCleared && !isClothesComplete))
            {
                UIManager.Instance.CloseAllUI();
                GamingManager.Instance.ChangeType(GamingType.Lose);
            }
        }

    }

    private void onClockwiseBtn()
    {
        AudioManager.Instance.PlayIntf("clockwiseIntf");

        //旋转方向列表，前一个会到后一个的位置，如此循环
        if (runningCoroutines == 0 && rotateCounter == 0)
        {
            rotateCounter = 16;
            Rotate(new int[]{
            0, 1, 2, 3, 4,
            9, 14, 19,
            24, 23, 22, 21, 20,
            15,10, 5
            });
        }
    }

    private void onUnClockwiseBtn()
    {
        AudioManager.Instance.PlayIntf("unClockwiseIntf");

        //旋转方向列表，前一个会到后一个的位置，如此循环
        if (runningCoroutines == 0 && rotateCounter == 0)
        {
            rotateCounter = 8;
            Rotate(new int[]{
            11,16,17,18,13,8,7,6
            });
        }
    }

    private void Rotate(int[] rotateOder)
    {
        // 存储旋转前的Button和逻辑数组
        GameObject[] tempButtons = new GameObject[rotateOder.Length];
        for (int i = 0; i < rotateOder.Length; i++)
        {
            tempButtons[i] = elementsCells[rotateOder[i]];
        }

        //更新逻辑层顺序
        GridManager.Instance.Rotate(rotateOder);

        // 更新Button数组的顺序
        for (int i = 0; i < rotateOder.Length; i++)
        {
            int nextIndex = (i + 1) % rotateOder.Length;
            elementsCells[rotateOder[nextIndex]] = tempButtons[i];
            elementsCells[rotateOder[nextIndex]].transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            int index = rotateOder[nextIndex];
            elementsCells[rotateOder[nextIndex]].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnElementClick(index));

        }
        UpdateUI();

        // 执行旋转动画（平移）
        for (int i = 0; i < rotateOder.Length; i++)
        {
            int nextIndex = (i + 1) % rotateOder.Length;
            elementsCells[rotateOder[i]].transform.DOMove(elementsCells[rotateOder[nextIndex]].transform.position, rotationDuration).OnComplete(() =>
            {
                rotateCounter--;
            });
        }
    }

    public void OnElementClick(int index)
    {
        if (runningCoroutines != 0 || rotateCounter != 0)
        {
            return;
        }

        if (GamingManager.Instance.CurQi == 0)
        {
            return;
        }
        if (GridManager.Instance.getElement(index).amount == 0) {
            return;
        }
        
        GamingManager.Instance.CurQi--;
        UIManager.Instance.GetUI<GamingUI>("GamingUI").UpdateWater();

        //使用协程，每次播放完动画再进入下一次迭代
        runningCoroutines++;
        StartCoroutine(AddElementCoroutine(index, GridManager.Instance.getElement(index).elementType));
        //结算要等动画执行完，所以干脆胜负都放在Update里计算了
    }

    private void PlayExplosionFrameAnimation(GameObject cell, ElementType elementType)
    {
        Image image = cell.transform.GetChild(0).GetComponent<Image>();
        Animator animator = image.GetComponent<Animator>();

        if (animator == null)
        {
            animator = image.gameObject.AddComponent<Animator>();
        }
        // 根据elementType设置Animator Controller
        animator.runtimeAnimatorController = GetAnimatorController(elementType);
    }

    private RuntimeAnimatorController GetAnimatorController(ElementType elementType)
    {
        // 根据elementType返回对应的Animator Controller
        switch (elementType)
        {
            case ElementType.Fire:
                return Resources.Load<RuntimeAnimatorController>("Elements/BombAnimation/Fire");
            case ElementType.Water:
                return Resources.Load<RuntimeAnimatorController>("Elements/BombAnimation/Water");
            case ElementType.Wood:
                return Resources.Load<RuntimeAnimatorController>("Elements/BombAnimation/Wood");
            case ElementType.Gold:
                return Resources.Load<RuntimeAnimatorController>("Elements/BombAnimation/Gold");
            case ElementType.Earth:
                return Resources.Load<RuntimeAnimatorController>("Elements/BombAnimation/Earth");
            default:
                return null;
        }
    }

    private IEnumerator PlayExplosionAnimationCoroutine(GameObject cell, List<int> neighbourElements, List<int> clothCellIndexs, ElementType bombingType)
    {
        //等待爆炸动画时间...应该播放动画完成后触发事件实现，但不想改了，先这样写吧
        yield return new WaitForSeconds(0.15f);

        foreach (int neighbourIndex in neighbourElements)
        {
            Vector3 neighbourPosition = elementsCells[neighbourIndex].transform.position;
            splashElement(cell, bombingType, neighbourPosition);
        }
        if (clothCellIndexs[0] != -1)
        {
            Vector3 targetPosition = clothesIconLeft.transform.GetChild(clothCellIndexs[0]).position;
            splashElement(cell, bombingType, targetPosition);
        }
        if (clothCellIndexs[1] != -1)
        {
            Vector3 targetPosition = clothesIconTop.transform.GetChild(clothCellIndexs[1]).position;
            splashElement(cell, bombingType, targetPosition);
        }
        if (clothCellIndexs[2] != -1)
        {
            Vector3 targetPosition = clothesIconRight.transform.GetChild(clothCellIndexs[2]).position;
            splashElement(cell, bombingType, targetPosition);
        }
        if (clothCellIndexs[3] != -1)
        {
            Vector3 targetPosition = clothesIconButtom.transform.GetChild(clothCellIndexs[3]).position;
            splashElement(cell, bombingType, targetPosition);
        }
        yield return new WaitForSeconds(splashDuration);//等待一次动画时间
    }

    private void splashElement(GameObject cell, ElementType bombingType, Vector3 targetPosition)
    {
        Vector3 cellPosition = cell.transform.position;
        GameObject waterDrop = Instantiate(Resources.Load<GameObject>(
                    "Elements/BombEffect/" + bombingType.ToString() + "Effect"), cellPosition, Quaternion.identity);
        waterDrop.transform.SetParent(cell.transform);
        Vector3 direction = (targetPosition - cellPosition).normalized;
        waterDrop.transform.DOMove(targetPosition, splashDuration).OnComplete(() =>
        {
            Destroy(waterDrop);
        });
    }


    private IEnumerator AddElementCoroutine(int index, ElementType type)
    {
        AudioManager.Instance.PlayEffect(type.ToString() + GridManager.Instance.getElement(index).amount + "Intf");

        int isBomb = GridManager.Instance.getElement(index).AddElement(type);
        if (isBomb == 1)
        {
            ElementType bombingType = GridManager.Instance.getElement(index).elementType;

            List<int> neighbourElements = GridManager.Instance.FindNeighbourElement(index);
            List<int> clothCellIndexs = GridManager.Instance.FindClothCellIndex(index);

            PlayExplosionFrameAnimation(elementsCells[index], bombingType);
            yield return StartCoroutine(PlayExplosionAnimationCoroutine(elementsCells[index], neighbourElements, clothCellIndexs, bombingType));

            GridManager.Instance.UpdateClothes(clothCellIndexs, bombingType);
            UpdateUI();

            foreach (int neighbourElementIndex in neighbourElements)
            {
                runningCoroutines++;
                StartCoroutine(AddElementCoroutine(neighbourElementIndex, bombingType));
            }
        }
        else
        {
            UpdateUI();
        }
        runningCoroutines--;
    }


    public void UpdateUI()
    {
        for (int i = 0; i < 25; i++)
        {
            Transform button = elementsCells[i].transform.GetChild(0);
            button.GetComponent<Image>().sprite = GridManager.Instance.GetElementSprite(i);
        }
        for (int i = 0; i < 5; i++)
        {
            Transform imageLeft = clothesIconLeft.transform.GetChild(i);
            imageLeft.GetComponent<Image>().sprite = GridManager.Instance.GetClothesSprite(i);

            Transform imageTop = clothesIconTop.transform.GetChild(i);
            imageTop.GetComponent<Image>().sprite = GridManager.Instance.GetClothesSprite(i + 5);

            Transform imageRight = clothesIconRight.transform.GetChild(i);
            imageRight.GetComponent<Image>().sprite = GridManager.Instance.GetClothesSprite(i + 10);

            Transform imageButtom = clothesIconButtom.transform.GetChild(i);
            imageButtom.GetComponent<Image>().sprite = GridManager.Instance.GetClothesSprite(i + 15);
        }
    }

}
