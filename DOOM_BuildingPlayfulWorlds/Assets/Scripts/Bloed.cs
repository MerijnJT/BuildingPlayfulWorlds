using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bloed : MonoBehaviour
{

    
    public Image vignette;
    public GameObject klodder;

    public List<GameObject> klodders;
    

    private void Start()
    {
        vignette = GetComponent<Image>();
    }
    public void DAMAGEKRIJGEN(float healthValue)
    {
        Color vignetteAlpha = vignette.color;
        vignetteAlpha.a = (1 - (healthValue/150)) * 0.6f;
        vignette.color = vignetteAlpha;
    }
    
    public void Executing(Image versie)
    {
        foreach (GameObject g in klodders)
        {
            if (!g.activeSelf)
            {
                Color bloodAlpha = versie.color;
                bloodAlpha.a = (bloodAlpha.a + 0.1f);
                versie.color = bloodAlpha;

                float xPos = Random.Range(-Screen.width, Screen.width)/2;
                float yPos = Random.Range(-Screen.height, Screen.height)/2;

                Vector3 spawnPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y, 0f);
                spawnPosition.x += xPos;
                spawnPosition.y += yPos;
                g.transform.localPosition = spawnPosition;
                g.SetActive(true);
                break;
            }
        
        }
        
        
        
    }



}
