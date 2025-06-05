using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickVisibilityController : MonoBehaviour
{
    public GameObject joystickRoot;

    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        joystickRoot.SetActive(true);
#else
        joystickRoot.SetActive(false);
#endif
    }
}
