using UnityEngine;
using Zenject;

public class TapHandler : MonoBehaviour
{
    private ActionBar _actionBar;
    public LayerMask figurineLayer;
    private Camera gameCamera;

    [Inject]
    public void Construct(ActionBar actionBar)
    {
        _actionBar = actionBar;
    }

    private void Awake()
    {
        gameCamera = Camera.main;
    }

    private void Update()
    {
        // Тап мобилка
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            HandleTap(Input.touches[0].position);
        }
        // Клик мышкой 
        if (Input.GetMouseButtonDown(0))
        {
            HandleTap(Input.mousePosition);
        }
    }
    
    private void HandleTap(Vector2 screenPosition)
    {
        Vector2 worldPos = gameCamera.ScreenToWorldPoint(screenPosition);        
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, figurineLayer);
        if (hit.collider != null)
        {
            Figurine figurine = hit.collider.GetComponentInParent<Figurine>();
            if (figurine != null)
            {        
                if (_actionBar.IsEnoughSlots())
                {
                    figurine.MakeInactive(); 

                    _actionBar.AttractFigurine(figurine);
                } 

            }
        }
    }
}
