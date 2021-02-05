using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Tower")]
    [SerializeField] private Segment _segmentTemplate;
    [SerializeField] private Block _blockTemplate;
    [SerializeField] private Finish _finishTemplate;
    [SerializeField] private int _towerSize;
    [Header("Coin")]
    [SerializeField] private Coin _coinTemplate;
    [SerializeField] private int _coinCount;
    [SerializeField] private float _distanceBetweenCoin;
    [SerializeField] private Transform _ballPosition;
    [SerializeField] private float _startPointY;

    private void Start()
    {
        BuildTower();
        SpawnCoin();
    }

    private void BuildTower()
    {
        GameObject currentPoint = gameObject;

        for (int i = 0; i < _towerSize; i++)
        {
            currentPoint = BuildSegment(currentPoint, _segmentTemplate.gameObject);

            currentPoint = BuildSegment(currentPoint, _blockTemplate.gameObject);
        }

        BuildSegment(currentPoint, _finishTemplate.gameObject);
    }

    private GameObject BuildSegment(GameObject currentSegment, GameObject nextSegment)
    {
        return Instantiate(nextSegment, GetBuildPoint(currentSegment.transform, nextSegment.transform), Quaternion.identity, transform);
    }

    private Vector3 GetBuildPoint(Transform currentSegment, Transform nextSegment)
    {
        return new Vector3(transform.position.x, currentSegment.position.y + currentSegment.localScale.y / 2f + nextSegment.localScale.y / 2f, transform.position.z);
    }

    private void SpawnCoin()
    {
        float pointY = _startPointY;
        for (int i = 0; i < _coinCount; i++)
        {
            Vector3 pointSpawn = new Vector3(_ballPosition.transform.position.x, pointY, _ballPosition.transform.position.z);
            pointY += _distanceBetweenCoin;
            Instantiate(_coinTemplate.gameObject, pointSpawn, _coinTemplate.transform.rotation, transform);
        }
    }
}
