using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class KameraHareketi : MonoBehaviour
{
    public OyunYönetici oyunYönetici;
    private Animator _animator;

    private bool _animDevamEdiyorMu;
    private float timer;

    private void Start()
    {
        //enabled = OyunYönetici.OyuncuMu;
        _animator = GetComponent<Animator>();
    }

    public void AnimBitti()
    {
        _animator.enabled = false;
        _animDevamEdiyorMu = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (oyunYönetici.oyunBittiMi || oyunYönetici.oyunDurdurulduMu ||
            _animDevamEdiyorMu || !AyarAyarlayıcı.Ayarlayıcı.oyuncuMu) return;
        
        // print(timer/20000f);
        if (Random.value > 0.999 - timer/20000f)
        {
            if (Random.value > 0.3)
            {
                Oynat("Dönme");
            }
            else
            {
                Oynat("Zoom");
            }
        }
    }

    public void Oynat(string isim)
    {
        _animDevamEdiyorMu = true;
        _animator.enabled = true;
        _animator.Play(isim);
    }
}