using UnityEditor;
using UnityEngine;
// ReSharper disable InconsistentNaming

#if UNITY_EDITOR
[CustomEditor(typeof(BildirimYapısı))]
public class BildirimEditör : Editor
{
    private static string _çevrilecekString = "Günaydın!/Evet.";
    
    private SerializedProperty başlık;
    private SerializedProperty yazı;
    private SerializedProperty küçükResimID;
    private SerializedProperty büyükResimID;
    private SerializedProperty renk;

    private BildirimYapısı yapı;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        yapı = (BildirimYapısı)serializedObject.targetObject;

        başlık = serializedObject.FindProperty("başlık");
        yazı = serializedObject.FindProperty("yazı");
        küçükResimID = serializedObject.FindProperty("küçükResimID");
        büyükResimID = serializedObject.FindProperty("büyükResimID");
        renk = serializedObject.FindProperty("renk");
        
        EditorGUILayout.PropertyField(başlık);
        EditorGUILayout.PropertyField(yazı);
        EditorGUILayout.PropertyField(küçükResimID);
        EditorGUILayout.PropertyField(büyükResimID);
        EditorGUILayout.PropertyField(renk);

        GUILayout.Space(10);

        GUILayout.Label("Seçenekler");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Renk kullanma"))
        {
            Undo.RecordObject(yapı, "Bildirim yapısı renk değişimi");
            yapı.renk = new Color(0, 0, 0, 0);
        }

        if (GUILayout.Button("Varsayılan sarıyı kullan"))
        {
            Undo.RecordObject(yapı, "Bildirim yapısı renk değişimi");
            yapı.renk = BildirimYapısı.Sarı;
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        if (GUILayout.Button("Varsayılan ayarlara geri döndür"))
        {
            Undo.RecordObject(yapı, "Bildirim yapısı varsayılana çevrimi");
            yapı.VarsayılanaÇevir();
        }

        GUILayout.Space(10);

        _çevrilecekString = GUILayout.TextField(_çevrilecekString);

        if (GUILayout.Button("String'i bildirime çevir"))
        {
            Undo.RecordObject(yapı, "Bildirim yapısı string'den bildirime");
            var splitted = _çevrilecekString.Split('/');
            yapı.başlık = splitted[0];
            yapı.yazı = splitted[1];
        }

        //Güncelle();
        serializedObject.ApplyModifiedProperties();
        Güncelle();
    }

    private void Güncelle()
    {
        başlık.stringValue = yapı.başlık;
        yazı.stringValue = yapı.yazı;
        küçükResimID.stringValue = yapı.küçükResimID;
        büyükResimID.stringValue = yapı.büyükResimID;
        renk.colorValue = yapı.renk;
    }
}
#endif