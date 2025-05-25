using UnityEngine;
using UnityEngine.UI;

public class FigurineView : MonoBehaviour
{
    public SpriteRenderer borderRenderer;
    public SpriteRenderer animaRenderer;

    /// <summary>
    /// Применяет данные 
    /// </summary>
    public void ApplyData(FigurineData data, GameObject[] shapePrefabs, Color[] colors, Sprite[] sprites)
    {        
        var shapeObj = Instantiate(shapePrefabs[(int)data.shape],transform);
        var borderObj = Instantiate(shapePrefabs[(int)data.shape],transform);
        shapeObj.transform.localScale *= 0.89f;
        shapeObj.GetComponent<SpriteRenderer>().color = colors[(int)data.color];
        var borderRenderer = borderObj.GetComponent<SpriteRenderer>();
        var shapeRenderer = shapeObj.GetComponent<SpriteRenderer>();
        shapeRenderer.color = colors[(int)data.color];
        borderRenderer.sortingOrder = 0;
        borderRenderer.color = Color.white;
        var iconSprite = animaRenderer.sprite;
        iconSprite = sprites[(int)data.animal];
        animaRenderer.sprite = iconSprite;
    }
}
