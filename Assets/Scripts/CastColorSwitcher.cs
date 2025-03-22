using UnityEngine;
using UnityEngine.UI;

public class CastColorSwitcher : MonoBehaviour
{
    public Image image;
    public GameObject colorBlue;
    public GameObject colorBlack;
    public GameObject colorPink;
    public GameObject colorWhite;
    public GameObject colorYellow;
    int colorNumber = 1;
    public void CheckColor()
    {
        if(colorNumber == 1)
        {
            ShowBlue();
        }
        else if (colorNumber == 2)
        {
            ShowBlack();
        }
        else if (colorNumber == 3)
        {
            ShowPink();
            image.color = Color.white;
        }
        else if (colorNumber == 4)
        {
            ShowWhite();
            image.color = Color.black;
        }
        else if (colorNumber == 5)
        {
            ShowYellow();
            image.color = Color.white;
        }
    }
    public void NextColor()
    {
        colorNumber += 1;
        if (colorNumber == 6)
        {
            colorNumber = 1;
        }
        CheckColor();
    }
    public void PreviousColor()
    {
        colorNumber -= 1;
        if (colorNumber == 0)
        {
            colorNumber = 5;
        }
        CheckColor();
    }
    public void ShowBlue()
    {
        colorBlue.SetActive(true);
        colorBlack.SetActive(false);
        colorPink.SetActive(false);
        colorWhite.SetActive(false);
        colorYellow.SetActive(false);
        colorNumber = 1;
        image.color = Color.white;
    }
    public void ShowBlack()
    {
        colorBlue.SetActive(false);
        colorBlack.SetActive(true);
        colorPink.SetActive(false);
        colorWhite.SetActive(false);
        colorYellow.SetActive(false);
        colorNumber = 2;
        image.color = Color.white;
    }
    public void ShowPink()
    {
        colorBlue.SetActive(false);
        colorBlack.SetActive(false);
        colorPink.SetActive(true);
        colorWhite.SetActive(false);
        colorYellow.SetActive(false);
        colorNumber = 3;
        image.color = Color.white;
    }
    public void ShowWhite()
    {
        
        colorBlue.SetActive(false);
        colorBlack.SetActive(false);
        colorPink.SetActive(false);
        colorWhite.SetActive(true);
        colorYellow.SetActive(false);
        colorNumber = 4;
        image.color = Color.black;
    }
    public void ShowYellow()
    {
        colorBlue.SetActive(false);
        colorBlack.SetActive(false);
        colorPink.SetActive(false);
        colorWhite.SetActive(false);
        colorYellow.SetActive(true);
        colorNumber = 5;
        image.color = Color.white;
    
    }

}
