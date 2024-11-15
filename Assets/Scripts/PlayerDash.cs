using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //Component's fields
    private Rigidbody2D _rigidbody;
    private TrailRenderer _trailRenderer;

    private float _dashingVelocity = 300f;
    private Vector2 _dashingDirection;
    private bool _isDashing;
    private bool _canDash = true;
    private float _cooldownDash = 1f;
    private float _dashingTime = 0.4f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        Dash();
    }

    public void Dash()
    {
        var dashInput = Input.GetButtonDown("Dash");
        Debug.Log(dashInput);
        if (dashInput && _canDash)
        {
            _isDashing = true;
            _canDash = false;
            _trailRenderer.emitting = true;
            _dashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            if (_dashingDirection == Vector2.zero)
            {
                _dashingDirection = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
        }

        if (_isDashing)
        {
            _rigidbody.velocity = _dashingDirection.normalized * _dashingVelocity;
            return;
        }

    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _isDashing = false;
        StartCoroutine(GrantPermissionForTheNextDash());
    }

    private IEnumerator GrantPermissionForTheNextDash()
    {
        yield return new WaitForSeconds(_cooldownDash);
        _canDash = true;
    }
}