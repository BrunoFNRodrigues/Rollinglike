using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToRoom : MonoBehaviour
{
    public int RoomNumber;

    public void MoveNextRoom()
    {
        int rnd = Random.Range(0,4);
        if(rnd != 0)
        {
            rnd = Random.Range(1,7);
            if (RoomNumber == rnd)
            {
                rnd++;
            }

        } else
        {
            rnd = 8;
        }


        SceneManager.LoadScene(rnd);
    }

}
