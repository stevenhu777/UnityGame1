using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    
    public float speed;    //プレイヤーの移動速度

    public int playerNumber;

    public ItemMove itemMoveObj;

    public Stone stoneObjCode;
    private ItemData selectedItem;

    public Transform attackStartPosition;

    private bool isSetItemPosition=false;
    private bool isSetStonePosition = false;

    //矢印の方向
    private float arrowAngle;

    public Transform arrowPositionTra;

    public Transform  arrowRotationTra;  

    public GameObject arrowPrefabs;
    private GameObject arrowGob;

    //public GameObject stonePrefabs;
    //private GameObject stoneGob;

    private Collider2D currentCol;

    private  Vector3 rotation ;

    float h, v;
    float horizontal;

    float vertical;

    private string  direction;

    public float hp = 100;

    public int coinCnt=0;
    public static bool isBoss = false;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        rigid = GetComponent<Rigidbody2D>();
        arrowGob=Instantiate(arrowPrefabs, arrowPositionTra.position, Quaternion.identity);
        coinCnt = 0;
        isBoss = false;
    }
    private void Update()
    {
        if (coinCnt == 5 && transform.localScale.x <= 3)
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);

        }

        if (isSetItemPosition&&selectedItem!=null&&itemMoveObj!=null)
        {
            itemMoveObj.SetPosition(attackStartPosition.position);
        }
        if (isSetStonePosition&&stoneObjCode!=null)
        {
            stoneObjCode.SetPosition(attackStartPosition.position);
        }
        

        if (currentCol!=null)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                if (currentCol.gameObject.tag=="Stone")
                {
                    isSetStonePosition = false;
                    AttackStone();
                }
                else
                {
                    isSetItemPosition = false;
                    AttackItem();
                    //Debug.Log(h);
                }

            }
        }

        
        //arrowGob.GetComponent<Arrow>().SendMessage("SetPosition", arrowPositionTra.position);
        //arrowGob.SendMessage("SetPosition", arrowPositionTra.position);
        //Debug.Log(arrowGob.transform.position);


        //if (Input.GetKeyDown(KeyCode.JoystickButton1))
        //{
        //     AttackRotation();
        // }
    }

    private void FixedUpdate()
    {
        if (playerNumber == 0)
        {
            return;
        }
        Move();
       
        SetArrowPosition();
        SetArrowRotation();
    }
    private void Move()
    {
        h = Input.GetAxisRaw("Jokstick"+playerNumber+"X");
        v = (Input.GetAxisRaw("Jokstick"+playerNumber+ "Y"));

        if (h>0)
        {
            direction = "right";
        }
        else if (h<0)
        {
            direction = "left";
        }
        if ((Mathf.Abs(h)<0.4&& Mathf.Abs(h)>0)|| (Mathf.Abs(v)<0.4&&Mathf.Abs(v)>0))
        {
            return;
        }
        rigid.velocity = new Vector2(speed * h, speed * v);
        //入力された値とプレイヤーの現在の向きによって
        //画像を反転にして　
        //if (h < 0 && transform.localScale.x > 0 ||
        //    h > 0 && transform.localScale.x < 0)
        //{
        //    Vector2 pos = transform.localScale;
        //    pos.x *= -1;
        //    transform.localScale = pos;

        //}

        //Debug.Log(arrowAngle);

        //Debug.Log(h+"+"+v);

        // arrowGob.SendMessage("SetArrowPosition", arrowPositionTra.position);
    }
    private void SetArrowPosition()
    {
        if (arrowGob!=null&&arrowPositionTra!=null)
        {
            arrowGob.transform.position = arrowPositionTra.position;
        }
     
    }
    private void SetArrowRotation()
    {
        if (arrowRotationTra==null&&arrowGob==null)
        {
            return;
        }
        if (h==0&&v==0)
        {
            return;
        }
        if (h==0&&v>0.1f)
        {
            arrowAngle = 90;
        }
        else if (h==0&&v<-0.1f)
        {
            arrowAngle = 270;
        }
        else if (v==0&&h>0.1f)
        {
            arrowAngle = 0;
        }
        else if (v==0&&h<0.1f)
        {
            arrowAngle = 180;
        }
        else
        {
            arrowAngle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;
        }
        rotation = new Vector3(0, 0, arrowAngle);
        //Debug.Log(h + "," + v);
        arrowRotationTra.rotation = Quaternion.Euler(rotation);
        arrowGob.transform.rotation = Quaternion.Euler(rotation);
 

    }

    private void AttackStone()
    {
        stoneObjCode.Attack(arrowAngle);
        currentCol = null;
    }
    private void AttackItemData(ItemData itemData)
    {
        if (itemData!=null)
        {
            isSetItemPosition = true;
        }

        selectedItem = itemData;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Instantiate(bulGam, transform.position, Quaternion.identity);
        //    Vector3 endPoint = transform.position + new Vector3(2, 0, 0);
        //    ItemMove.Instance.SetPoint(transform.position, endPoint, 0.5f, 2);
        //}
       
    }
    private void AttackItem()
    {
        if (itemMoveObj!=null)
        {
            if (direction=="right")
            {
                Vector3 endPoint = transform.position + new Vector3(selectedItem.width, 0, 0);
                itemMoveObj.SetAttackData(attackStartPosition.position, endPoint, selectedItem.speed, selectedItem.height);
            }
            else if(direction=="left")
            {
                Vector3 endPoint = transform.position -new Vector3(selectedItem.width, 0, 0);
                itemMoveObj.SetAttackData(attackStartPosition.position, endPoint, selectedItem.speed, selectedItem.height);
            }

            itemMoveObj = null;
            selectedItem = null;
            
        }
        currentCol = null;
    }
  
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (currentCol==null)
        {
            switch (col.gameObject.tag)
            {
                case "Item1":
                    if (Input.GetKeyDown(KeyCode.JoystickButton3))
                    {
                        BuildManager.Instance.DataReturn(col, this.gameObject);
                        itemMoveObj = col.gameObject.GetComponent<ItemMove>();
                        currentCol = col;
                    }
                    break;
                case "Stone":
                    if (Input.GetKeyDown(KeyCode.JoystickButton3))
                    {
                        //BuildManager.Instance.DataReturn(col, this.gameObject);
                        stoneObjCode = col.gameObject.GetComponent<Stone>();
                        stoneObjCode.SetPlayerObject(this.gameObject);
                        isSetStonePosition = true;
                        currentCol = col;
                    }
                    break;
            }
        }
       
        
    }
}
