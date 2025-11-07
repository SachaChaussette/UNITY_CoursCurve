using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Spring : MonoBehaviour
{
    [SerializeField] AnimationCurve forceCurve = new AnimationCurve();
    [SerializeField] AnimationCurve bounceCurve = new AnimationCurve();
    [SerializeField] float springCompressionNormalized = 0.0f, forceMultiplier = 0.0f, springLength = 1.0f, upwardForce = 10.0f;
    [SerializeField] LayerMask mask = 0;
    [SerializeField] Rigidbody rb = null;

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        CalculateAscendingForce();
        ApplySuspension();
    }

    void Init()
    {
        rb = GetComponent<Rigidbody>();

    }

    void CalculateAscendingForce()
    {
        bool _hit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit _result, springLength, mask);

        if (!_hit) return;

        springCompressionNormalized = _result.distance / springLength;
        upwardForce = forceMultiplier * forceCurve.Evaluate(springCompressionNormalized);

        // Force Ascendante = Multiplicateur * la force de tension par rapport à la compression du ressort (curve)
    }

    void ApplySuspension()
    {
        // Tire un Rayon depuis la position vers le bas de la longueur du ressort
        bool _hit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit _result, springLength, mask);

        if (!_hit)
        {
            // Visualise les résultats
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * springLength, Color.red);
            return;
        }

        // Visualise les résultats
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _result.distance, Color.green);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * (springLength - _result.distance), Color.red);

        // Normalise la distance de hit par la longueur et on inverse pour représenter la charge via la curve de rebond
        float _forceY = upwardForce * bounceCurve.Evaluate(1.0f - (_result.distance / springLength));

        rb.AddForce(new Vector3(0.0f, _forceY, 0.0f));
    }
}
