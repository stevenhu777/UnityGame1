using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public enum CameraTrigger
    {
        center,
        left,
        right,
        up,
        down
    }
    //＊＊＊＊＊＊＊＊＊＊＊
    //Camera.main.fieldOfView　透視カメラの視野
    //Camera.main.orthographicSize  正交カメラの視野

    //プレイヤーの位置
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    //public GameObject player4;

    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> playersCenter = new List<GameObject>();
    //カメラの移動速度
    public float speed =0.5f;

    //カメラフィールド
    private Camera thisCamera;

    private Vector2 changePositiveView;
    private Vector2 changeNegativeView;
    private Vector2 changePositiveBorder;
    private Vector2 changeNegativeBorder;

    //カメラ移動範囲を設定のため

    //マップの大さと同じ

    public GameObject rangeGameObject;

    private Collider2D rangeCol;

    private Vector3 offset;

    private bool isInView = true;
    private bool isInBorder = false;

    //private bool isDecreaseScale;

    //private bool isInCreaseScale;

    //private float lastCameraScale;

    private float currentCameraScale;
    public void Awake()
    {
        //プレイヤーの位置情報を取得
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //カメラを取得
        thisCamera = GetComponent<Camera>();
        //コライダーで範囲を設定
        rangeCol = rangeGameObject.GetComponent<Collider2D>();
    }
    private bool IsInView(Vector3 worldPos, Vector2 positiveNum, Vector2 negativeNum)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);

        Vector3 dir = (worldPos - camTransform.position).normalized;
       // Debug.Log(negativeNum.x);
        //float dot = Vector3.Dot(camTransform.forward, dir);
        if (viewPos.x >= 0.08 + negativeNum.x && viewPos.x <= 0.92 + positiveNum.x &&
            viewPos.y >= 0.08 + negativeNum.y && viewPos.y <= 0.92 + positiveNum.y)
        {

            return true;
        }
        else
        {
            return false;
        }

    }
    private bool IsInBorder(Vector3 position)
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(position);
        if (viewPos.x <= 0.12f || viewPos.x >= 0.88f 
            || viewPos.y <= 0.12f || viewPos.y >= 0.88f )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //private bool IsInWorldBorder(Vector3 position)
    //{
    //    if ((Mathf.Abs(position.x)>=8.1f)||(Mathf.Abs(position.y)>=4.1f))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
       
    //}
    // Update is called once per frame
    private void FixedUpdate()
    {
        //マウスのホイールボタン
        //if (Input.GetAxis("Mouse ScrollWheel") < 0)
        //{

        //    //if (Camera.main.fieldOfView < 100)
        //    //{
        //    //    Camera.main.fieldOfView += 2;
        //    //}
        //    if (Camera.main.orthographicSize < 5)
        //    {
        //        Camera.main.orthographicSize += 0.5f;
        //    }
        //}
        //if (Input.GetAxis("Mouse ScrollWheel") > 0)
        //{
        //    //if (Camera.main.fieldOfView > 2)
        //    //{
        //    //    Camera.main.fieldOfView -= 2;
        //    //}
        //    if (Camera.main.orthographicSize > 2)
        //    {
        //        Camera.main.orthographicSize -= 0.5f;
        //    }
        //}

        if (player1 != null)
        {
            players.Add(player1);
            playersCenter.Add(player1);
        }
        if (player2 != null)
        {
            players.Add(player2);
            playersCenter.Add(player2);
        }
        if (player3 != null)
        {
            players.Add(player3);
            playersCenter.Add(player3);
        }
        //if (player4!=null)
        //{
        //    players.Add(player4);
        //}
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].transform.position.x >= 8.1f)
            {
                changePositiveView.x = 0.1f;
                //changeNegativeBorder.x = -0.05f;
            }
            if (players[i].transform.position.x <= -8.1f)
            {
                changeNegativeView.x = -0.1f;
                //changePositiveBorder.x = 0.05f;
            }
            if (players[i].transform.position.y >= 4.1f)
            {
                changePositiveView.y = 0.1f;
               // changeNegativeBorder.y = -0.05f;               
            }
            if (players[i].transform.position.y <= -4.1f)
            {
                changeNegativeView.y = -0.1f;
                //changePositiveBorder.y = 0.05f;
            }
        }
        if (players == null)
        {
            return;
        }
        for (int i = 0; i < players.Count; i++)
        {
            if (IsInView(players[i].transform.position, changePositiveView, changeNegativeView) == false)
            {
                IncreaseScale();
                isInView = false;
            }
        }
        if (isInView == true)
        {
            for (int j= 0; j < players.Count; j++)
            {
                if (IsInBorder(players[j].transform.position))
                {
                    isInBorder = true;
                    break;
                }
            }
            if (isInBorder==false)
            {
                DecreaseScale();
            }
            
        }
        //for (int i = 0; i < players.Count; i++)
        //{
        //    if (IsInView(players[i].transform.position)==false)
        //    {
        //        if (Camera.main.orthographicSize < 5)
        //        {
        //            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5, Time.deltaTime * 1);
        //            //lastCameraScale = Camera.main.orthographicSize;
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("wewe");
        //        for (int j = 0; j < players.Count; j++)
        //        {
        //            if (IsInBorder(players[j].transform.position))
        //            {

        //                return;
        //            }
        //        }
        //        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2, Time.deltaTime * 1);
        //        //lastCameraScale = Camera.main.orthographicSize;
        //        return;

        //    }
        //}

        isInView = true;
        isInBorder = false;

        changeNegativeView = Vector2.zero;
        changePositiveView = Vector2.zero;
        changePositiveBorder = Vector2.zero;
        changeNegativeBorder = Vector2.zero;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3, 5);
    }
    private void IncreaseScale()
    {
        if (Camera.main.orthographicSize < 5)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5, Time.deltaTime * speed);
            //lastCameraScale = Camera.main.orthographicSize;
        }
    }
    private void DecreaseScale()
    {
        if (Camera.main.orthographicSize > 3)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, Time.deltaTime * 1);
        }
    }
    private void LateUpdate()
    {
        //現在カメラの縦サイズ（height/2）
        float cameraH = thisCamera.orthographicSize;
        //現在カメラの横サイズ
        float cameraW = cameraH * thisCamera.aspect;

        //コライダー上の一番小さいの点
        Vector2 minRange = rangeCol.bounds.min;
        //コライダー上の一番大きいの点
        Vector2 maxRange = rangeCol.bounds.max;

        for (int i = 0; i < playersCenter.Count; i++)
        {
            offset += playersCenter[i].transform.position;
        }
        //offset = (player1.transform.position + player2.transform.position + player3.transform.position + player4.transform.position) / 4;
        offset = offset / playersCenter.Count;
        offset.x = Mathf.Clamp(offset.x, minRange.x + cameraW, maxRange.x - cameraW);

        offset.y = Mathf.Clamp(offset.y, minRange.y + cameraH, maxRange.y - cameraH);
        //カメラ移動範囲（ｘ軸とｙ軸）を設定
        transform.position = new Vector3(offset.x, offset.y, transform.position.z);


        ////カメラの移動条件を判定
        //if (targetPos.y > cameraPos.y + cameraH - 1 || targetPos.y < cameraPos.y - cameraH + 1
        //    || targetPos.x < cameraPos.x - cameraW + 1 || targetPos.x > cameraPos.x + cameraW - 1)
        //{
        //    transform.position = Vector3.Lerp(cameraPos, targetPos, speed * Time.deltaTime);
        //}
    }
}
