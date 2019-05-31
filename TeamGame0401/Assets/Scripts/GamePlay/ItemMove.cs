using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{

    public Vector3 positionPlus;
    private Vector3 m_StartPoint;    　//投擲開始位置

    private Vector3 m_EndPoint;      //投擲終了位置

    private Vector3 m_ContorlPoint;　//投擲コントロールポイント

    private float speed;             //スピード

    private float m_MaxDistance;     //投擲の最大の高さ

    private float angle;　　　　　　　

    private float m_StartTime;       //投げ飛ばす時間

    private bool isAttackStart = false;　　//攻撃するかどうか

    private bool isAttackFinish;　　　　　//攻撃終わりを判定

    float x;

    float y;
    private static ItemMove instance;
    public static ItemMove Instance
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
    /// <summary>
    /// アイテムの位置を設定
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        //Debug.Log(m_StartPoint);
    }
    /// <summary>
    ///　渡されたデータをアイテムに設定
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <param name="speed"></param>
    /// <param name="hight"></param>
    public void SetAttackData(Vector3 startPoint,Vector3 endPoint,float speed,float hight)
    {
        m_StartPoint = startPoint;
        //m_EndPoint = endPoint;
        m_EndPoint = new Vector3(1,1,0);
        this.speed = speed;
        m_MaxDistance = hight;

        SetContorlPoint();      
    }

    /// <summary>
    /// データによって投擲コントロールポイントを計算
    /// </summary>
    public void SetContorlPoint()
    {
        Vector3 distanceVec =new Vector3( Mathf.Abs(m_EndPoint.x - m_StartPoint.x), Mathf.Abs(m_EndPoint.y - m_StartPoint.y),0);
        x = Mathf.Max(m_MaxDistance / 10, Mathf.Min(distanceVec.x, m_MaxDistance / 2));
        y = Mathf.Max(m_MaxDistance / 2, Mathf.Min(distanceVec.y, m_MaxDistance));
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 0, 0));
        if (m_EndPoint.x < m_StartPoint.x)
        {
            m_ContorlPoint = m_StartPoint +new Vector3(-x, y, 0);
        }
        else
        {
            m_ContorlPoint = m_StartPoint + new Vector3(x, y, 0);
        }
        isAttackStart = true;
    }


    void Update()
    {
        if (isAttackStart)
        {
            if (m_EndPoint.x < m_StartPoint.x)
            {
                if (transform.position.x<=m_EndPoint.x)
                {
                    //Destroy(this.gameObject);
                   // return;

                }
            }
            else if (m_EndPoint.x > m_StartPoint.x)
            {
                if (transform.position.x>=m_EndPoint.x)
                {
                   // Destroy(this.gameObject);
                   // return;
                }
            }       
            GeneratingCurve();    
        }
        
    }
    //ベジェ曲線
    private Vector3 CalculateCubicBezierPoint(float t, Vector3 startPoint, Vector3 endPoint, Vector3 controlPoint)
    {
        Vector3 PassPoint;
        PassPoint.x = t * t * (endPoint.x - 2 * controlPoint.x + startPoint.x) + startPoint.x + 2 * t * (controlPoint.x - startPoint.x);
        PassPoint.y = t * t * (endPoint.y - 2 * controlPoint.y + startPoint.y) + startPoint.y + 2 * t * (controlPoint.y - startPoint.y);
        PassPoint.z = t * t * (endPoint.z - 2 * controlPoint.z + startPoint.z) + startPoint.z + 2 * t * (controlPoint.z - startPoint.z);
        return PassPoint;
    }
    void GeneratingCurve()
    {
        Vector3 oldPoint = CalculateCubicBezierPoint(m_StartTime, m_StartPoint, m_EndPoint, m_ContorlPoint);
        Vector3 newPoint = CalculateCubicBezierPoint(m_StartTime += (Time.deltaTime * speed), m_StartPoint, m_EndPoint, m_ContorlPoint);

        Vector3 rotDistance = newPoint - oldPoint;
        angle = Mathf.Atan2(rotDistance.y ,rotDistance.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = newPoint;
    }
}
