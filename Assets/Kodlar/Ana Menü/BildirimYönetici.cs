using System;
using UnityEngine;
using Unity.Notifications.Android;
using Random = UnityEngine.Random;

public class BildirimYönetici : MonoBehaviour
{
    public BildirimYapısı[] bildirimler;
    public bool bildirimAçıkMı;

    private DateTime _bildirimZamanı;
    private AndroidNotificationChannel _günlükBildirimKanal;
    private AndroidNotification _bildirim;

    private void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        BildirimleriAyarla();
    }

    private void BildirimleriAyarla()
    {
        if (!bildirimAçıkMı)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            return;
        }

        _bildirimZamanı = DateTime.Today.AddHours(17);

        _günlükBildirimKanal = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Günlük Hatırlatıcı",
            Description = "Günlük kapsülcülük hatırlatması, oyuna 3 gün boyunca girmezseniz bildirim göndermez",
            Importance = Importance.Default
        };
        AndroidNotificationCenter.RegisterNotificationChannel(_günlükBildirimKanal);

        _bildirim = new AndroidNotification()
        {
            Title = "Günaydın!",
            Text = "Epik Kapsül oynamak güzeldir.",
            SmallIcon = "kucuk_logo",
            LargeIcon = "buyuk_logo",
            FireTime = _bildirimZamanı
        };
    }

    private static int ZamanaGöreId(DateTime zaman)
    {
        var id = 0;
        id += zaman.Day * 24 * 60 * 60;
        id += zaman.Hour * 60 * 60;
        id += zaman.Minute * 60;
        id += zaman.Second;
        return id;
    }

    public void BildirimleriGönder()
    {
        if (!bildirimAçıkMı)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            return;
        }

        BildirimleriAyarla();

        for (var i = 0; i < 3; i++)
        {
            _bildirimZamanı = _bildirimZamanı.AddDays(1);
            var bildirimYapısı = bildirimler[Random.Range(0, bildirimler.Length)];
            try
            {
                _bildirim = bildirimYapısı.BildirimeÇevir();
            }
            catch (Exception)
            {
                _bildirim = BildirimYapısı.Varsayılan().BildirimeÇevir();
            }
            _bildirim.FireTime = _bildirimZamanı;
        
            var id = ZamanaGöreId(_bildirimZamanı);
            var status = AndroidNotificationCenter.CheckScheduledNotificationStatus(id);
        
            // Debug.Log($"{_bildirimZamanı}, {id}, {status:G}");
        
            if (status == NotificationStatus.Unknown)
                AndroidNotificationCenter.SendNotificationWithExplicitID(_bildirim, "default_channel", id);
        }
    }
}