using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class BananaDropper : MonoBehaviour
{
    public GameObject bananaPrefab;  // UI-based banana prefab
    public RectTransform canvasRect; // Canvas (must have RectTransform)
    public RectTransform[] treePositions; // UI positions where bananas can drop
    public float dropDuration = 1.2f; // Time to fall
    public float minInterval = 0.5f, maxInterval = 2f; // Random drop interval
    public float spawnOffsetY = 100f; // Height above trees for banana spawn

    public Image monkeyImage; // ?? Existing UI Image in the scene
    private bool isMonkeyRunning = false;

    public Button dropButton; // Assign UI button to drop the monkey

    private void Start()
    {
        StartCoroutine(SpawnBananas());
        dropButton.onClick.AddListener(DropMonkey); // Button triggers the drop
    }

    IEnumerator SpawnBananas()
    {
        while (true)
        {
            SpawnBanana();
            float randomDelay = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    void SpawnBanana()
    {
        if (treePositions.Length == 0) return;

        RectTransform tree = treePositions[Random.Range(0, treePositions.Length)];

        GameObject banana = Instantiate(bananaPrefab, canvasRect);
        RectTransform bananaRect = banana.GetComponent<RectTransform>();

        float dropY = tree.anchoredPosition.y + spawnOffsetY;
        bananaRect.anchoredPosition = new Vector2(tree.anchoredPosition.x, dropY);

        float groundY = -canvasRect.rect.height / 2;
        bananaRect.DOAnchorPosY(groundY + 50, dropDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                bananaRect.DOAnchorPosY(groundY, 0.2f).SetLoops(2, LoopType.Yoyo)
                    .OnComplete(() => Destroy(banana, 1f));
            });

        bananaRect.DORotate(Vector3.forward * 360, dropDuration, RotateMode.FastBeyond360);
    }

    void DropMonkey()
    {
        if (isMonkeyRunning) return; // Prevent multiple drops

        RectTransform monkeyRect = monkeyImage.GetComponent<RectTransform>();

        // Set the monkey's start position (lower than top)
        float startY = canvasRect.rect.height / 4; // Adjust as needed
        monkeyRect.anchoredPosition = new Vector2(0, startY);

        // Drop the monkey to the bottom
        float groundY = -canvasRect.rect.height / 2 + 70;

        monkeyRect.DOAnchorPosY(groundY, 1f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => StartMonkeyMovement(monkeyRect, groundY));
    }

    void StartMonkeyMovement(RectTransform monkeyRect, float groundY)
    {
        if (isMonkeyRunning) return;
        isMonkeyRunning = true;
        StartCoroutine(MonkeyRunAround(monkeyRect, groundY));
    }

    IEnumerator MonkeyRunAround(RectTransform monkeyRect, float groundY)
    {
        while (isMonkeyRunning)
        {
            float randomX = Random.Range(-canvasRect.rect.width / 2 + 50, canvasRect.rect.width / 2 - 50);

            // Move monkey left and right at the bottom
            monkeyRect.DOAnchorPos(new Vector2(randomX, groundY), 1f)
                .SetEase(Ease.Linear);

            yield return new WaitForSeconds(1f);
        }
    }
}