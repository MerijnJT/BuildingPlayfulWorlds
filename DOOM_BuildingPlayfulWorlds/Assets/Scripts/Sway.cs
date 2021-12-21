using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    #region Variables 

    public float intensity;
    public float smoothOut;

    private Quaternion originRotation;
    #endregion


    #region Monobehaviour callbacks
    private void Start()
    {
        originRotation = transform.localRotation;
    }
    private void Update()
    {
        UpdateSway();
    }

    #endregion


    #region Private Methods

    private void UpdateSway()
    {
        //controls
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        //calculate the rotation
        Quaternion xAdjust = Quaternion.AngleAxis(-intensity * xMouse, Vector3.up);
        Quaternion yAdjust = Quaternion.AngleAxis(intensity * yMouse, Vector3.right);
        Quaternion targetRotation = originRotation * xAdjust * yAdjust;

        //rotate towards target rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smoothOut);

    }

    #endregion
}
