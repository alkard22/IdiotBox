using UnityEngine;
using UnityEngine.UI;

public class ShowTileView : MonoBehaviour
{
    public ShowAvatar avatar;
    public Text titleField;

    private Show show;

    public void SetShowData(Show s)
    {
        if(s != null) {
            titleField.text = s.concept.name;
            avatar.title = s.concept.name;
            show = s;
        }
    }

    public Show GetShowData()
    {
        return show;
    }

    public void TileSelected()
    {
        LibraryTileController controller = this.transform.GetComponentInParent<LibraryTileController>();
        if(controller != null) {
            controller.SetTileAsActive(this.gameObject);
        }
    }
}
