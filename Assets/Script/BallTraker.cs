﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTraker : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, _ball.transform.position.y, transform.position.z);
        targetPosition += _offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.fixedDeltaTime);
    }
}