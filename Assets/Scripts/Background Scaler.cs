using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{ 
    void Start()
    {
        // Kamera referans�
        Camera mainCamera = Camera.main;

        // Sprite boyutu
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // Ekran boyutlar�
        float screenHeight = mainCamera.orthographicSize * 2.0f;  
        float screenWidth = screenHeight * Screen.width / Screen.height;  

        // Sprite'� �l�ekle
        transform.localScale = new Vector3(
            screenWidth / spriteSize.x,
            screenHeight / spriteSize.y,
            1
        );
    }
}
