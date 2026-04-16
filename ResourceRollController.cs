using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class ResourceRollController : MonoBehaviour
{
    [Header("Visual Settings")]
    public Transform RollMesh;
    public float MaxScale = 2.0f;
    public float MinScale = 0.2f;
    public float LerpDuration = 0.3f;

    [Header("LOD Meshes")]
    public GameObject FullMesh;
    public GameObject MediumMesh;
    public GameObject LowMesh;
    public GameObject EmptyMesh;

    private VisualState currentState;

    public void UpdateVisual(int backendValue, VisualState newState)
    {
        // Плавное изменение размера
        float targetScale = Mathf.Lerp(MinScale, MaxScale, backendValue / 40f);
        StartCoroutine(SmoothScale(targetScale));

        // Смена LOD-меша
        if (newState != currentState)
        {
            SwapMesh(newState);
            currentState = newState;
        }
    }

    IEnumerator SmoothScale(float targetScale)
    {
        float startScale = RollMesh.localScale.y;
        float elapsed = 0f;

        while (elapsed < LerpDuration)
        {
            elapsed += Time.deltaTime;
            float newScale = Mathf.Lerp(startScale, targetScale, elapsed / LerpDuration);
            RollMesh.localScale = new Vector3(RollMesh.localScale.x, newScale, RollMesh.localScale.z);
            yield return null;
        }
    }

    void SwapMesh(VisualState state)
    {
        FullMesh.SetActive(false);
        MediumMesh.SetActive(false);
        LowMesh.SetActive(false);
        EmptyMesh.SetActive(false);

        switch (state)
        {
            case VisualState.Full: FullMesh.SetActive(true); break;
            case VisualState.Medium: MediumMesh.SetActive(true); break;
            case VisualState.Low: LowMesh.SetActive(true); break;
            case VisualState.Empty: EmptyMesh.SetActive(true); break;
        }
    }
}

