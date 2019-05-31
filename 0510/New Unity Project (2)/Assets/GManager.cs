using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerController;

public class GManager : MonoBehaviour
{
    GameObject[] player;
    List<GameObject> players = new List<GameObject>();
    List<GameObject> pl = new List<GameObject>();

    GameObject boss;
    public static int x = 1;       //游戏阶段
    Text text;
    List<Vector3> UIPos = new List<Vector3>()
    {
        new Vector3(0,0,0),new Vector3(1200,0,0),new Vector3(1400,0,0),new Vector3(1600,0,0)
    };
    bool isEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        text.gameObject.SetActive(false);
        foreach (var p in player)
        {
            players.Add(p);
            pl.Add(p);
            
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd)
        {
            System.Threading.Thread.Sleep(3000);
            SceneManager.LoadScene("GameOver");
        }
        if (x==1)
        {
            foreach (var p in players)
            {

                if (p.GetComponent<PlayerController>().team == PlayerController.PlayerTeam.Boss)
                {
                    x = 2;
                    boss = p;
                                  
                    players.Remove(p);
                    pl.Remove(p);
                    break;
                }
                
            }
        }
        else
        {
            Vector3 pos1 = boss.transform.GetChild(0).GetChild(0).transform.position;
            boss.transform.GetChild(0).GetChild(0).transform.position = Vector3.MoveTowards(pos1, UIPos[0], 1000);
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<PlayerController>().team = PlayerController.PlayerTeam.Team;
                Vector3 pos = players[i].transform.GetChild(0).GetChild(0).transform.position;
                players[i].transform.GetChild(0).GetChild(0).transform.position = Vector3.MoveTowards(pos, UIPos[i+1], 1000);

            }
            //foreach (var p in players)
            //{
            //    p.GetComponent<PlayerController>().team = PlayerController.PlayerTeam.Team;
            //}
            
        }
        if (IsBoss)
        {
            if (boss.GetComponent<PlayerController>().Hp==0)
            {
                text.gameObject.SetActive(true);
                text.text = "Team　Win!";
                isEnd = true;
            }
            else
            {
                for (int i = 0; i < pl.Count; i++)
                {
                    if (pl[i].GetComponent<PlayerController>().Hp==0)
                    {
                        pl.Remove(pl[i]);
                    }
                }
                if (pl.Count==0)
                {
                    text.gameObject.SetActive(true);
                    text.text = "Boss Win!";
                    isEnd = true;
                }
            }
        }


    }

    void changeUIPos()
    {

    }
}
