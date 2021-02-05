using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Stick : MonoBehaviour
{
    [SerializeField] private Transform _pointForBall;

    public Transform PointForBall => _pointForBall;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateBlend(float value)
    {
        _animator.SetFloat("Blend", value);
    }
}
