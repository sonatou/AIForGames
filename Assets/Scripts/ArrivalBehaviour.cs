using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArrivalBehavior : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float slowRadius = 3f;

    private Rigidbody2D _rb;
    private Transform _target;
    private bool _Istouching = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            _target = player.transform;
    }

    void FixedUpdate()
    {
        if (_target == null || _Istouching) return;

        Vector2 toTarget = (Vector2)_target.position - _rb.position;
        float distance = toTarget.magnitude;

        float targetSpeed = distance < slowRadius
            ? maxSpeed * (distance / slowRadius)
            : maxSpeed;

        _rb.linearVelocity = toTarget.normalized * targetSpeed;

        float angle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Player"))
        {
            _Istouching = true;
            _rb.linearVelocity = Vector2.zero;
        }
    }

    void OnCollisionExit2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Player"))
            _Istouching = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, slowRadius);
    }
}