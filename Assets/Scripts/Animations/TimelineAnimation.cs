using DG.Tweening;
using UnityEngine;

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
        bool isRouteB = APIClient.Instance.User.TimeLineRoute;
        int currentStep = Mathf.Clamp(APIClient.Instance.User.CurrentStep ?? 0, 0, int.MaxValue);

        Vector3 targetPosition;

        if (isRouteB)
        {
            targetPosition = GetStepPosition(currentStep, ControleB, BijDeArtsB, Operatie, GipsB, MedicatieB, NazorgB, ControleBijDeArtsB);
            MoveApe(animatorApeB, targetPosition);
        }
        else
        {
            targetPosition = GetStepPosition(currentStep, ControleA, BijDeArtsA, GipsA, MedicatieA, NazorgA, ControleBijDeArtsA);
            MoveApe(animatorApeA, targetPosition);
        }
    }

    private Vector3 GetStepPosition(int step, params Vector3[] steps)
    {
        return steps[Mathf.Clamp(step - 1, 0, steps.Length - 1)];
    }

    public void MoveApe(Transform ape, Vector3 targetPosition)
    {
        if (ape == null)
        {
            Debug.LogError("AnimatorApe niet gevonden!");
            return;
        }

        ape.DOMove(targetPosition, 2).SetEase(Ease.InOutSine);
    }
}

