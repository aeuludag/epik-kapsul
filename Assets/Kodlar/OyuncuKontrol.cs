using UnityEngine;

public class OyuncuKontrol : MonoBehaviour
{
    public float yukariHareket = 3f;
    public OyunYönetici oyunYönetici;
    public GameObject patlama;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (oyunYönetici.oyunDurdurulduMu)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Zıpla();
        }

        YönAyarla();
    }

    public void Zıpla()
    {
        if (oyunYönetici.oyunBittiMi) return;
        Time.timeScale = 1;
        SesYönetici.Yönetici.SesOyna("Zıpla");
        _rb.velocity = new Vector3(0, yukariHareket);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("OyunBitiriciNesne"))
        {
            Instantiate(patlama, transform.position, Quaternion.identity);
            oyunYönetici.OyunBitir();
            //Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuanTrigger"))
        {
            SesYönetici.Yönetici.SesOyna("Puan");
            oyunYönetici.Puan++;
        }
    }

    private void YönAyarla()
    {
        var transform1 = transform;
        var eulerAngles = transform1.eulerAngles;

        eulerAngles = new Vector3(
            eulerAngles.x,
            eulerAngles.y,
            (_rb.velocity.y / 3) * 30 - 90
        );
        transform1.eulerAngles = eulerAngles;
    }
}