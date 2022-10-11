using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToRoom1 : MonoBehaviour
{
    public int RoomNumber;
    public void MoveNextRoom()
    {
        SceneManager.LoadScene(RoomNumber);
    }

}
