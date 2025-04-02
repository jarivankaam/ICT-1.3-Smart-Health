using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class BananaDropper : MonoBehaviour
{
    public GameObject bananaPrefab;  // Assign a UI-based banana prefab
    public RectTransform canvasRect; // Assign the Canvas (must have RectTransform)
    public RectTransform[] treePositions; // Assign UI positions where bananas can drop
    public float dropDuration = 1.2f; // Time to fall
    public float minInterval = 0.5f, maxInterval = 2f; // Random drop interval
    public float spawnOffsetY = 80f; // How high above trees bananas start

    private void Start()
    {
        StartCoroutine(SpawnBananas());
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

        // Pick a random tree position
        RectTransform tree = treePositions[Random.Range(0, treePositions.Length)];

        // Instantiate banana inside the Canvas
        GameObject banana = Instantiate(bananaPrefab, canvasRect);
        RectTransform bananaRect = banana.GetComponent<RectTransform>();

        // Spawn above the tree, instead of at the canvas top
        float dropY = tree.anchoredPosition.y + spawnOffsetY;
        bananaRect.anchoredPosition = new Vector2(tree.anchoredPosition.x, dropY);

        // Animate falling with bounce effect
        float groundY = -canvasRect.rect.height / 2; // Bottom of the canvas
        bananaRect.DOAnchorPosY(groundY + 50, dropDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                // Bounce effect when landing
                bananaRect.DOAnchorPosY(groundY, 0.2f).SetLoops(2, LoopType.Yoyo)
                    .OnComplete(() => Destroy(banana, 1f)); // Destroy after landing
            });

        // Rotate while falling
        bananaRect.DORotate(Vector3.forward * 360, dropDuration, RotateMode.FastBeyond360);
    }
}