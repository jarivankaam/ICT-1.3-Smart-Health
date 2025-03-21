using UnityEngine;

public class ExplanationText : MonoBehaviour
{
    bool textCloudPillIsActive = false;
    bool textCloudSyringeIsActive = false;
    bool textCloudOintmentIsActive = false;
    public GameObject textCloudPill;
    public GameObject textCloudSyringe;
    public GameObject textCloudOintment;
    public void CloseTextCloudPill()
    {
        Debug.Log("Pill werkt");
        if (!textCloudPillIsActive)
        {
            textCloudPill.SetActive(true);
            textCloudPillIsActive = true;
        }
        else
        {
            textCloudPill.SetActive(false);
            textCloudPillIsActive = false;
        }
    }
    public void CloseTextCloudSyringe()
    {
        Debug.Log("Syringe werkt");
        if (!textCloudSyringeIsActive)
        {
            textCloudSyringe.SetActive(true);
            textCloudSyringeIsActive = true;
        }
        else
        {
            textCloudSyringe.SetActive(false);
            textCloudSyringeIsActive = false;
        }
    }
    public void CloseTextCloudOintment()
    {
        Debug.Log("Ointment werkt");
        if (!textCloudOintmentIsActive)
        {
            textCloudOintment.SetActive(true);
            textCloudOintmentIsActive = true;
        }
        else
        {
            textCloudOintment.SetActive(false);
            textCloudOintmentIsActive = false;
        }
    }
}