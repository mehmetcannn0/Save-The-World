 
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class RecyclingGame : MonoBehaviour
{
  

    public List<GameObject> recyclingBins;  
    public GameObject[] wastePrefabs;  
    public Transform conveyorBelt; 
    
    public float conveyorSpeed = 1f;  
    public float spawnRepeateRate = 4f;

    public Transform spawnPoint;
    public TextMeshProUGUI scoreText;  
    public TextMeshProUGUI LevelText;  
    public int score = 0;  

    public GameObject mixedTrashBin;

    private List<GameObject> activeWastes = new List<GameObject>();
    public int NextLevelPoint = 100;
    public int level = 1;
    public int health = 3;
    public GameObject[] heartsUI;
    public Sprite[] hearts;


    public GameObject resultEffectUI;
    public GameObject pauseScreenUI;
    public GameObject gameOverScreenUI;
    void Start()
    { 
        InvokeRepeating("SpawnWaste", 1f, spawnRepeateRate);  
        UpdateScore();
    }

    void Update()
    { 
        MoveWastes();
    }
    void PreviousLevel()
    {
        NextLevelPoint -= 100;
        conveyorSpeed /= 1.5f;
        spawnRepeateRate += 0.5f;
        CancelInvoke();
        InvokeRepeating("SpawnWaste", 1f, spawnRepeateRate);
    }
    void NextLevel()
    {
        NextLevelPoint += 100;
        conveyorSpeed *= 1.5f;
        spawnRepeateRate -= 0.5f;
        if (spawnRepeateRate < 1f)
        {
            spawnRepeateRate = 1f;
        }
        CancelInvoke();
        InvokeRepeating("SpawnWaste", 1f, spawnRepeateRate);
    }

    void SpawnWaste()
    { 
        GameObject waste = Instantiate(wastePrefabs[Random.Range(0, wastePrefabs.Length)], spawnPoint.position, Quaternion.identity);

        
        activeWastes.Add(waste);
         
        var dragHandler = waste.AddComponent<WasteDragHandler>();
        if (waste.GetComponent<WasteDragHandler>() !=null)
        {
            Debug.Log(" null degýl");
        }
        else
        {
            Debug.Log(" null");
        }
        dragHandler.gameManager = this;
    }

    public void MoveWastes()
    {
        for (int i = activeWastes.Count - 1; i >= 0; i--)
        {
            GameObject waste = activeWastes[i];
             
            waste.transform.Translate(Vector3.right * conveyorSpeed * Time.deltaTime);
             
            if (waste.transform.localPosition.x > conveyorBelt.localScale.x / 2)
            { 
                float mixedBinX = mixedTrashBin.transform.position.x;
                 
                if (waste.transform.position.x > mixedBinX)
                {

                    Debug.Log("banttan cýktý " + waste.gameObject.tag);
                    score -= 10;
                    heartsUI[health].GetComponent<Image>().sprite= hearts[0];
                    health -= 1;

                    WrongBin();
                    UpdateScore();

                    Destroy(waste);
                    activeWastes.RemoveAt(i);
                }
            }
        }
    }

     
    public void OnWasteDropped(GameObject waste)
    {
        foreach (var bin in recyclingBins)
        { 
            if (Vector3.Distance(waste.transform.position, bin.gameObject.transform.position) < 3f)
            {
                if (waste.gameObject.tag == bin.gameObject.tag)
                {
                    Debug.Log("Doðru kategoriye atýldý: " + bin.gameObject.tag);
                    score += 10;  
                    CorretBin();
                    UpdateScore();
                    Destroy(waste);
                    activeWastes.Remove(waste);
                }
                else
                {
                    Debug.Log("Yanlýþ kategoriye atýldý: " + bin.gameObject.tag);
                    score -= 10;
                    heartsUI[health-1].GetComponent<Image>().sprite = hearts[0];

                    health -= 1;
                    WrongBin();
                    UpdateScore();
                    Destroy(waste);
                    activeWastes.Remove(waste);
                }
                return;
            }
        }

   
    }

   public void UpdateScore()
    {
        scoreText.text = "Puan: " + score;


        if (score == NextLevelPoint)
        {
            Debug.Log("Next Level");
            NextLevel();
        }else if (score == NextLevelPoint - 110 && score != 0)
        {
            Debug.Log("Previous Level");
            if (level>1)
            {
                PreviousLevel();
                
            }
        }

        level = NextLevelPoint / 100;

        LevelText.text = "Level: " + level;
        if (health == 0)
        {
            GameOver();
        }


    }
    void WrongBin()
    {
        resultEffectUI.SetActive(true);
        Image resultImage = resultEffectUI.GetComponent<Image>();
        resultImage.color = new Color(250, 0, 0, 0);
        Color targetColor = new Color(250,0,0,0.2f);
        resultImage.DOColor(targetColor, 1).OnComplete(() =>
        { 
            resultEffectUI.SetActive(false);
        });
    }

    void CorretBin()
    { 
        resultEffectUI.SetActive(true);
        Image resultImage = resultEffectUI.GetComponent<Image>();
        resultImage.color = new Color(0, 250, 0,0);
        Color targetColor = new Color(0, 250, 0, 0.2f);
        resultImage.DOColor(targetColor, 1).OnComplete(() =>
        {
            resultEffectUI.SetActive(false);
        });
    }
    public void PauseGame()
    {
        pauseScreenUI.SetActive(true);

        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        pauseScreenUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreenUI.SetActive(true);
        Debug.Log("Game Over");
    }

    public void BackToMenuScene() {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");

    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
