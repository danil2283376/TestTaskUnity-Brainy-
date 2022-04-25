using UnityEngine;
using UnityEngine.UI;

public class CombinationRecognition : MonoBehaviour
{
    [SerializeField] private float _timeViewText = 1f;

    [SerializeField] private MovePlayer _movePlayer;
    [SerializeField] private RotationPlayer _rotationPlayer;
    [SerializeField] private WeaponPlayer _weaponPlayer;
    [SerializeField] private Text _combinationText;

    private float _currentTime = 0f;
    private bool _timeChangeText = true;
    private bool _startTimer = false;

    private void Start()
    {
        if (_movePlayer == null)
            throw new System.Exception("Script MovePlayer need set link!");
        if (_rotationPlayer == null)
            throw new System.Exception("Script RotationPlayer need set link!");
        if (_weaponPlayer == null)
            throw new System.Exception("Script WeaponPlayer need set link!");
        if (_combinationText == null)
            throw new System.Exception("need set link on Text CombinationText!");
    }

    private void Update()
    {
        SetText();
    }

    private void SetText()
    {

        if (_movePlayer.MoveState == MoveState.Move
            && _rotationPlayer.RotationState == RotationState.Rotation
            && _weaponPlayer.ReboundShot)
            ChangeText("Перемещение, разворот, выстрел с отскоком снаряда");

        else if (_movePlayer.MoveState == MoveState.Move
            && _weaponPlayer.ReboundShot)
            ChangeText("Перемещение, и выстрел с отскоком снаряда");

        else if (_movePlayer.MoveState == MoveState.Move
            && _rotationPlayer.RotationState == RotationState.Rotation
            && _weaponPlayer.Shooting)
            ChangeText("Перемещение, разворот и выстрел");

        else if (_movePlayer.DirectionState == DirectionState.MovingHorizontal
            && _weaponPlayer.Shooting)
            ChangeText("Боковое перемещение и выстрел");

        else if (_rotationPlayer.RotationState == RotationState.Rotation
            && _weaponPlayer.Shooting)
            ChangeText("Разворот и выстрел");
    }


    private void ChangeText(string text)
    {
        _combinationText.text = text;
        _timeChangeText = false;
        _startTimer = true;
    }
    public void ResetText()
    {
        _currentTime = 0f;
        _timeChangeText = true;
        _startTimer = false;
        _combinationText.text = "";
    }
}
