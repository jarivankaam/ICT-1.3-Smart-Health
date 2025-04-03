using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineAnimation : MonoBehaviour
{
    public Transform animatorApeA;
    public Transform animatorApeB;
    public Vector3 ControleA;
    public Vector3 BijDeArtsA;
    public Vector3 GipsA;
    public Vector3 MedicatieA;
    public Vector3 NazorgA;
    public Vector3 ControleBijDeArtsA;
    public Vector3 ControleB;
    public Vector3 BijDeArtsB;
    public Vector3 Operatie;
    public Vector3 GipsB;
    public Vector3 MedicatieB;
    public Vector3 NazorgB;
    public Vector3 ControleBijDeArtsB;

    public void Start()
    {
        var isRouteB = APIClient.Instance.User.TimeLineRoute;
        var currentStep = APIClient.Instance.User.CurrentStep;

        Debug.Log(currentStep);

        if (currentStep is 0 or null)
        {
            currentStep = 0;
        }

        if (isRouteB)
        {
            switch (currentStep)
            {
                case 1:
                    ChangeApePositionToControleB();
                    break;
                case 2:
                    ChangeApePositionToBijDeArtsB();
                    break;
                case 3:
                    ChangeApePositionToOperatie();
                    break;
                case 4:
                    ChangeApePositionToGipsB();
                    break;
                case 5:
                    ChangeApePositionToMedicatieB();
                    break;
                case 6:
                    ChangeApePositionToNazorgB();
                    break;
                case 7:
                    ChangeApePositionToControleBijDeArtsB();
                    break;
                default:
                    ChangeApePositionToControleB();
                    break;
            }
        }
        else
        {
            switch (currentStep)
            {
                case 1:
                    ChangeApePositionToControleA();
                    break;
                case 2:
                    ChangeApePositionToBijDeArtsA();
                    break;
                case 3:
                    ChangeApePositionToGipsA();
                    break;
                case 4:
                    ChangeApePositionToMedicatieA();
                    break;
                case 5:
                    ChangeApePositionToNazorgA();
                    break;
                case 6:
                    ChangeApePositionToControleBijDeArtsA();
                    break;
                default:
                    ChangeApePositionToControleA();
                    break;
            }
        }
    }

    public void ChangeApePositionToControleA()
    {
        animatorApeA.DOMove(ControleA, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToBijDeArtsA()
    {
        animatorApeA.DOMove(BijDeArtsA, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToGipsA()
    {
        animatorApeA.DOMove(GipsA, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToMedicatieA()
    {
        animatorApeA.DOMove(MedicatieA, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToNazorgA()
    {
        animatorApeA.DOMove(NazorgA, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToControleBijDeArtsA()
    {
        animatorApeA.DOMove(ControleBijDeArtsA, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToControleB()
    {
        animatorApeB.DOMove(ControleB, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToBijDeArtsB()
    {
        animatorApeB.DOMove(BijDeArtsB, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToOperatie()
    {
        animatorApeB.DOMove(Operatie, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToGipsB()
    {
        animatorApeB.DOMove(GipsB, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToMedicatieB()
    {
        animatorApeB.DOMove(MedicatieB, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToNazorgB()
    {
        animatorApeB.DOMove(NazorgB, 2)
           .SetEase(Ease.InOutSine);
    }
    public void ChangeApePositionToControleBijDeArtsB()
    {
        animatorApeB.DOMove(ControleBijDeArtsB, 2)
           .SetEase(Ease.InOutSine);
    }
}

