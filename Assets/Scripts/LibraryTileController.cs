using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryTileController : MonoBehaviour
{
    public Color normalColor;
    public Color selectColor;
    public GameObject showTilePrefab;

    private List<GameObject> library = new List<GameObject>();
    private float tileHeight = 60;

    public void UpdateAvailableShows(List<GameObject> shows)
    {
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, tileHeight * shows.Count);
        library.Clear();

        foreach(GameObject obj in shows) {
            GameObject tile = Instantiate(showTilePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            tile.GetComponent<ShowTileView>().UpdateTileDetails(null, "Show Title Here"); // TODO: add the objects details
            tile.transform.parent = this.transform;
            library.Add(tile);
        }
        SetTileAsActive(library[0]);
    }

    public void SetTileAsActive(GameObject tile)
    {
        foreach(GameObject obj in library) {
            if(obj.GetInstanceID() == tile.GetInstanceID()) {
                obj.GetComponent<Image>().color = selectColor;
            } else {
                obj.GetComponent<Image>().color = normalColor;
            }
        }
    }

}
