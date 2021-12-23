using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

   

    Color vignetteAlpha;

    public Slider slider;
    // Start is called before the first frame update
    
    public void ChangeHealth( int healthValue)
    {
        slider.value = healthValue;
    }
}
