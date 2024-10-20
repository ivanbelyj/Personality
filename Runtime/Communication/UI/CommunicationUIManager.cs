using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationUIManager : MonoBehaviour
{
    [SerializeField]
    private MessagesPanel messagesPanel;
    public MessagesPanel MessagesPanel => messagesPanel;

    [SerializeField]
    private CommunicationPanel communicationPanel;
    public CommunicationPanel CommunicationPanel => communicationPanel;

    // [SerializeField]
    // private RectTransform panel;

    // [SerializeField]
    // private float animationSpeed = 1.0f;

    [SerializeField]
    private float transitionDuration = 0.6f;

    [SerializeField]
    private CanvasGroup[] canvasGroupsToHide;

    private Coroutine panelAnimationCoroutine;

    private bool IsVisible => canvasGroupsToHide[0].alpha > 0;

    private void Awake() {
        if (canvasGroupsToHide.Length == 0) {
            Debug.LogWarning(
                $"{nameof(canvasGroupsToHide)} is empty. Animation won't be applied"
            );
        }
    }

    public void ToggleUI()
    {
        if (panelAnimationCoroutine != null) {
            return;
        }

        if (canvasGroupsToHide.Length > 0) {
            panelAnimationCoroutine = StartCoroutine(AnimatePanel());
        }
    }

    // private IEnumerator AnimatePanel()
    // {
    //     float startX = panel.anchoredPosition.x;
    //     float targetX = isVisible ? 0 : panel.sizeDelta.x;

    //     float startTime = Time.time;
    //     while (Time.time - startTime <= 1.0f / animationSpeed)
    //     {
    //         float t = (Time.time - startTime) * animationSpeed;
    //         panel.anchoredPosition = new Vector2(Mathf.Lerp(startX, targetX, t), 0);
    //         yield return null;
    //     }

    //     panel.anchoredPosition = new Vector2(targetX, 0);
    // }

    private IEnumerator AnimatePanel() {
        float startAlpha = canvasGroupsToHide[0].alpha;
        float targetAlpha = IsVisible ? 0f : 1f;
        yield return StartCoroutine(SmoothTransition(
            startAlpha,
            targetAlpha,
            transitionDuration,
            (newValue) => {
                foreach (var canvasGroup in canvasGroupsToHide) {
                    canvasGroup.alpha = newValue;
                }
            }));

        OnEndAnimation();
    }

    private void OnEndAnimation() {
        panelAnimationCoroutine = null;

        foreach (var canvasGroup in canvasGroupsToHide) {
            canvasGroup.interactable = IsVisible;
        }
    }

    // Todo: use from common package ?
    private static IEnumerator SmoothTransition(
        float startValue,
        float endValue,
        float duration,
        Action<float> applyNewValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = t * t * (3f - 2f * t);

            float newValue = Mathf.Lerp(startValue, endValue, t);
            applyNewValue(newValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        applyNewValue(endValue);
    }
}
