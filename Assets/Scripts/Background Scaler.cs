using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{ 
    void Start()
    {
        // Kamera referansý
        Camera mainCamera = Camera.main;

        // Sprite boyutu
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // Ekran boyutlarý
        float screenHeight = mainCamera.orthographicSize * 2.0f;  
        float screenWidth = screenHeight * Screen.width / Screen.height;  

        // Sprite'ý ölçekle
        transform.localScale = new Vector3(
            screenWidth / spriteSize.x,
            screenHeight / spriteSize.y,
            1
        );
    }
}
