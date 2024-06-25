using System;
using UnityEngine;


public class SesYönetici : MonoBehaviour
{
    public Ses[] sesler;
    public static SesYönetici Yönetici;

    private void Awake()
    {
        if (Yönetici is null)
        {
            Yönetici = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (var ses in sesler)
        {
            ses.kaynak = gameObject.AddComponent<AudioSource>();
            ses.kaynak.clip = ses.klip;
            ses.kaynak.volume = ses.düzey;
            ses.kaynak.loop = ses.tekrarla;
            ses.kaynak.playOnAwake = ses.uyanıştaOynat;
        }
    }

    public void SesOyna(string sesİsmi)
    {
        var ses = Array.Find(sesler, ses => ses.isim == sesİsmi);
        ses.kaynak.Play();
    }

    public void SesOyna(Ses ses)
    {
        ses.kaynak.Play();
    }

    public void TümSeslerinDüzeyiniAyarla(float düzey)
    {
        foreach (var ses in sesler)
        {
            ses.düzey = düzey;
            ses.kaynak.volume = düzey;
        }
    }
}