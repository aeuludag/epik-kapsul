using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AyarAyarlayıcı : MonoBehaviour
{
    [Header("Ana Ekrana Özel")] public BildirimYönetici bildirimYönetici;

    public Button ayarlarButonu;

    public UIKapatVeAç sesUI;
    public UIKapatVeAç bildirimUI;

    public bool sesAyar;
    public bool bildirimAyar;

    [HideInInspector] public GameObject borular;
    public Material kolayBoru;
    public Material zorBoru;
    public Material oyuncuBoru;

    [Header("Genel")] public bool zorMu;
    public bool oyuncuMu;

    public static AyarAyarlayıcı Ayarlayıcı;

    private void Awake()
    {
        if (Ayarlayıcı is null)
        {
            Ayarlayıcı = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Ayarlayıcı != this || scene.buildIndex != 0) return;

        ayarlarButonu = GameObject.Find("Ayarlar Butonu").GetComponent<Button>();

        sesUI = FindObjectsOfType<UIKapatVeAç>(true)
            .Where(g => g.gameObject.name == "Ses Ayarı").ToArray()[0];
        bildirimUI = FindObjectsOfType<UIKapatVeAç>(true)
            .Where(g => g.gameObject.name == "Bildirim Ayarı").ToArray()[0];

        sesUI.gameObject.GetComponent<Button>().onClick.AddListener(SesAyarıKaydet);
        bildirimUI.gameObject.GetComponent<Button>().onClick.AddListener(BildirimAyarıKaydet);
        ayarlarButonu.onClick.AddListener(AyarlarıYükle);

        borular = GameObject.Find("Borular");
        BorularıAyarla();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) return;
        bildirimYönetici.BildirimleriGönder();
        sesAyar = PlayerPrefs.GetInt("ses", 1) == 1;
        SesYönetici.Yönetici.TümSeslerinDüzeyiniAyarla(sesAyar ? 1f : 0f);
    }

    public void AyarlarıYükle()
    {
        //print("Ayarlar yükleniyor...");
        sesAyar = PlayerPrefs.GetInt("ses", 1) == 1;
        SesYönetici.Yönetici.TümSeslerinDüzeyiniAyarla(sesAyar ? 1f : 0f);
        sesUI.DurumBelirle(sesAyar);

        bildirimAyar = PlayerPrefs.GetInt("bildirim", 1) == 1;
        bildirimYönetici.bildirimAçıkMı = bildirimAyar;
        bildirimYönetici.BildirimleriGönder();
        bildirimUI.DurumBelirle(bildirimAyar);
        //print($"Ses Ayar: {sesAyar}, Bildirim Ayar: {bildirimAyar} (AyarlarıYükle)");
    }

    public void SesAyarıKaydet()
    {
        sesAyar = sesUI.gözüküyorMu;
        PlayerPrefs.SetInt("ses", sesAyar ? 1 : 0);
        SesYönetici.Yönetici.TümSeslerinDüzeyiniAyarla(sesAyar ? 1f : 0f);
        //print($"Ses Ayar: {sesAyar}, Bildirim Ayar: {bildirimAyar} (SesAyarıKaydet)");
    }

    public void BildirimAyarıKaydet()
    {
        bildirimAyar = bildirimUI.gözüküyorMu;
        PlayerPrefs.SetInt("bildirim", bildirimAyar ? 1 : 0);
        bildirimYönetici.bildirimAçıkMı = bildirimAyar;
        bildirimYönetici.BildirimleriGönder();
        //print($"Ses Ayar: {sesAyar}, Bildirim Ayar: {bildirimAyar} (BildirimAyarıKaydet)");
    }

    public void AyarlarıKaydet()
    {
        SesAyarıKaydet();
        BildirimAyarıKaydet();
    }

    public void ZorModuAyarla(bool zorMod)
    {
        zorMu = zorMod;
        Ayarlayıcı.zorMu = zorMod;
    }

    public void OyuncuModunuAyarla(bool oyuncuMod)
    {
        oyuncuMu = oyuncuMod;
        Ayarlayıcı.oyuncuMu = oyuncuMod;
    }

    public void BorularıAyarla()
    {
        for (var i = 0; i < 2; i++)
        {
            foreach (Transform boru in borular.transform.GetChild(0).GetChild(i).transform)
            {
                if (boru.name.StartsWith("M-"))
                {
                    boru.GetComponent<MeshRenderer>().material = Ayarlayıcı.zorMu ?
                        (Ayarlayıcı.oyuncuMu ? oyuncuBoru : zorBoru) : kolayBoru;
                }
            }
        }
    }
}