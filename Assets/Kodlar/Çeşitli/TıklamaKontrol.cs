using UnityEngine;
using UnityEngine.EventSystems;

public class TıklamaKontrol : MonoBehaviour, IPointerDownHandler
{
    public OyuncuKontrol oyuncuKontrol;

    // private void Update()
    // {
    //     
    // }

    public void OnPointerDown(PointerEventData eventData)
    {
       oyuncuKontrol.Zıpla();
    }
}