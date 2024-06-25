using UnityEngine;

public class BulutYönetici : MonoBehaviour
{
    public OyunYönetici oyunYönetici;
    public BoruYönetici boruYönetici;
    public GameObject bulut;
    [HideInInspector] public float bulutHızı = 3f;

    private void Start()
    {
        oyunYönetici.OyunBitti += OyunBitince;
        bulut.GetComponent<BulutHareketi>().bulutYönetici = this;
        InvokeRepeating(nameof(BulutYap), 0f, 1.5f);
    }

    private void BulutYap()
    {
        bulutHızı = boruYönetici.boruHızı;
        Instantiate(bulut);
    }
    
    private void OyunBitince()
    {
        bulutHızı = 0;
        CancelInvoke(nameof(BulutYap));
    }
}
