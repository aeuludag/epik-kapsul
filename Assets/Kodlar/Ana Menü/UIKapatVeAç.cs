using UnityEngine;

public class UIKapatVeAç : MonoBehaviour
{
    public bool gözüküyorMu;
    public GameObject[] gözükecekNesneler;
    public GameObject[] kapatılacakNesneler;

    public void KapatYaDaAç()
    {
        gözüküyorMu = !gözüküyorMu;
        foreach (var o in gözükecekNesneler)
        {
            o.SetActive(gözüküyorMu);
        }

        foreach (var o in kapatılacakNesneler)
        {
            o.SetActive(!gözüküyorMu);
        }
    }

    public void DurumBelirle(bool durum)
    {
        if (durum == gözüküyorMu) return;
        KapatYaDaAç();
    }
}