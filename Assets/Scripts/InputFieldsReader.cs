using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class InputFieldsReader : MonoBehaviour
{
    [SerializeField] private TMP_InputField countInput;
    [SerializeField] private TMP_InputField winInput;
    [SerializeField] private TMP_InputField drawInput;

    [SerializeField] private Button button;
    [SerializeField] private Slider slider;
    [SerializeField] private CubesLauncher launcher;
    [SerializeField] private ScoreSummator summator;

    
    private void Awake()
    {
        countInput.onEndEdit.AddListener(OnCountInputChanged);
        winInput.onEndEdit.AddListener(OnWinInputChanged);
        drawInput.onEndEdit.AddListener(OnDrawInputChanged);
        button.onClick.AddListener(OnButtonClick);
        
    }
    private void OnCountInputChanged(string arg)
    {
        if (int.TryParse(arg, out var value))
            launcher.CubesCount = value;
        
    }
    private void OnWinInputChanged(string arg)
    {
        if (int.TryParse(arg, out var value))
            summator.WinMin = value;
    }
    private void OnDrawInputChanged(string arg)
    {
        if (int.TryParse(arg, out var value))
            summator.DrawMin = value;
    }
    private void OnButtonClick()
    {
        if (launcher.IsCharging)
        {
            launcher.LaunchCubes();
            
            launcher.IsCharging = false;
            slider.value = 0f;
            launcher.Power = 0;
        }
        else
        {
            launcher.IsCharging = true;
        }
    }

    private void FixedUpdate()
    {
        if (launcher.IsCharging)
        {
            launcher.Power += 0.01f;
            slider.value += 0.01f;
        }
    }
}
