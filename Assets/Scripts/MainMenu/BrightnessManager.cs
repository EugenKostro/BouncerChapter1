using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessManager : MonoBehaviour
{
    public void SetBrightness(float brightness)
    {
        RenderSettings.ambientLight = Color.white * brightness;  
    }
}
