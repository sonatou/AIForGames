using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FleeBehavior : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float maxForce = 8f;
    [SerializeField] private float panicRadius = 3f;

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

        Vector2 toTarget = (Vector2)_target.position - _rb.position;

        if (toTarget.magnitude > panicRadius)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 flee = -toTarget.normalized * maxSpeed;
        Vector2 fleeVelocity = flee - _rb.linearVelocity;
        fleeVelocity = Vector2.ClampMagnitude(fleeVelocity, maxForce);

        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity + fleeVelocity * Time.fixedDeltaTime, maxSpeed);

        if (_rb.linearVelocity.magnitude > 0.01f)
        {
            float angle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.3f, 0.3f, 0.4f);
        Gizmos.DrawWireSphere(transform.position, panicRadius);
    }
}