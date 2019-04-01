using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //＊＊＊＊＊＊＊＊＊＊＊
    //Camera.main.fieldOfView　透視カメラの視野
    //Camera.main.orthographicSize  正交カメラの視野

    //プレイヤーの位置
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    //カメラの移動速度
    public float speed = 5.0f;
    
    //カメラフィールド
    private Camera thisCamera;

    //カメラ移動範囲を設定のため

    //マップの大さと同じ

    public GameObject rangeGameObject;

    private Collider2D rangeCol;

    private bool isDecreaseScale;

    private bool isInCreaseScale;

    private float lastCameraScale;

    private float currentCameraScale;
    public  void Awake()
    {
        //プレイヤーの位置情報を取得
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //カメラを取得
        thisCamera = GetComponent<Camera>();
        //コライダーで範囲を設定
        rangeCol = rangeGameObject.GetComponent<Collider2D>();      
    }
    private bool IsInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);

        Vector3 dir = (worldPos - camTransform.position).normalized;

        float dot = Vector3.Dot(camTransform.forward, dir);
        if (dot > 0 && viewPos.x >= 0.1&& viewPos.x <= 0.9&& viewPos.y >= 0.1 && viewPos.y <= 0.9)
            return true;
        else
            return false;
    }
    private bool IsInBorder(Vector3 position)
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(position);
        if (viewPos.x<=0.15f||viewPos.x>=0.85f||viewPos.y<=0.15f||viewPos.y>=0.85f)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
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

        ////プレイヤー位置を目標位置に設定
        //Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);
        ////今のカメラの位置を取得
        //Vector3 cameraPos= transform.position;


        //if (IsInView(player1.transform.position) && IsInView(player2.transform.position)
        //  && IsInView(player3.transform.position) && IsInView(player4.transform.position))
        //{
        //    if (IsNotInBorder(player1.transform.position) && IsNotInBorder(player2.transform.position)
        //     && IsNotInBorder(player3.transform.position) && IsNotInBorder(player4.transform.position))
        //    {
        //        if (Camera.main.orthographicSize>2)
        //        {
        //        Debug.Log("qweqwewe");
        //        Debug.Log(Camera.main.WorldToViewportPoint(player1.transform.position));
        //            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2, Time.deltaTime * speed);
        //        }

        //    }
        ////}

        //do
        //{
        //    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2, Time.deltaTime * speed);
        //} while ((IsInView(player1.transform.position) == false || IsInView(player2.transform.position) == false
        //  || IsInView(player3.transform.position) == false || IsInView(player4.transform.position) == false));


        if (IsInView(player1.transform.position) == false || IsInView(player2.transform.position) == false
        || IsInView(player3.transform.position) == false || IsInView(player4.transform.position) == false)
        {
            //if (isDecreaseScale)
            //{
            //    currentCameraScale =Mathf.Lerp(currentCameraScale, 5, Time.deltaTime * speed);
            //    Debug.Log(currentCameraScale);
            //    Debug.Log(lastCameraScale);
            //    if (currentCameraScale-lastCameraScale>=0.3f)
            //    {
            //        Camera.main.orthographicSize = currentCameraScale;
            //        isDecreaseScale = false;
            //    }
            //}
            //else
            //{

                if (Camera.main.orthographicSize <5)
               {
                    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,5, Time.deltaTime * 1);
                    lastCameraScale = Camera.main.orthographicSize;
                    isDecreaseScale = false;
                    isInCreaseScale = true;
                }
            //}
            
            //else if (IsInBorder(player1.transform.position)&&IsInBorder(player2.transform.position)
            //&&IsInBorder(player3.transform.position)&&IsInBorder(player4.transform.position))
            //{
            //    return;
            //}
        }
        else
        {
            //if (isInCreaseScale)
            //{
            //    currentCameraScale = Mathf.Lerp(currentCameraScale, 2, Time.deltaTime * speed);
            //    if (lastCameraScale-currentCameraScale>=0.3f)
            //    {
            //       // Camera.main.orthographicSize = currentCameraScale;
            //        isDecreaseScale = true;
            //        isInCreaseScale = false;
            //    }
            //}
            //else
            //{
                //Debug.Log("1");
                if (IsInBorder(player1.transform.position)||IsInBorder(player2.transform.position)
                    ||IsInBorder(player3.transform.position)||IsInBorder(player4.transform.position))
                {
                    return;
                }
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2, Time.deltaTime * 1);
                lastCameraScale = Camera.main.orthographicSize;
                isDecreaseScale = true;
                isInCreaseScale = false;
           // }
               
        }
        //m_Camera.orthographicSize = Mathf.Lerp(m_Camera.orthographicSize, m_Camera.orthographicSize += speed / 2, Time.deltaTime * 30f);
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2, 5);
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

        Vector3 offset = (player1.transform.position + player2.transform.position + player3.transform.position + player4.transform.position) / 4;

        offset.x = Mathf.Clamp(offset.x, minRange.x + cameraW, maxRange.x - cameraW);

        offset.y = Mathf.Clamp(offset.y, minRange.y + cameraH, maxRange.y - cameraH);
        //カメラ移動範囲（ｘ軸とｙ軸）を設定
        transform.position =new Vector3(offset.x,offset.y,transform.position.z);
        

        ////カメラの移動条件を判定
        //if (targetPos.y > cameraPos.y + cameraH - 1 || targetPos.y < cameraPos.y - cameraH + 1
        //    || targetPos.x < cameraPos.x - cameraW + 1 || targetPos.x > cameraPos.x + cameraW - 1)
        //{
        //    transform.position = Vector3.Lerp(cameraPos, targetPos, speed * Time.deltaTime);
        //}
    }

}
