using System.Collections;
using UnityEngine;
using TMPro;

public class TextTyper : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; 
    public float typingSpeed = 0.05f;    

    
    public void StartTyping(string text)
    {
        StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        tutorialText.text = ""; 
        foreach (char letter in text.ToCharArray())
        {
            tutorialText.text += letter; 
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
