using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private void OnEnable()
    {
        _ball.FinishBlock += OnFinishLevel;
    }

    private void OnDisable()
    {
        _ball.FinishBlock -= OnFinishLevel;
    }

    private void OnFinishLevel()
    {
        Debug.Log("Level Finish");
    }
}
