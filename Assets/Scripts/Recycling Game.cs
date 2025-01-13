 
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

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
    private int NextLevelPoint = 100;

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
            if (Vector3.Distance(waste.transform.position, bin.gameObject.transform.position) < 1f)
            {
                if (waste.gameObject.tag == bin.gameObject.tag)
                {
                    Debug.Log("Doðru kategoriye atýldý: " + bin.gameObject.tag);
                    score += 10;  
                    UpdateScore();
                    Destroy(waste);
                    activeWastes.Remove(waste);
                }
                else
                {
                    Debug.Log("Yanlýþ kategoriye atýldý: " + bin.gameObject.tag);
                    score -= 10;  
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

        if(score == NextLevelPoint)
        {
            Debug.Log("Next Level");
            NextLevel();
            LevelText.text = "Level: " + NextLevelPoint / 100 ;
        }else if (score == NextLevelPoint - 100 && score != 0)
        {
            Debug.Log("Previous Level");
            PreviousLevel();
            LevelText.text = "Level: " + NextLevelPoint / 100;  
        }
    }
}
