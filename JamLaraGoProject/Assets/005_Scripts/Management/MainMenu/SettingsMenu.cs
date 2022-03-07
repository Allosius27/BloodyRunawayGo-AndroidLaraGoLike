using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    #region Fields

    private MainMenu mainMenu;

    private Resolution[] resolutions;
    private List<Resolution> listRes = new List<Resolution>();
    private List<string> options = new List<string>();

    #endregion

    #region Properties

    public bool windowControls { get; set; }

    public TabButtonCtrl CurrentActiveTabSettingMenu => currentActiveTabSettingMenu;

    #endregion

    #region UnityInspector

    [SerializeField] private AudioMixer musicsAudioMixer, sfxAudioMixer, ambientsAudioMixer;

    [SerializeField] private Dropdown resolutionDropDown;

    [SerializeField] private GameObject settings;

    [SerializeField] private TabButtonCtrl currentActiveTabSettingMenu;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = FindObjectOfType<MainMenu>();

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        //resolutions = Screen.resolutions;
        Debug.Log(resolutions.Length);

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (listRes.Contains(resolutions[i]) == false)
            {
                float ratio = (float)(resolutions[i].width) / (float)(resolutions[i].height);
                Debug.Log(resolutions[i].width + " " + resolutions[i].height);
                Debug.Log(ratio);

                if (ratio >= 1.77f - 0.1f && ratio < 1.77f + 0.1f)
                {
                    listRes.Add(resolutions[i]);
                }
            }
        }

        resolutionDropDown.ClearOptions();

        //listRes.Add(Screen.currentResolution);
        //options.Add(Screen.currentResolution.width + "x" + Screen.currentResolution.height);

        int currentResolutionIndex = 0;
        for (int i = 0; i < listRes.Count; i++)
        {
            //listRes.Add(resolutions[i]);

            string option = listRes[i].width + "x" + listRes[i].height;
            Debug.Log(option);
            options.Add(option);

            if (listRes[i].width == Screen.width &&
                listRes[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);

        Debug.Log(listRes.Count);

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

        Screen.fullScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(mainMenu != null && mainMenu.activeSettings == true)
        {
            mainMenu.enabled = false;
            mainMenu.activeSettings = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitSettings();
        }
    }

    public void SetCurrentActiveTabSettingMenu(TabButtonCtrl _button)
    {
        currentActiveTabSettingMenu = _button;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = listRes[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusicsVolume(float volume)
    {
        musicsAudioMixer.SetFloat("volume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxAudioMixer.SetFloat("volume", volume);
    }

    public void SetAmbientsVolume(float volume)
    {
        ambientsAudioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void ExitSettings()
    {
        if (mainMenu != null)
        {
            mainMenu.activesButtons = true;
            mainMenu.enabled = true;
        }

        settings.SetActive(false);
    }
}
