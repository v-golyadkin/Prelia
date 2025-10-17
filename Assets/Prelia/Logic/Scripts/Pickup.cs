using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{   
    public enum PickUpType
    {
        GoldCoin,
        Stamina,
        Health
    }

    [SerializeField] private PickUpType _pickUpType;
    [SerializeField] private float _pickUpDistance = 5f;
    [SerializeField] private float _accelarationRate = 0.2f;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private float _heightY = 1.5f;
    [SerializeField] private float _popDuration = 1f;

    private Vector3 _moveDirection;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    private void Start()
    {
        StartCoroutine(AnimationCurveSpawnRoutine());
    }

    private void Update()
    {
        Vector3 playerPosition = Player.Instance.transform.position;

        if(Vector3.Distance(transform.position, playerPosition) < _pickUpDistance)
        {
            _moveDirection = (playerPosition - transform.position).normalized;
            _moveSpeed += _accelarationRate;
        }
        else
        {
            _moveDirection = Vector3.zero;
            _moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDirection * _moveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().PickUp(_pickUpType);
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimationCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + UnityEngine.Random.Range(-2f, 2f);
        float randomY = transform.position.y + UnityEngine.Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while(timePassed < _popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _popDuration;
            float heighT = _animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, _heightY, heighT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }

    }
}
