using UnityEngine;

public class BoruHareketi : MonoBehaviour
{
    public BoruYönetici boruYönetici;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rb.velocity = new Vector3(-boruYönetici.boruHızı, 0);
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }

    // public void BoruyuSıfırla()
    // {
    //     gameObject.SetActive(false);
    // }
}