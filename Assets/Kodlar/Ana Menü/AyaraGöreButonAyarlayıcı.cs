using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AyaraGöreButonAyarlayıcı : MonoBehaviour
{
    [FormerlySerializedAs("zorButon")] public GameObject ayarlanacakButon;
    public string playerPref;
    public int değerGereksinim;
    [FormerlySerializedAs("zorModBilgi")] public string bilgiYazısı;

    private void Start()
    {
        ayarlanacakButon.transform.GetChild(0).GetComponent<Text>().text =
            string.Format(bilgiYazısı, değerGereksinim);

        ButonDurumunuBelirle();
    }
    
    public void ButonDurumunuBelirle()
    {
        var zorModAçıkMı = PlayerPrefs.GetInt(playerPref, 0) >= değerGereksinim;
        ayarlanacakButon.GetComponent<Button>().interactable = zorModAçıkMı;
        ayarlanacakButon.GetComponent<UIKapatVeAç>().DurumBelirle(zorModAçıkMı);
    }
}