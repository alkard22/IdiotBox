using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsTileController : MonoBehaviour
{
    public GameObject showTilePrefab;
    private List<GameObject> library = new List<GameObject>();

    private void OnEnable()
    {
        List<Ad> ads = GameState.current.availAds;
        UpdateAvailableAds(ads);
    }

    public void UpdateAvailableAds(List<Ad> ads)
    {
        float tileHeight = showTilePrefab.GetComponent<RectTransform>().sizeDelta.y;
        float spacing = this.GetComponent<VerticalLayoutGroup>().spacing;

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (tileHeight + spacing) * (ads.Count + 1));
        library.Clear();

        foreach (Ad a in ads)
        {
            GameObject tile = Instantiate(showTilePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            tile.GetComponent<AdTileView>().UpdateTileDetails(a);
            tile.transform.parent = this.transform;
            library.Add(tile);
        }
        SetTileAsActive(library[0]);
    }

    public void SetTileAsActive(GameObject tile)
    {
        foreach (GameObject obj in library)
        {
            Color c = obj.GetComponent<Image>().color;
            if (obj.GetInstanceID() == tile.GetInstanceID())
            {
                c.a = 0;
            }
            else
            {
                c.a = 255;
            }
            obj.GetComponent<Image>().color = c;
        }
    }

}