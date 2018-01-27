using UnityEngine;
using UnityEngine.UI;

public class ShowTileView : MonoBehaviour
{
    public ShowAvatar avatar;
    public Text titleField;

    public void UpdateTileDetails (string title)
    {
        if(title != null) {
            titleField.text = title;
            avatar.title = title;
        }
    }

    public void TileSelected()
    {
        LibraryTileController controller = this.transform.GetComponentInParent<LibraryTileController>();
        if(controller != null) {
            controller.SetTileAsActive(this.gameObject);
        }
    }
}
