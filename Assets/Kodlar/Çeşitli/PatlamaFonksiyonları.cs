using UnityEngine;

public class PatlamaFonksiyonları : MonoBehaviour
{
    public Ses patlamaSesi;

    private void Start()
    {
        SesYönetici.Yönetici.SesOyna(patlamaSesi);
        GameObject.Find("Kamera").GetComponent<KameraHareketi>().Oynat("Sarsıntı");
    }
}