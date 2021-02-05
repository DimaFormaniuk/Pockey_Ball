using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private float _speedRotate;

    public int Value => _value;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * _speedRotate * Time.fixedDeltaTime);
    }
}
