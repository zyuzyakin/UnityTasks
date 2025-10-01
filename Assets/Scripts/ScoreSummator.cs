using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreSummator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private TextMeshProUGUI finalText;

    [SerializeField] private TextMeshProUGUI rangeText;

    [SerializeField] private int winMin;
    [SerializeField] private int drawMin;

    private string range;
    private int resultValue;

    public int ResultValue 
    { 
        get => resultValue; 
        set 
        {
            resultValue = value;
            resultText.text = "Итог: " + ResultValue.ToString();
        }
    }
    public string Range
    {
        get => range;
        set
        {
            range = value;
            rangeText.text = value;
        }
    }

    public int WinMin
    {
        get => winMin;
        set => winMin = value;
    }
    public int DrawMin
    {
        get => drawMin;
        set => drawMin = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Face>(out var c))
            ResultValue += 7 - c.Value;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Face>(out var c))
            ResultValue -= 7 - c.Value;
    }

    public void FinalizeScore()
    {
        if (resultValue >= winMin)
        {
            finalText.text = "Победа";
        }
        else if (resultValue >= DrawMin)
        {
            finalText.text = "Ничья";
        }
        else
        {
            finalText.text = "Поражение";
        }
    }
    public void ResetResultValue() => ResultValue = 0;
}
