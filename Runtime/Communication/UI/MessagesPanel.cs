using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Panel displaying different game messages (sounds including dialogs first of all)
/// </summary>
public class MessagesPanel : MonoBehaviour
{
    [SerializeField]
    private VerticalLayoutGroup messageItemsParent;

    // Todo: normal adding message items
    [SerializeField]
    private GameObject textPrefab;

    [SerializeField]
    private ScrollRect scrollRect;

    public void AddMessage(string messageText) {
        var go = Instantiate(textPrefab);
        go.GetComponent<TextMeshProUGUI>().text = messageText;
        go.transform.SetParent(messageItemsParent.transform);
    }

    public void ScrollToBottom() {
        Canvas.ForceUpdateCanvases();

        messageItemsParent.CalculateLayoutInputVertical() ;
        messageItemsParent.GetComponent<ContentSizeFitter>().SetLayoutVertical() ;

        scrollRect.content.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical() ;
        scrollRect.content.GetComponent<ContentSizeFitter>().SetLayoutVertical() ;

        scrollRect.verticalNormalizedPosition = 0 ;
    }
}
