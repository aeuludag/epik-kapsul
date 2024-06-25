using Unity.Notifications.Android;
using UnityEngine;

[CreateAssetMenu(fileName = "Yeni Bildirim Yapısı", menuName = "Bildirim Yapısı", order = 0)]
public class BildirimYapısı : ScriptableObject
{
    public string başlık = "Günaydın!";
    [TextArea(1, int.MaxValue)] public string yazı = "Epik Kapsül oynamaya ne dersin?";
    
    public string küçükResimID = "kucuk_logo";
    public string büyükResimID = "buyuk_logo";

    public Color renk = new Color(1f, 0.72f, 0f);
    
    public static readonly Color Sarı = new Color(1f, 0.72f, 0f);

    public static BildirimYapısı Varsayılan()
    {
        // ReSharper disable once Unity.IncorrectScriptableObjectInstantiation
        var bildirim = new BildirimYapısı
        {
            başlık = "Günaydın!",
            yazı = "Epik Kapsül oynamaya ne dersin?",
            küçükResimID = "kucuk_logo",
            büyükResimID = "buyuk_logo",
            renk = Sarı
        };

        return bildirim;
    }

    public void VarsayılanaÇevir()
    {
        başlık = "Günaydın!";
        yazı = "Epik Kapsül oynamaya ne dersin?";
        küçükResimID = "kucuk_logo";
        büyükResimID = "buyuk_logo";
        renk = Sarı;
    }
    public AndroidNotification BildirimeÇevir()
    {
        var bildirim = new AndroidNotification
        {
            Title = başlık,
            Text = yazı,
            LargeIcon = büyükResimID,
            SmallIcon = küçükResimID,
            Color = renk
        };

        return bildirim;
    }
}