using TMPro;
using UnityEngine;

public class TimeLineAgenda : MonoBehaviour
{
    public TextMeshProUGUI tmp_text;
    private int counter;

    public async void getData()
    {
        var items = await APIClient.Instance.GetAgendaItems();

        if (items == null || items.Count == 0)
        {
            tmp_text.text = "Geen agenda items gevonden.";
            return;
        }

        string allContent = "";

        foreach (var item in items)
        {
            allContent += $"- {item.content}\n";
            counter++;

            if(counter > 3)
            {
                break;
            }
        }

        tmp_text.text = allContent;
    }






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
