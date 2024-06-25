using UnityEngine;
using Random = UnityEngine.Random;

public class BulutHareketi : MonoBehaviour
{
    public BulutYönetici bulutYönetici;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        var transform1 = transform;
        transform1.position = new Vector3(20, Random.Range(-4f, 4f), 9f + Random.value);
        transform1.eulerAngles = new Vector3(Random.Range(0f, 360f), 0f, 0f);
        var boyut = Random.Range(0.5f, 1.5f);
        transform1.localScale =
            new Vector3(boyut, boyut, boyut);
    }

    private void Update()
    {
        _rb.velocity = new Vector3(-bulutYönetici.bulutHızı - 0.5f, 0, 0);
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}