using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGeçici : MonoBehaviour
{
    public static void SahneyeGit(int sahne)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sahne);
    }
}
