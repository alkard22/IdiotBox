using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryTileController : MonoBehaviour
{
    public ShowDetailsView detailsView;
    public GameObject showTilePrefab;
    private List<GameObject> library = new List<GameObject>();

    private void OnEnable()
    {
        List<Show> shows = GameState.current.availShows;
        UpdateAvailableShows(shows);
    }

    private void OnDisable()
    {
        for(int i = 0; i < library.Count; i++) {
            Destroy(library[i]);
        }
        library.Clear();
    }

    private void UpdateAvailableShows(List<Show> shows)
    {
        float tileHeight = showTilePrefab.GetComponent<RectTransform>().sizeDelta.y;
        float spacing = this.GetComponent<VerticalLayoutGroup>().spacing;

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (tileHeight+spacing) * (shows.Count + 1));

        foreach(Show s in shows) {
            GameObject tile = Instantiate(showTilePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            tile.GetComponent<ShowTileView>().SetShowData(s);
            tile.transform.parent = this.transform;
            library.Add(tile);
        }
        SetTileAsActive(library[0]);
    }

    public void SetTileAsActive(GameObject tile)
    {
        foreach(GameObject obj in library) {
            Color c = obj.GetComponent<Image>().color;
            if(obj.GetInstanceID() == tile.GetInstanceID()) {
                c.a = 0;
            } else {
                c.a = 255;
            }
            obj.GetComponent<Image>().color = c;
        }
        detailsView.UpdateShowDetailsPanel(tile.GetComponent<ShowTileView>().GetShowData());
    }

}
