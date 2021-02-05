using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Stick _stickTemplate;
    [SerializeField] private Transform _pointForStick;

    private Rigidbody _rigidbody;

    private float _power;
    private Stick _currentStick;
    private bool _canPlace;

    public event UnityAction FinishBlock;
    public event UnityAction<int> AddCoin;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _power = 0;
        _currentStick = CreateStick();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, Vector3.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out Block block))
                {
                    _rigidbody.isKinematic = false;
                    _rigidbody.velocity = Vector3.zero;
                }
                else if (hitInfo.collider.TryGetComponent(out Segment segment))
                {
                    _rigidbody.isKinematic = true;
                    _rigidbody.velocity = Vector3.zero;
                }
                else if (hitInfo.collider.TryGetComponent(out Finish finish))
                {
                    _rigidbody.isKinematic = true;
                    _rigidbody.velocity = Vector3.zero;
                    FinishBlock?.Invoke();
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (_currentStick == null && _rigidbody.isKinematic == true)
            {
                _power = 0;
                _currentStick = CreateStick();
            }

            _power += Time.deltaTime;
            _power = Mathf.Clamp(_power, 0f, 1f);

            if (_currentStick != null)
            {
                _currentStick.UpdateBlend(_power);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.parent = null;
            if (_currentStick != null)
            {
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(Vector3.up * _jumpForce * _power, ForceMode.Impulse);

                Destroy(_currentStick.gameObject);
            }

            _currentStick = null;
            transform.rotation = Quaternion.identity;// new Quaternion(0, 0, 0, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, -4f);
        }
    }

    private Stick CreateStick()
    {
        var stick = Instantiate(_stickTemplate.gameObject, _pointForStick.position, _stickTemplate.gameObject.transform.rotation).GetComponent<Stick>();
        transform.parent = stick.PointForBall;
        return stick;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            AddCoin?.Invoke(coin.Value);
            Destroy(coin.gameObject);
        }
    }
}
