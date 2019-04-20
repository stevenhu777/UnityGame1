using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public ItemData bulletData;
    private static BuildManager instance;
    public static BuildManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;

        }
    }
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    //    Instantiate(bulletData.itemPrefab, new Vector3(-1,-1,0), Quaternion.identity);
    //    Instantiate(bulletData.itemPrefab, new Vector3(-1,1,0), Quaternion.identity);
    //    Instantiate(bulletData.itemPrefab, new Vector3(1,-1,0), Quaternion.identity);
    //    Instantiate(bulletData.itemPrefab, new Vector3(1,1,0), Quaternion.identity);
    //    Instantiate(bulletData.itemPrefab, new Vector3(1,2,0), Quaternion.identity);
    //    Instantiate(bulletData.itemPrefab, new Vector3(2,1,0), Quaternion.identity);
    //    Instantiate(bulletData.itemPrefab, new Vector3(2,2,0), Quaternion.identity);
    //    Instantiate(bulletData.itemPrefab, new Vector3(2,0,0), Quaternion.identity);
    //    Instantiate(bulletData.itemPrefab, new Vector3(0,2,0), Quaternion.identity);
    //    Instantiate(bulletData.itemPrefab, new Vector3(0,0,0), Quaternion.identity);
       // Instantiate(bulletData.itemPrefab, new Vector3(0,0,0), Quaternion.identity);
    }

    public  void DataReturn(Collider2D col,GameObject obj)
    {
        if (col.gameObject.tag=="Item1")
        {
            obj.SendMessage("AttackItemData", bulletData);
        }
    }

}
