using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetJokStick : MonoBehaviour
{
    public GameObject[] players;
    private List<int> numbers =new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
    private int playerCount=0;
    private bool isSetOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSetOver)
        {
            return;
        }
        for (int i = 0; i < numbers.Count; i++)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Jokstick" + numbers[i] + "X")) > 0 ||
                    Mathf.Abs(Input.GetAxisRaw("Jokstick" + numbers[i] + "Y")) > 0)
            {
                if (playerCount<players.Length)
                {
                    players[playerCount].GetComponent<PlayerMove>().playerNumber = numbers[i];
                    playerCount++;
                }      
                if (playerCount>=players.Length)
                {
                    isSetOver=true;
                }
                numbers.Remove(numbers[i]);
            }
        }

       
    }
}
