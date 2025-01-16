using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> wasteObjects; // At�k prefableri
    public List<Transform> targetPositions; // Hedef pozisyonlar (kutular�n konumu)
    public float animationDuration = 1.5f; // Animasyon s�resi
    public Transform conveyorBelt;
    public GameObject nextButtonUI;
    public GameObject closeButtonUI;
    public GameObject InfoUI;





    void WastePositionAdjuster()
    {
        for (int i = 0; i < wasteObjects.Count; i++)
        {
            wasteObjects[i].transform.position = new Vector3(wasteObjects[i].transform.position.x, conveyorBelt.position.y+2 , -2);

        }
    }
    private void Start()
    {
        WastePositionAdjuster();
    }

    private IEnumerator PlayTutorialSequence()
    {
        for (int i = 0; i < wasteObjects.Count; i++)
        {
            // At��� hedef pozisyona hareket ettir
            wasteObjects[i].transform.DOMove(new Vector3(targetPositions[i].position.x, targetPositions[i].position.y, wasteObjects[i].transform.position.z), animationDuration)
                .SetEase(Ease.InOutQuad);

            // Animasyon tamamlanana kadar bekle
            yield return new WaitForSeconds(animationDuration + 0.5f);
        }

        // Tutorial tamamland�, mesaj g�ster veya ba�ka bir i�lem yap
        WastePositionAdjuster();
        StartCoroutine(PlayTutorialSequence());
        Debug.Log("Tutorial tamamland�!");
    }
    public void NextStep()
    {
        closeButtonUI.SetActive(true);
        nextButtonUI.SetActive(false);
        InfoUI.SetActive(false);
        StartCoroutine(PlayTutorialSequence());
        TextTyper textTyper = FindObjectOfType<TextTyper>();
        textTyper.StartTyping("Ho� geldiniz! At�klar�n ve at�k kutular�n�n t�rlerini ��renin.");
    }
    public void CloseTutorial()
    {
        StopAllCoroutines();
        DOTween.KillAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }
   
}
