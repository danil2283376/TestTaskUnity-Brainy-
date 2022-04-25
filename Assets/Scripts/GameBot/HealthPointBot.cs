using UnityEngine;
using UnityEngine.UI;

public class HealthPointBot : MonoBehaviour, IHealthPoint
{
    [SerializeField] private CounterGame _counterScore;
    [SerializeField] private CombinationRecognition _combinationText;

    private void Start()
    {
        if (_counterScore == null)
            throw new System.Exception("Need set link on script Counter Score!");
        if (_combinationText == null)
            throw new System.Exception("Need set link on script Combination Recognition!");
    }

    public void Dead()
    {
        _counterScore.SetPointBot();
        _combinationText.ResetText();
    }
}
