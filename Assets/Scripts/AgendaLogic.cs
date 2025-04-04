using TMPro;
using UnityEngine;

public class AgendaLogic : MonoBehaviour
{
    public TMP_InputField content;
    public TMP_InputField datum;
    public TMP_InputField tijd;
     
    public async void SendData()
    {
        string finalContent = $"Afspraak:{content.text} op {datum.text} - {tijd.text}";
        await APIClient.Instance.SendAgendaInfo(finalContent);
        Debug.Log(finalContent);
    }

    
}
