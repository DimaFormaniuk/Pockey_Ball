using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinSum : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    public event UnityAction<int> ChangeCoinSum;

    private int _value;

    private void Start()
    {
        _value = 0;
    }

    private void OnEnable()
    {
        _ball.AddCoin += OnAddCoin;
    }

    private void OnDisable()
    {
        _ball.AddCoin -= OnAddCoin;
    }

    private void OnAddCoin(int value)
    {
        _value += value;
        ChangeCoinSum?.Invoke(_value);
    }
}
