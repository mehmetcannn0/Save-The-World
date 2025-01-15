using System.Collections;
using UnityEngine;
using TMPro;

public class TextTyper : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // TextMeshPro bile�eni
    public float typingSpeed = 0.05f;    // Harf yazma h�z�

    private void Start()
    {
        // �rnek bir metni animasyonlu olarak yazd�r
        StartTyping("Ho� geldiniz! At�klar�n ve at�k kutular�n�n t�rlerini ��renin.");
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
