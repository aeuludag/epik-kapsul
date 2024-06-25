using UnityEngine;

public class EpikOyuncuYazısı : MonoBehaviour
{
    public int sınır;
    private void Start()
    { 
        gameObject.SetActive(PlayerPrefs.GetInt("oyuncuYüksekSkor", 0) > sınır);
    }
}