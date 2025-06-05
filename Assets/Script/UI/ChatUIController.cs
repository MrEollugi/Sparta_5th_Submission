using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class ChatUIController : MonoBehaviour
{
    public static ChatUIController Instance { get; private set; }

    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject chatMessagePrefab;
    [SerializeField] private Transform chatContent;
    [SerializeField] private TMP_InputField chatInput;

    public bool IsChatFocused => chatInput != null && chatInput.isFocused;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        inputActions = new PlayerInputActions();

        inputActions.UI.Cancel.performed += ctx => CloseChatPanel();
        inputActions.UI.SubmitChat.performed += ctx => SubmitMessage();
    }

    private void Start()
    {
        chatPanel.SetActive(false);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void ToggleChatPanel()
    {
        chatPanel.SetActive(!chatPanel.activeSelf);

        if (chatPanel.activeSelf)
        {
            chatInput.ActivateInputField();
        }
    }

    public void SubmitMessage()
    {
        string msg = chatInput.text.Trim();

        if (string.IsNullOrEmpty(msg))
        {
            return;
        }

        GameObject newMsg = Instantiate(chatMessagePrefab, chatContent);
        newMsg.GetComponentInChildren<TMP_Text>().text = msg;

        chatInput.text = "";
        chatInput.ActivateInputField();
    }

    public void CloseChatPanel()
    {
        chatPanel.SetActive(false);
    }
}
