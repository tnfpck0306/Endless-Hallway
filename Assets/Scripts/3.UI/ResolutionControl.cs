using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionControl : MonoBehaviour
{
    [SerializeField] private Dropdown resolutionDropdown;
    private List<Resolution> resolutions = new List<Resolution>();

    private void Start()
    {
        resolutionDropdown.ClearOptions();

        // 모든 해상도 옵션 가져오기
        foreach (Resolution res in Screen.resolutions)
        {
            resolutions.Add(res);
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.width + "x" + res.height));
        }

        // 현재 해상도 설정
        resolutionDropdown.value = resolutions.FindIndex(res => res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height);

        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value);  });
    }

    public void SetResolution(int index)
    {
        Resolution selectedResolution = resolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}
