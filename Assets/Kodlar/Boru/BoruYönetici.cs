using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable InconsistentNaming

public class BoruYönetici : MonoBehaviour
{
    public OyunYönetici oyunYönetici;
    public GameObject borular;
    public RuntimeAnimatorController animKontrol;
    public float temelBoruHızı = 3f;
    public float temelSıklık = 1f;
    public int enHızlıOlunanPuan;
    public float zorlukKatı;

    public GameObject çevre;

    public Material kolayÇevre;
    public Material zorÇevre;
    public Material oyuncuÇevre;

    public Material kolayBoru;
    public Material zorBoru;
    public Material oyuncuBoru;

    private float _timeOffset;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private Material _çevreMateryali;
    private float _beklenenZaman;
    private bool _beklenenBoruVarMı;

    [HideInInspector] public float boruHızı;
    [HideInInspector] public float sıklık;

    private Animator borularAnimator;

    private void Start()
    {
        _çevreMateryali = OyunYönetici.ZorMu ? (OyunYönetici.OyuncuMu ? oyuncuÇevre : zorÇevre) : kolayÇevre;

        _çevreMateryali.SetTextureOffset(MainTex, Vector2.zero);
        oyunYönetici.OyunBitti += OyunBitince;
        borular.GetComponent<BoruHareketi>().boruYönetici = this;
        borularAnimator = borular.GetComponent<Animator>();
        //Debug.Log($"OyunYönetici.ZorMu: {OyunYönetici.ZorMu}, Ayarlayıcı.ZorMu: {AyarAyarlayıcı.Ayarlayıcı.zorMu}");
        foreach (Transform boruGrup in borular.transform.GetChild(0))
        {
            if (!boruGrup.name.StartsWith("boru")) continue;
            foreach (Transform boru in boruGrup.transform)
            {
                if (boru.name.StartsWith("M-"))
                {
                    boru.GetComponent<MeshRenderer>().material = OyunYönetici.ZorMu ?
                        (OyunYönetici.OyuncuMu ? oyuncuBoru : zorBoru) : kolayBoru;
                }
            }
        }

        foreach (Transform t in çevre.transform)
        {
            t.GetComponent<MeshRenderer>().material = _çevreMateryali;
        }

        if (OyunYönetici.ZorMu)
        {
            temelBoruHızı *= zorlukKatı;
            enHızlıOlunanPuan = Mathf.CeilToInt(enHızlıOlunanPuan / zorlukKatı);
        }
    }

    private void Update()
    {
        if (oyunYönetici.oyunBittiMi || oyunYönetici.oyunDurdurulduMu) return;

        var puan01 = Mathf.Min(1f, (float)oyunYönetici.Puan / enHızlıOlunanPuan);
        boruHızı = temelBoruHızı + puan01;
        sıklık = temelSıklık + puan01 / 2;

        SıklıklaBoruYap((6f / sıklık) / boruHızı);
    }

    private void SıklıklaBoruYap(float beklemeSüresi)
    {
        if (Time.time >= _beklenenZaman)
        {
            _beklenenBoruVarMı = false;
            BoruYap();
        }

        if (_beklenenBoruVarMı) return;

        _beklenenBoruVarMı = true;
        _beklenenZaman = Time.time + beklemeSüresi;

        // Debug.Log($"Zaman: {_beklenenZaman - beklemeSüresi}");
        // Debug.Log($"Beklenen zaman: {_beklenenZaman}");
        // Debug.Log($"Bekleme süresi: {beklemeSüresi}");

        if (Time.time >= _beklenenZaman)
        {
            _beklenenBoruVarMı = false;
            BoruYap();
        }
    }

    private void LateUpdate()
    {
        _timeOffset = -(boruHızı / 4);
        _çevreMateryali.SetTextureOffset(MainTex,
            new Vector2(_çevreMateryali.GetTextureOffset(MainTex).x + _timeOffset * Time.deltaTime, 0));
    }

    private void BoruYap()
    {
        var eklenenBorular = Instantiate(borular, new Vector3(15, 1 - Random.value * 2, 0), Quaternion.identity);
        Animator eklenenBorularAnimator = eklenenBorular.GetComponent<Animator>();

        if (AyarAyarlayıcı.Ayarlayıcı.zorMu)
        {
            if (Random.value > 0.7f)
            {
                eklenenBorularAnimator.runtimeAnimatorController = animKontrol;
                eklenenBorularAnimator.enabled = true;
                eklenenBorularAnimator.SetFloat("Offset", Random.value / 3);
                eklenenBorularAnimator.SetFloat("Speed", 1 + (Random.value - 0.5f) / 10);
                //Debug.Log(value);
            }
            else
            {
                eklenenBorularAnimator.enabled = false;
            }
        }

    }

    private void OyunBitince()
    {
        CancelInvoke(nameof(BoruYap));
        boruHızı = 0;
    }
}