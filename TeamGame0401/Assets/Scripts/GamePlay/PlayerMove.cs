using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   
    Rigidbody2D rigid; 　　　　　　　　　　　 //自分もっているのRigidbodyを取得

    public float speed;   　　　　　　　　　　//プレイヤーの移動速度

    public int playerNumber;  　　　　　　　 //名前でコントローラーを取得できるためのナンバー

    public ItemMove itemMoveObj;　　　　　　　//持っているのアイテムを取得

    public Stone stoneObjCode;　　　　　　　　//マップ上の石を取得

    private ItemData selectedItem;　　　　　　//空のアイテムデータを生成

    public Transform attackStartPosition;　　 //プレイヤー攻撃開始の位置を取得

    private bool isSetItemPosition = false;　 //アイテム攻撃開始位置を設定するかどうか

    private bool isSetStonePosition = false;  //石攻撃開始位置を設定するかどうか

    private float arrowAngle;　　　　　　　  //矢印の回転角度

    public Transform arrowPositionTra;　　　 //矢印のポジションを取得

    public Transform  arrowRotationTra;  　  //矢印の回転方向を取得

    public GameObject arrowPrefabs;　　　　  //矢印プレハブオブジェクを取得

    private GameObject arrowGob;　　　　　   //矢印オブジェクを取得

    private Collider2D currentCol;　　　　   //あたり判定用のコライダー2Dを生成

    private  Vector3 rotation;　　　　　   //回転用のベクトル座標

    float h,v,x,y;　　　　　　　　　　　　　   //ｈ、ｖでプレイやー移動方向表示

    private string  direction;         　　 //現在プレイヤーの方向

    public float hp = 100;　　　　　　　　　//プレイやーのＨＰ　

    public int coinCnt=0;　　　　　　　 　　//今プレイヤーもっているのコイン計算カウンター

    public static bool isBoss = false;  　　//プレイヤーはボスになるかどうか
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        hp = 100;
        rigid = GetComponent<Rigidbody2D>();
        arrowGob = Instantiate(arrowPrefabs, arrowPositionTra.position, Quaternion.identity);
        arrowGob.SetActive(false);
        coinCnt = 0;
        isBoss = false;
    }
    private void Update()
    {
        //プレイヤーがコイン五つ集めたら、ボスになります（スケール拡大）
        if (coinCnt == 5 && transform.localScale.x <= 3)
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        }
        //アイテム攻撃開始するかどうかを判定
        if (isSetItemPosition&&selectedItem!=null&&itemMoveObj!=null)
        {
            //まだ攻撃始めないとき、アイテムを開始位置に設定
            itemMoveObj.SetPosition(attackStartPosition.position);
        }
        //石攻撃開始するかどうかを判定
        if (isSetStonePosition&&stoneObjCode!=null)
        {
            //まだ攻撃始めないとき、石を開始位置に設定
            stoneObjCode.SetPosition(attackStartPosition.position);
        }
        //プレイヤーがマップ上のオブジェクに衝突するとき
        if (currentCol!=null)
        {
            //コントローラーの指定キーが押されたかどうかを判定
            if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                //石に衝突するとき
                if (currentCol.gameObject.tag=="Stone")
                {
                    //開始位置によって、攻撃初めて
                    isSetStonePosition = false;
                    AttackStone();
                }
                else if(currentCol.gameObject.tag=="Item1")
                {
                    //石ではないときは、アイテム攻撃を初めて
                    isSetItemPosition = false;
                    AttackItem();
                }

            }
        }
    }
    /// <summary>
    /// プレイヤーの移動処理と矢印の設定メソッドを読み込む
    /// </summary>
    private void FixedUpdate()
    {
        if (playerNumber == 0)
        {
            return;
        }
        Move();
        JokstickDirection();

        SetArrowPosition();
        SetArrowRotation();
    }
    /// <summary>
    /// プレイヤーの移動処理を行う
    /// </summary>
    private void Move()
    {
        //プレイヤーナンバーによって、コントローラーのスティック操作を取得
        h = Input.GetAxisRaw("Jokstick"+playerNumber+"X");
        v = (Input.GetAxisRaw("Jokstick"+playerNumber+ "Y"));
        //ｈの値によって、プレイヤーの画像方向を変えて
        if (h>0)
        {
            direction = "right";
            if (transform.rotation.y<180)
            {
                Quaternion qua = transform.rotation;
                qua.y = 180;
                transform.rotation = qua;
            }
        }
        else if (h<0)
        {
            direction = "left";
            if (transform.rotation.y>0)
            {
                Quaternion qua = transform.rotation;
                qua.y = 0;
                transform.rotation = qua;
            }
        }
        //移動量が小さすぎるときは処理しません
        if ((Mathf.Abs(h)<0.4&& Mathf.Abs(h)>0)|| (Mathf.Abs(v)<0.4&&Mathf.Abs(v)>0))
        {
            return;
        }
        rigid.velocity = new Vector2(speed * h, speed * v);

    }
    private void JokstickDirection()
    {
        x = Input.GetAxisRaw("Jokstick" + playerNumber + "AX");
        y = Input.GetAxisRaw("Jokstick" + playerNumber + "AY");
    }
    /// <summary>
    /// 矢印の位置を設定
    /// </summary>
    private void SetArrowPosition()
    {
         //矢印オブジェクが存在している＆＆ポジションを取得した
        if (arrowGob!=null&&arrowPositionTra!=null)
        {
            // 矢印の位置を取得したのポジションに設定
            arrowGob.transform.position = arrowPositionTra.position;
        }
     
    }
    /// <summary>
    /// 矢印の方向を設定
    /// </summary>
    private void SetArrowRotation()
    {
        //矢印の回転座標を取得しない&&オブジェクが存在しないとき
        if (arrowRotationTra==null&&arrowGob==null)
        {
            return;
        }
        //コントローラーのスティック方向によって、矢印の方向を設定
        if (x==0&&y==0)
        {
            arrowGob.SetActive(false);
            return;
        }
        else
        {
            arrowGob.SetActive(true) ;
        }
        if (x==0&&y>0.1f)
        {
            arrowAngle = 90;
        }
        else if (x==0&&y<-0.1f)
        {
            arrowAngle = 270;
        }
        else if (y==0&&x>0.1f)
        {
            arrowAngle = 0;
        }
        else if (y==0&&x<0.1f)
        {
            arrowAngle = 180;
        }
        else
        {
            arrowAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        }
        //回転座標を設定
        rotation = new Vector3(0, 0, arrowAngle);
        arrowRotationTra.rotation = Quaternion.Euler(rotation);
        arrowGob.transform.rotation = Quaternion.Euler(rotation);
    }
    

    /// <summary>
    /// 攻撃用のアイテムデータを取得
    /// </summary>
    /// <param name="itemData"></param>
    private void AttackItemData(ItemData itemData)
    {
        //データが存在しないとき、アイテムの位置を攻撃開始位置に設定
        if (itemData!=null)
        {
            isSetItemPosition = true;
        }
        //取得したのデータを空のitemData対象に代入
        selectedItem = itemData;       
    }
    /// <summary>
    /// 石の射出攻撃メソッド
    /// </summary>
    private void AttackStone()
    {
        //矢印の角度で石オブジェクのアタックメソッドを読み込む
        stoneObjCode.Attack(arrowAngle);
        //現在のコライダーを空にします
        currentCol = null;
    }

    /// <summary>
    /// アイテムの射出攻撃メソッド
    /// </summary>
    private void AttackItem()
    {
        if (itemMoveObj!=null)
        {
            //射出するオブジェクのデータを渡す
            //引数1：攻撃開始位置
            //引数2：攻撃終了位置
            //引数3：アイテムの移動速度
            //引数4：移動中最大の高さ
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
  
    //プレイヤーのあたり判定
    private void OnTriggerStay2D(Collider2D col)
    {
        if (currentCol == null)
        {
            switch (col.gameObject.tag)
            {
                //アイテムに衝突するとき
                case "Item1":
                    if (Input.GetKeyDown(KeyCode.JoystickButton4))
                    {
                        BuildManager.Instance.DataReturn(col, this.gameObject);
                        itemMoveObj = col.gameObject.GetComponent<ItemMove>();
                        currentCol = col;
                    }
                    break;
                //石に衝突するとき
                case "Stone":
                    // Debug.Log("dsdd");
                    if (Input.GetKeyDown(KeyCode.JoystickButton4))
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
