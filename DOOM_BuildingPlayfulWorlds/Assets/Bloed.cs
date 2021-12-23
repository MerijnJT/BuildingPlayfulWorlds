using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bloed : MonoBehaviour
{

    public Image blood;
    public Image vignette;

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
    

}
