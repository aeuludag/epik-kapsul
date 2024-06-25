using UnityEngine;

public class UrlAçma : MonoBehaviour
{
    public void URLAç(string url)
    {
        Application.OpenURL(url);
    }
}