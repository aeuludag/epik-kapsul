using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OyunYönetici : MonoBehaviour
{
    public Camera kamera;
    public OyuncuKontrol oyuncu;

    public int yüksekSkor;
    public bool oyunBittiMi;
    public bool oyunDurdurulduMu;

    public Color kolayArkaplanRenk;
    public Color zorArkaplanRenk;
    public Color oyuncuArkaplanRenk;

    public Text puanText;
    
    public GameObject durdurmaEkranı;
    public GameObject ölmeEkranı;
    public GameObject ölmeButonları;
    
    public Text dokunText;
    public Text[] yüksekSkorTextler;
    public GameObject yeniYüksekSkorText;

    private int _puan;

    public int Puan
    {
        get => _puan;
        set
        {
            _puan = value;
            puanText.text = _puan.ToString();
        }
    }

    public static bool ZorMu => AyarAyarlayıcı.Ayarlayıcı.zorMu;
    public static bool OyuncuMu => AyarAyarlayıcı.Ayarlayıcı.oyuncuMu;

    public delegate void OyunBittiHandler();

    [JetBrains.Annotations.CanBeNull] public event OyunBittiHandler OyunBitti;


    private void Start()
    {
        Time.timeScale = 0;
        oyunBittiMi = false;
        Puan = 0;
        yüksekSkor = PlayerPrefs.GetInt(ZorMu ? (OyuncuMu ? "oyuncuYüksekSkor" : "zorYüksekSkor") : "yüksekSkor", 0);
        kamera.backgroundColor = ZorMu ? (OyuncuMu ? oyuncuArkaplanRenk : zorArkaplanRenk) : kolayArkaplanRenk;
    }

    public void DurdurYaDaDevamEt()
    {
        Time.timeScale = 1 - Time.timeScale;
        oyunDurdurulduMu = Time.timeScale != 1f;
        durdurmaEkranı.SetActive(oyunDurdurulduMu);
    }

    public void OyunBitir()
    {
        if (oyunBittiMi)
        {
            return;
        }

        if (_puan > yüksekSkor)
        {
            foreach (var yüksekSkorText in yüksekSkorTextler)
            {
                yüksekSkorText.gameObject.SetActive(false);
            }

            yüksekSkor = _puan;
            yeniYüksekSkorText.SetActive(true);
            PlayerPrefs.SetInt(ZorMu ? (OyuncuMu ? "oyuncuYüksekSkor" : "zorYüksekSkor") : "yüksekSkor", yüksekSkor);
        }
        else
        {
            foreach (var yüksekSkorText in yüksekSkorTextler)
            {
                yüksekSkorText.gameObject.SetActive(true);
            }
        }

        oyunBittiMi = true;
        ölmeEkranı.SetActive(true);
        ölmeButonları.SetActive(true);
        dokunText.gameObject.SetActive(false);
        OyunBitti?.Invoke();
    }

    public void OyunaBaşla()
    {
        oyunDurdurulduMu = false;
        ölmeEkranı.SetActive(false);
        Time.timeScale = 1;
        oyuncu.Zıpla();
        foreach (var yüksekSkorText in yüksekSkorTextler)
        {
            yüksekSkorText.text = $"Yüksek Skor: {yüksekSkor}";
        }
    }

    public void BaştanBaşla()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}