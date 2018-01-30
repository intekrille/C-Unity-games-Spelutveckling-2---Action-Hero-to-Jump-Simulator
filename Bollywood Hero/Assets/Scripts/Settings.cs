using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    [SerializeField] static public float mouseSensitivity = 80;
    public static float GetMouseSensitivity() { return mouseSensitivity; }
    public void SetMouseSensitivity(float f)
    {
        if (f >= 0 && f <= 100)
            mouseSensitivity = f;
    }
    
}
