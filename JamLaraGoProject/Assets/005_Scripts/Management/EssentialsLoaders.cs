using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EssentialsLoaders : MonoBehaviour
{
    [SerializeField] private GameObject audioMan, langueMan, sceneLoader, uiCanvasManager;

    private void Awake()
    {
        if (AllosiusDev.AudioManager.Instance == null)
        {
            Instantiate(audioMan);
        }

        if (LangueManager.Instance == null)
        {
            Instantiate(langueMan);
        }

        if(SceneLoader.Instance == null)
        {
            Instantiate(sceneLoader);
        }

        if(UICanvasManager.Instance == null)
        {
            Instantiate(uiCanvasManager);
        }
    }
}
