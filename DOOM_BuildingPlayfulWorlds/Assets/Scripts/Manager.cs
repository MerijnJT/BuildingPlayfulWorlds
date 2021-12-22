using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public void EndGame()
    {
        Debug.Log("game over");
    }

    public void Explode(GameObject explosion, Vector3 place)
    {
        Instantiate(explosion, place, Quaternion.identity);
        Debug.Log("badaboem");
    }


}
