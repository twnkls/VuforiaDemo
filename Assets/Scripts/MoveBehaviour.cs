using UnityEngine;
using System.Collections;

public class MoveBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _moveDelay = 1f;
    [SerializeField] private float _moveDuration = 0.5f;
    private float _progress;
    private bool _waiting = false;
    private bool _moving = false;

    /// <summary>
    /// Called on enabling the GameObject
    /// </summary>
    private void OnEnable()
    {
        StartAnimate();
    }

    /// <summary>
    /// Called on disabling the GameObject
    /// </summary>
    private void OnDisable()
    {
        Reset();
    }

    /// <summary>
    /// Called when this GameObjects gets awoken.
    /// </summary>
    private void Awake()
    {
        if (_moveDuration == 0)
            _moveDuration = 1f;

        Reset();
    }

    /// <summary>
    /// Starts to animate this GameObject.
    /// </summary>
	public void StartAnimate()
    {
        if (_moveDelay > 0f)
        {
            _waiting = true;
            _moving = false;
        }
        else
        {
            _waiting = false;
            _moving = true;
        }

        _progress = 0f;
    }

    /// <summary>
    /// Stops to animate this GameObject.
    /// </summary>
    public void StopAnimate()
    {
        _waiting = false;
        _moving = false;
    }

    /// <summary>
    /// Reset the position of this GameObject.
    /// </summary>
    public void Reset()
    {
        transform.localPosition = _startPosition;
    }

    /// <summary>
    /// Updates the movement of this GameObject.
    /// </summary>
	private void Update()
    {
        // Delay before moving the GameObject
        if (_waiting)
        {
            if (_progress >= _moveDelay)
            {
                _progress = 0f;
                _waiting = false;
                _moving = true;
            }

            _progress += Time.deltaTime;
        }
        // Moving the GameObject
        else if (_moving)
        {
            if (_progress >= _moveDuration)
            {
                _progress = _moveDuration;
                _moving = false;
            }

            float percentage = _progress / _moveDuration;
            transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, percentage);

            _progress += Time.deltaTime;
        }
    }
}