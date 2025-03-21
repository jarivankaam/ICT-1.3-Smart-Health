using DG.Tweening;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public GameObject menuButton;
    public Transform AnimateMenu;
    Vector2 menuOnScreen = new Vector2((float)719.5, (float)0.49);
    Vector2 menuOffScreen = new Vector2(1203, (float)0.49);

    public void MenuAppear()
    {
        AnimateMenu.DOLocalMove(menuOnScreen, 1);
        menuButton.SetActive(false);
    }

    public void MenuDisappear()
    {
        AnimateMenu.DOLocalMove(menuOffScreen, 1);
        menuButton.SetActive(true);
    }


    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

}
