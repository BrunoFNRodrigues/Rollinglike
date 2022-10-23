using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToRoom : MonoBehaviour
{
    public int RoomNumber;

    public void MoveNextRoom()
    {
        int rnd = Random.Range(2,7);
        if(RoomNumber == rnd) 
        {
            rnd++;
        }
        if (RoomNumber == 2)
        {
            rnd = 0;
        }
        SceneManager.LoadScene(rnd);
    }

}
