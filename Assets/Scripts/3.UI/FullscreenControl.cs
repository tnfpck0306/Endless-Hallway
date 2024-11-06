using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenControl : MonoBehaviour
{
    [SerializeField] private Toggle toggleButton;
    
    private void Awake()
    {
        toggleButton.onValueChanged.AddListener(ToggleFullScreen);
    }

    public void ToggleFullScreen(bool isOn)
    {
        if(isOn)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }


}
