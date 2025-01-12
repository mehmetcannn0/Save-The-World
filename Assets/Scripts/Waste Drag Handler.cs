using UnityEngine;

public class WasteDragHandler : MonoBehaviour
{
    public RecyclingGame gameManager;
    private Vector3 startPosition;
    private bool isDragging = false;

    private void Start()
    {
        gameManager = FindObjectOfType<RecyclingGame>();
    }

    void Update()
    { 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (IsTouchingThisObject(touch))
                    {
                        startPosition = transform.position;
                        isDragging = true;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        transform.position = new Vector3(touchPosition.x, touchPosition.y, 0);
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        isDragging = false;
                        gameManager.OnWasteDropped(gameObject);
                    }
                    break;
            }
        }
     
    }
 
  
    private bool IsTouchingThisObject(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
     
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    
}
