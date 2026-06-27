using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerLight : MonoBehaviour
{
    [SerializeField] private Light2D _light;

    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _min = 0.5f;
    [SerializeField] private float _max = 1.3f;

    private float _seed;

    private void Start()
    {
        _seed = Random.value * 100;
    }

    private void Update()
    {
        float noise = Mathf.PerlinNoise(_seed, Time.time * _speed);

        _light.intensity = Mathf.Lerp(_min, _max, noise);
    }
}