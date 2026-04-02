using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PursuitBehavior : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float maxForce = 8f;
    [SerializeField] private float predictionAccuracy = 0.5f;

    private Rigidbody2D _rb;
    private Rigidbody2D _targetRb;
    private Transform _target;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _target = player.transform;
            _targetRb = player.GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        if (_target == null) return;

        Vector2 toTarget = (Vector2)_target.position - _rb.position;
        float distance = toTarget.magnitude;

        float speed = _rb.linearVelocity.magnitude;
        float predictionTime = speed > 0.01f
            ? Mathf.Min(distance / speed * predictionAccuracy, 1f)
            : predictionAccuracy;

        Vector2 targetVelocity =  _targetRb.linearVelocity;
        Vector2 predictedPos = (Vector2)_target.position + targetVelocity * predictionTime;

        Vector2 destination = (predictedPos - _rb.position).normalized * maxSpeed;
        Vector2 destinationForce = destination - _rb.linearVelocity;
        destinationForce = Vector2.ClampMagnitude(destinationForce, maxForce);

        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity + destinationForce * Time.fixedDeltaTime, maxSpeed);

        if (_rb.linearVelocity.magnitude > 0.01f)
        {
            float angle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }

    void OnDrawGizmos()
    {
        if (_target == null) return;

        Vector2 targetVelocity = _targetRb != null ? _targetRb.linearVelocity : Vector2.zero;
        float predictionTime = predictionAccuracy;
        Vector2 predicted = (Vector2)_target.position + targetVelocity * predictionTime;

        Gizmos.color = new Color(1f, 0f, 0f, 0.4f);
        Gizmos.DrawLine(transform.position, _target.position);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, predicted);
        Gizmos.DrawWireSphere(predicted, 0.2f);
    }
}