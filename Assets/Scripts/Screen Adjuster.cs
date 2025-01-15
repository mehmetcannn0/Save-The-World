using UnityEngine;

public class ScreenAdjuster : MonoBehaviour
{
    public Transform conveyorBelt;
    public Transform spawnPoint;   
    public Transform mixedWasteBin;
    public Transform[] recyclingBins; 

    void Awake()
    {
        AdjustConveyorBelt();
        AdjustSpawnAndMixedBin();
        ArrangeRecyclingBins();
    }

    void AdjustConveyorBelt()
    {
       
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

       
        SpriteRenderer beltSprite = conveyorBelt.GetComponent<SpriteRenderer>();

        if (beltSprite != null)
        {
            float beltWidth = beltSprite.bounds.size.x;
            float targetWidth = screenBounds.x * 2.2f; 
            float scaleFactor = targetWidth / beltWidth;
            conveyorBelt.localScale = new Vector3(scaleFactor, conveyorBelt.localScale.y, conveyorBelt.localScale.z);
            float yOffset = screenBounds.y * 0.75f;
            conveyorBelt.position = new Vector3(0, -screenBounds.y + yOffset, 0);
        }
      
    }

    void AdjustSpawnAndMixedBin()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        spawnPoint.position = new Vector3(-screenBounds.x + 0.5f, conveyorBelt.position.y, -2);
        mixedWasteBin.position = new Vector3(screenBounds.x - 0.5f, conveyorBelt.position.y, -2);
    }

    void ArrangeRecyclingBins()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        float yOffset = screenBounds.y * 1.4f; 
        float spacing = screenBounds.x * 2 / recyclingBins.Length; 

        for (int i = 0; i < recyclingBins.Length; i++)
        {
            float xPosition = -screenBounds.x + spacing * (i + 0.5f); 
            recyclingBins[i].position = new Vector3(xPosition, -screenBounds.y + yOffset, -2);
        }
    }
}
