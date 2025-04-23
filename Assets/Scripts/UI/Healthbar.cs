using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health _health;

    [SerializeField] private RectTransform _barRect;

    [SerializeField] private RectMask2D _mask;

    private float _maxRightMask;
    private float _intialRightMask;

    private void Start()
    {
        //x = left , w = top , y = bottom, z = right
        _maxRightMask = _barRect.rect.width - _mask.padding.x - _mask.padding.z;
        _intialRightMask = _mask.padding.z;
    }

    public void SetValue(int newValue)
    {
        var targetWidth = newValue * _maxRightMask; // _health.MaxHp;
        var newRightMask = _maxRightMask + _intialRightMask - targetWidth;
        var padding = _mask.padding;
        padding.z = newRightMask;
        _mask.padding = padding;
    }
}
