using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float dropTime;
    public GameObject prefabStar;
    public GameObject BlackWhole;
    public GameObject Firebomb;

    public MeteorType type;
    GameObject newmeteor;


    Vector3 parScale;
    Vector3 childScale;
    // Start is called before the first frame update
    void Start()
    {
        parScale = transform.localScale;
        childScale = transform.GetChild(0).transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).transform.position = endPos;
        if (transform.position == endPos)
        {
            switch (type)
            {
                case MeteorType.PointStar:
                    newmeteor = Instantiate(prefabStar, transform.position+new Vector3(0,-1,0), Quaternion.identity);
                    break;
                case MeteorType.BlackWhole:
                    newmeteor = Instantiate(BlackWhole, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                    
                    break;
                case MeteorType.Icebomb:
                    break;
                case MeteorType.Firebomb:
                    newmeteor = Instantiate(Firebomb, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                    break;




                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos, Vector3.Distance(startPos, endPos) / dropTime * Time.deltaTime);
        if (transform.localScale.x > 0)
        {
            transform.localScale += new Vector3(3 / dropTime, 3 / dropTime, 0) * Time.deltaTime;
        }
        else
        {
            transform.localScale += new Vector3(-3 / dropTime, 3 / dropTime, 0) * Time.deltaTime;
        }
        float x = transform.localScale.x / parScale.x;
        transform.GetChild(0).transform.localScale = childScale / x;
    }


    public enum MeteorType
    {
        PointStar,
        BlackWhole,
        Firebomb,
        Icebomb,
    }
}
