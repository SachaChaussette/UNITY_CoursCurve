using UnityEngine;

public class CameraCurve : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float moveSpeed = 5.0f, rotationSpeed = 50.0f;
    [SerializeField] float radius = 5.0f, angle = 0.0f, heightOffset = 0.0f;
    [SerializeField] AnimationCurve heightCurve = null;

    void Start()
    {
        
    }

    void Update()
    {
        MoveTo();
        RotateTo();
    }

    // But : Faire une caméra orbital en code et utiliser une curve

    void MoveTo()
    {
        angle += Time.deltaTime * moveSpeed;
        angle %= 360.0f;
        float _x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float _z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        float _y = heightCurve.Evaluate(Time.realtimeSinceStartup);

        transform.position = target.position + new Vector3(_x, _y, _z);
    }

    void RotateTo()
    {
        Vector3 _dirLook = target.position - transform.position;

        // Empêche de pivoter si la cam et la target sont à la même position
        if (_dirLook == Vector3.zero) return;

        Quaternion _rotTowards = Quaternion.LookRotation(_dirLook);

        transform.rotation = Quaternion.RotateTowards(_rotTowards, transform.rotation, Time.deltaTime * rotationSpeed);
    }
}
