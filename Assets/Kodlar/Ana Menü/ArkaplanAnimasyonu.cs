using System;
using UnityEngine;

public class ArkaplanAnimasyonu : MonoBehaviour
{
    public Material arkaplanMateryali;
    private readonly int MainTex = Shader.PropertyToID("_MainTex"); 

    private void Update()
    {
        arkaplanMateryali.SetTextureOffset(MainTex, new Vector2(Time.time, Time.time));
    }
}
