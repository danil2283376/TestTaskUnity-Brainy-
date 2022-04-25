using UnityEngine;
using UnityEngine.UI;

public class CounterGame : MonoBehaviour
{
    [SerializeField] private Text _counterText;
    [SerializeField] private int _scorePlayer = 0;
    [SerializeField] private int _scoreBot = 0;

    [SerializeField] GameObject _player;
    [SerializeField] GameObject _bot;

    private Vector3 _startPositionPlayer;
    private Vector3 _startPositionBot;
    private Quaternion _startAnglePlayer;
    private Quaternion _startAngleBot;

    void Start()
    {
        if (_counterText == null)
            throw new System.Exception("Need set link on Counter Text!");
        if (_player == null)
            throw new System.Exception("Need set link on Player!");
        if (_bot == null)
            throw new System.Exception("Need set link on Bot!");

        _startPositionPlayer = _player.transform.position;
        _startPositionBot = _bot.transform.position;

        _startAnglePlayer = _player.transform.rotation;
        _startAngleBot = _bot.transform.rotation;
    }

    public void SetPointPlayer()
    {
        _scorePlayer++;
        string updatedScore = _scorePlayer + ":" + _scoreBot;
        _counterText.text = updatedScore;
        ResetGame();
    }

    public void SetPointBot()
    {
        _scoreBot++;
        string updatedScore = _scorePlayer + ":" + _scoreBot;
        _counterText.text = updatedScore;
        ResetGame();
    }

    private void ResetGame() 
    {
        _player.transform.position = _startPositionPlayer;
        _player.transform.rotation = _startAnglePlayer;

        _bot.transform.position = _startPositionBot;
        _bot.transform.rotation = _startAngleBot;
    }
}
