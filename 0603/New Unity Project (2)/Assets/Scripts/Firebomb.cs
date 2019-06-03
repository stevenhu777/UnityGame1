using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;
using static Item;
public class Firebomb : MonoBehaviour
{
   // public float Damage;
    public PlayerTeam team;
   //float Speed;

    public explosionRange explosionRange;

    private AudioSource audio;
    public AudioClip bombClip;
    
  //  Vector2 attackVelocity;
    State state;

    public Vector3 positionPlus;
    private Vector3 m_StartPoint;    　//投擲開始位置

    private Vector3 m_EndPoint;      //投擲終了位置

    private Vector3 m_ContorlPoint;　//投擲コントロールポイント

    public float  speed=10f;             //スピード

    public float m_MaxDistance=3f;     //投擲の最大の高さ

    private float angle;

    private float m_StartTime;       //投げ飛ばす時間
    //private bool isAttackStart = false;　　//攻撃するかどうか
    private bool isAttackFinish;　　　　　//攻撃終わりを判定
    float x;
    float y;

    private Animator anim;

    private void Start()
    {
        team = gameObject.GetComponent<Item>().team;
        state = GetComponent<Item>().state;
        //  Speed = 30;
        // Damage = 35;
        explosionRange = GetComponentInChildren<explosionRange>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

    }
    /// <summary>
    ///　渡されたデータをアイテムに設定
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <param name="speed"></param>
    /// <param name="hight"></param>
    public void SetAttackData(Vector3 startPoint, Vector3 endPoint,float speed,float hight)
    {
        m_StartPoint = startPoint;
        m_EndPoint = endPoint;
        this.speed = speed;
        m_MaxDistance = hight;
        SetContorlPoint();
        
    }
    /// <summary>
    /// データによって投擲コントロールポイントを計算
    /// </summary>
    public void SetContorlPoint()
    {
        Vector3 distanceVec = new Vector3(Mathf.Abs(m_EndPoint.x - m_StartPoint.x), Mathf.Abs(m_EndPoint.y - m_StartPoint.y), 0);
        x = Mathf.Max(m_MaxDistance / 10, Mathf.Min(distanceVec.x, m_MaxDistance / 2));
        y = Mathf.Max(m_MaxDistance / 2, Mathf.Min(distanceVec.y, m_MaxDistance));
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 0, 0));
        //if (m_EndPoint.x < m_StartPoint.x)
        //{
        //    m_ContorlPoint = m_StartPoint + new Vector3(-x, y, 0);
        //}
        //else
        //{
        //    m_ContorlPoint = m_StartPoint + new Vector3(x, y, 0);
        //}
        //if (m_EndPoint.y>m_StartPoint.y)
        //{
        //    m_ContorlPoint=m_StartPoint+new Vector3(x,-y,0)
        //}
        if (m_EndPoint.x<m_StartPoint.x)
        {
            x = -x;
        }
        if (m_EndPoint.y > m_StartPoint.y)
        {
            y = y+(m_EndPoint.y-m_StartPoint.y);
        }
        m_ContorlPoint = m_StartPoint + new Vector3(x, y, 0);
        //isAttackStart = true;
        state = State.OnAttack;
    }
    void Update()
    {
        OutOfScreen();
        if (state==State.OnAttack)
        {
            //if (m_EndPoint.x < m_StartPoint.x)
            //{
            //    if (transform.position.x <= m_EndPoint.x)
            //    {
            //        //Destroy(this.gameObject);
            //        // return;

            //    }
            //}
            //else if (m_EndPoint.x > m_StartPoint.x)
            //{
            //    if (transform.position.x >= m_EndPoint.x)
            //    {
            //        // Destroy(this.gameObject);
            //        // return;
            //    }
            //}
            Vector3 position = m_EndPoint - transform.position;
            if (Mathf.Abs(position.magnitude)<=1.5f)
            {
                state = State.OnBomb;
                anim.SetTrigger("Bomb");
                audio.clip = bombClip;
                audio.Play();
                Destroy(this.gameObject, 0.667f);
            }
            else
            {
                GeneratingCurve();
            }

        }
        else if (state==State.OnBomb)
        {
            explosionRange.isAction = true;
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

        Vector3 rotDistance =new Vector2( Mathf.Abs( newPoint.x - oldPoint.x),Mathf.Abs(newPoint.y-oldPoint.y));
        angle = Mathf.Atan2(rotDistance.y, rotDistance.x) * Mathf.Rad2Deg;
        //if (newPoint.x < oldPoint.x)
        //{
        //    angle = -angle;
        //}
        //Debug.Log(angle);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.LookRotation(newPoint);
        transform.position = newPoint;
    }
    //private void Start()
    //{
    //    team = gameObject.GetComponent<Item>().team;
    //    Speed = 30;
    //    Damage = 35;
        
    //} 
    // Update is called once per frame
    //void Update()
    //{
    //    state = GetComponent<Item>().state;
    //    //attackVelocity = GetComponent<Item>().attackVelocity;
    //    //if (state == State.OnAttack)
    //    //{
    //    //    Attack();
    //    //}
    //    OutOfScreen();
    //}
    void OutOfScreen()
    {
        if (transform.position.x < -35 ||
            transform.position.x > 35 ||
            transform.position.y < -20 ||
            transform.position.y > 20)
        {
            Destroy(gameObject);
        }
    }
    //public void Attack()
    //{
    //    transform.position += new Vector3(attackVelocity.x * Speed * Time.deltaTime, attackVelocity.y * Speed * Time.deltaTime, 0);

    //}
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (state == State.OnAttack)
        //{
        //    GetComponent<Item>().state = State.OnBomb;
        //}     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnBecameInvisible()
    {

    }
}
