using UnityEngine;
  
public class ExplanationText : MonoBehaviour
{
    //TimelineItemChoice
    bool InfoBoxIsActive = false;

   //TimelineItemChoice
    public void ShowInfoBox(GameObject infoBox)
    {
        if (!InfoBoxIsActive)
        {
            infoBox.SetActive(true);
            InfoBoxIsActive = true;
        }
        else
        {
            infoBox.SetActive(false);
            InfoBoxIsActive = false;
        }
    }
}