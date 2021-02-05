using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CoinSum _coinSum;

    private void OnEnable()
    {
        _coinSum.ChangeCoinSum += OnUpdateTextCoinValue;
    }

    private void OnDisable()
    {
        _coinSum.ChangeCoinSum -= OnUpdateTextCoinValue;
    }

    private void OnUpdateTextCoinValue(int value)
    {
        _text.text = value.ToString();
    }
}
