using UnityEngine;
using UnityEngine.UI;

public class ShowTileView : MonoBehaviour
{
    public Image image;
    public Text titleField;

    public void UpdateTileDetails (Sprite img, string title)
    {
        if(img != null)
            image.sprite = img;
        if(title != null)
            titleField.text = title;
    }

    public void TileSelected()
    {
        LibraryTileController controller = this.transform.GetComponentInParent<LibraryTileController>();
        if(controller != null) {
            controller.SetTileAsActive(this.gameObject);
        }
    }
}
