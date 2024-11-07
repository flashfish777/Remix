using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "DataSO/ItemSO")]
public class ItemSO : ScriptableObject
{
    public int id;
    public Sprite smallImage;
    public Sprite bigImage;
}
