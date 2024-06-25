using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
[CreateAssetMenu(fileName = "Yeni Ses", menuName = "Ses Nesnesi", order = 0)]
public class Ses : ScriptableObject
{
    public string isim;
    public AudioClip klip;
    [Range(0f, 1f)] public float düzey = 1f;
    public bool tekrarla;
    public bool uyanıştaOynat;
    [HideInInspector] public AudioSource kaynak;
}