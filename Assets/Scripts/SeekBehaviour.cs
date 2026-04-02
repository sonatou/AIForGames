using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeekBehavior : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float maxForce = 8f;

    private Rigidbody2D _rb;
    private Transform _target;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            _target = player.transform;
    }

    void FixedUpdate()
    {
        if (_target == null) return;

        Vector2 destination = ((Vector2)_target.position - _rb.position).normalized * maxSpeed;
        Vector2 destinationForce = destination - _rb.linearVelocity;
        destinationForce = Vector2.ClampMagnitude(destinationForce, maxForce);

        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity + destinationForce * Time.fixedDeltaTime, maxSpeed);
        if (_rb.linearVelocity.magnitude > 0.01f)
        {
            float angle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }
}