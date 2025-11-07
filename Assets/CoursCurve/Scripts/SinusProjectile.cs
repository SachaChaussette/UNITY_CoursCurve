using UnityEngine;

public class SinusProjectile : MonoBehaviour
{
    // frequency = frequence de l'oscillation
    // amplitude = son amplitude (merci françois)
    [SerializeField] float speed = 5.0f, frequency = 2.0f, amplitude = 1.0f, lifeSpan = 10.0f;
    [SerializeField] Vector3 startPosition = Vector3.zero;
    [SerializeField] float timer = 0.0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        timer += Time.deltaTime;

        if(timer >= lifeSpan)
        {
            startPosition = transform.position;
            timer = 0.0f;
        }

        // Calcule de la progression ( de 0 à 1 ) et application de la fonction easeInOutSine
        float _t = Mathf.Clamp01(timer / lifeSpan);
        float _easedT = EaseInOutSine(_t);

        // Mouvement linéaire sur l'axe Z
        Vector3 _linearMovement = startPosition + transform.forward * _easedT * speed;

        // Oscillation sinusoïdale sir l'axe X
        float _oscillation = Mathf.Sin(frequency * 2.0f * Mathf.PI * _t) * amplitude;
        Vector3 _oscillationOffset = transform.right * _oscillation;

        transform.position = _linearMovement + _oscillationOffset;
    }

    float EaseInOutSine(float _t)
    {
        return -(Mathf.Cos(Mathf.PI * _t) - 1.0f) / 2.0f;
    }
}

