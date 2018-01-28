using UnityEngine;
using UnityEngine.UI;

public class AdTileView : MonoBehaviour
{
    public ShowAvatar avatar;
    public Text adName;
    public Text primary;
    public Text secondary;
    public Text runDuration;
    private Ad ad;

    public void UpdateTileDetails (Ad ad)
    {
        if(ad.name != null) {
            adName.text = ad.name;
            avatar.title = ad.name;
            runDuration.text = "" + ad.duration;
            DemographicTarget target = ad.primary;
            primary.text = "$" + target.revenue +" per view for " + target.target.name;
            secondary.text = "$" + ad.revenueOther + " per view for other demographics";
            this.ad = ad;
        }
    }

    public Ad GetAdData()
    {
        return ad;
    }

    public void TileSelected()
    {
        AdsTileController controller = this.transform.GetComponentInParent<AdsTileController>();
        if(controller != null) {
            controller.SetTileAsActive(this.gameObject);
        }
    }
}
