using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnaEkranDebugMenu : MonoBehaviour, IPointerDownHandler
{
    public int týklamaLimiti = 10;
    private int týk;
    private Text baþlýkYazýsý;
    private void Start()
    {
        baþlýkYazýsý = GetComponent<Text>();
        týk = 0;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        týk++;

        if (týk > týklamaLimiti)
        {
            baþlýkYazýsý.text = "Tebrikler, artýk havalýsýn.";

            switch (týk - týklamaLimiti)
            {
                case 1:
                    var finalText =
                        $"Ses: {PlayerPrefs.GetInt("ses", -1)}, Bildirim: {PlayerPrefs.GetInt("bildirim", -1)}, " +
                        $"Yüksek Skor: {PlayerPrefs.GetInt("yüksekSkor", -1)}, Zor Yüksek Skor: {PlayerPrefs.GetInt("zorYüksekSkor", -1)}, " +
                        $"Zor Yüksek Skor: {PlayerPrefs.GetInt("oyuncuYüksekSkor", -1)}";
                    baþlýkYazýsý.text = finalText;
                    break;
                case 2:
                    baþlýkYazýsý.text = "Tebrikler, yüksek skorunu yetenekle geçtin.";
                    PlayerPrefs.SetInt("yüksekSkor", 100);
                    break;
                case 3:
                    baþlýkYazýsý.text = "Tebrikler, zor yüksek skorunu üstün yetenekle geçtin.";
                    PlayerPrefs.SetInt("zorYüksekSkor", 100);
                    break;
                case 4:
                    baþlýkYazýsý.text = "Tebrikler, oyuncu yüksek skorunu mükemmel yetenekle geçtin.";
                    PlayerPrefs.SetInt("oyuncuYüksekSkor", 100);
                    break;
                default:
                    baþlýkYazýsý.text = "Artýk bir þey yok bruh zorlama!";
                    break;
            }
        }
    }
}
