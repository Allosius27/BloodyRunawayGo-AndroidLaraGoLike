using UnityEngine;
using TMPro;

public class FpsCounterDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _fpsCountText;
    
    private float[] _frameDeltaTimeArray;
    private int _lastFrameIndex;
    
    private void Awake()
    {
        _frameDeltaTimeArray = new float[50];
    }
    
    private void Update()
    {
        UpdateUiText();
    }

    private void UpdateUiText()
    {
        _frameDeltaTimeArray[_lastFrameIndex] = Time.deltaTime;
        _lastFrameIndex = (_lastFrameIndex + 1) % _frameDeltaTimeArray.Length;

        _fpsCountText.text = Mathf.RoundToInt(CalculateFps()).ToString();
    }
    
    private float CalculateFps()
    {
        float total = 0f;

        foreach (float deltaTime in _frameDeltaTimeArray)
        {
            total += deltaTime;
        }

        return _frameDeltaTimeArray.Length / total;
    }
}
