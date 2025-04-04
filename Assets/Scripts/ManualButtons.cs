using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualButtons : MonoBehaviour
{
    public string StepName;

    public void SwitchContent()
    {
        SceneManager.LoadScene(StepName);
    }
}
