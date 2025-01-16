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
        // Dokunmatik giriþ
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouch(touch);
        }
        // Fare giriþi (WebGL ve Editör için)
        else if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            HandleMouse();
        }
    }

    private void HandleTouch(Touch touch)
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (IsTouchingThisObject(touchPosition))
                {
                    startPosition = transform.position;
                    isDragging = true;
                }
                break;

            case TouchPhase.Moved:
                if (isDragging)
                {
                    transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);
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

    private void HandleMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        if (Input.GetMouseButtonDown(0))
        {
            if (IsTouchingThisObject(mousePosition))
            {
                startPosition = transform.position;
                isDragging = true;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            gameManager.OnWasteDropped(gameObject);
        }
    }

    private bool IsTouchingThisObject(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }
}
