using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public int target = 30;

    void Awake()
    {
        Application.targetFrameRate = target;
    }

    void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;
    }

    public GameObject completeScreen;

    public void EndGame()
    {
        Debug.Log("game over");
    }

    public void Explode(GameObject explosion, Vector3 place)
    {
        Instantiate(explosion, place, Quaternion.identity);
        Debug.Log("badaboem");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void GameEnd()
    {
        if(GameObject.FindGameObjectsWithTag("Demon").Length == 1)
        {
            completeScreen.SetActive(true);
            Time.timeScale = 0f;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }
    
}
