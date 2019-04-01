using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemData
{
    public GameObject itemPrefab;
    public float width;
    public float height;
    public float speed;
    public ItemType type;
}
public enum ItemType
{
    BulletType,
}
