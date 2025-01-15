using System.Collections;
using UnityEngine;
using TMPro;

public class TextTyper : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // TextMeshPro bileþeni
    public float typingSpeed = 0.05f;    // Harf yazma hýzý

    private void Start()
    {
        // Örnek bir metni animasyonlu olarak yazdýr
        StartTyping("Hoþ geldiniz! Atýklarýn ve atýk kutularýnýn türlerini öðrenin.");
    }

    public void StartTyping(string text)
    {
        StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        tutorialText.text = ""; // Metni temizle
        foreach (char letter in text.ToCharArray())
        {
            tutorialText.text += letter; // Harf harf ekle
            yield return new WaitForSeconds(typingSpeed); // Bekle
        }
    }
}
