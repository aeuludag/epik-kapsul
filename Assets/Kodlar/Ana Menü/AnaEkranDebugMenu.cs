using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnaEkranDebugMenu : MonoBehaviour, IPointerDownHandler
{
    public int t�klamaLimiti = 10;
    private int t�k;
    private Text ba�l�kYaz�s�;
    private void Start()
    {
        ba�l�kYaz�s� = GetComponent<Text>();
        t�k = 0;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        t�k++;

        if (t�k > t�klamaLimiti)
        {
            ba�l�kYaz�s�.text = "Tebrikler, art�k haval�s�n.";

            switch (t�k - t�klamaLimiti)
            {
                case 1:
                    var finalText =
                        $"Ses: {PlayerPrefs.GetInt("ses", -1)}, Bildirim: {PlayerPrefs.GetInt("bildirim", -1)}, " +
                        $"Y�ksek Skor: {PlayerPrefs.GetInt("y�ksekSkor", -1)}, Zor Y�ksek Skor: {PlayerPrefs.GetInt("zorY�ksekSkor", -1)}, " +
                        $"Zor Y�ksek Skor: {PlayerPrefs.GetInt("oyuncuY�ksekSkor", -1)}";
                    ba�l�kYaz�s�.text = finalText;
                    break;
                case 2:
                    ba�l�kYaz�s�.text = "Tebrikler, y�ksek skorunu yetenekle ge�tin.";
                    PlayerPrefs.SetInt("y�ksekSkor", 100);
                    break;
                case 3:
                    ba�l�kYaz�s�.text = "Tebrikler, zor y�ksek skorunu �st�n yetenekle ge�tin.";
                    PlayerPrefs.SetInt("zorY�ksekSkor", 100);
                    break;
                case 4:
                    ba�l�kYaz�s�.text = "Tebrikler, oyuncu y�ksek skorunu m�kemmel yetenekle ge�tin.";
                    PlayerPrefs.SetInt("oyuncuY�ksekSkor", 100);
                    break;
                default:
                    ba�l�kYaz�s�.text = "Art�k bir �ey yok bruh zorlama!";
                    break;
            }
        }
    }
}
