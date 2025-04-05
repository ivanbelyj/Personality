using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommunicationPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject choiceItemPrefab;

    [SerializeField]
    private Transform choiceItemsParent;

    /// <summary>
    /// UI items representing choices
    /// </summary>
    private List<ChoiceItem> choiceItems;

    // [SerializeField]
    // private TMP_Dropdown speechVolumeDropdown;

    [SerializeField]
    private TMP_Dropdown recipientDropdown;

    [SerializeField]
    private TMP_InputField messageInputField;

    private List<Guid> availableRecipientEntityIds;
    private Guid? selectedRecipientEntityId;

    private IChoicesProvider choicesProvider;
    private ICommunicationSubject subject;
    private ISpeaker speaker;
    private Action<string, Guid?> onMessageSubmitted;

    private void Awake() {
        messageInputField.onSubmit.AddListener(OnMessageSubmitted);
    }

    public void OnMessageSubmitted(string text) {
        messageInputField.text = "";
        onMessageSubmitted?.Invoke(text, selectedRecipientEntityId);
    }

    public void MakeChoiceByNumber(int number) {
        if (number - 1 < 0 || number - 1 >= choiceItems.Count) {
            return;
        }
        choiceItems[number - 1].MakeChoice();
    }

    public void Init(
        IChoicesProvider choicesProvider,
        ICommunicationSubject subject,
        ISpeaker speaker,
        Action<string, Guid?> onMessageSubmitted)
    {
        this.choicesProvider = choicesProvider;
        this.subject = subject;
        this.speaker = speaker;
        this.onMessageSubmitted = onMessageSubmitted;
    }

    public void OnRecipientSelected(int newIndex) {
        // Debug.Log($"Selected item {newIndex}. there are {availableRecipients.Count} recipients available");
        SetRecipientId(GetRecipientByDropdownIndex(newIndex));
    }

    public void OnSpeechVolumeSelected(int newIndex) {
        SpeechVolume volume = (SpeechVolume)newIndex;
        speaker.SetSpeechVolume(volume);
    }

    public void SetRecipientId(Guid? recipientEntityId) {
        selectedRecipientEntityId = recipientEntityId;
        UpdatePanel();
    }

    public void SetAvailableRecipientIds(List<Guid> recipientEntityIds) {
        recipientDropdown.ClearOptions();
        var options = new List<TMP_Dropdown.OptionData>() {
            new TMP_Dropdown.OptionData() {
                text = "Everybody"
            }
        };
        options.AddRange(recipientEntityIds
            .Where(x => x != subject.EntityId)
            .Select(recipientId => new TMP_Dropdown.OptionData() {
                text = $"{subject.GetKnownCharacterName(recipientId)}"
            }));
        recipientDropdown.AddOptions(options);

        this.availableRecipientEntityIds = recipientEntityIds;

        // If selected recipient is not actiual, change to "Everybody"
        if (GetRecipientDropdownIndex(selectedRecipientEntityId) == -1) {
            selectedRecipientEntityId = null;
        }

        UpdatePanel();
    }

    private Guid? GetRecipientByDropdownIndex(int index) {
        return index == 0
            ? null
            : availableRecipientEntityIds[index - 1];
    }

    private int GetRecipientDropdownIndex(Guid? recipientEntityId) {
        if (recipientEntityId == null)
        {
            return 0;
        }
        int indexOfRecipient = availableRecipientEntityIds.IndexOf(recipientEntityId.Value);
        return indexOfRecipient == -1 ? indexOfRecipient : indexOfRecipient + 1;
    }

    private void UpdatePanel() {
        recipientDropdown.value = GetRecipientDropdownIndex(selectedRecipientEntityId);
        
        var choices = choicesProvider.GetChoices(selectedRecipientEntityId);
        choiceItems?.ForEach(c => Destroy(c.gameObject));
        choiceItems = new List<ChoiceItem>();

        int choiceNumber = 1;
        foreach (var cmd in choices)
        {
            ChoiceItem newChoiceItem = Instantiate(choiceItemPrefab)
                .GetComponent<ChoiceItem>();
            newChoiceItem.transform.SetParent(choiceItemsParent);
            newChoiceItem.Set(choiceNumber++, cmd);

            choiceItems.Add(newChoiceItem);
        };
    }
}
