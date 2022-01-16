using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool playerSpeakingFirst;

    [Header("Dialogue Text")]
    [SerializeField] private TextMeshProUGUI playerDialogueText;
    [SerializeField] private TextMeshProUGUI npcDialogueText;

    [Header("Continue Buttons")]
    [SerializeField] private GameObject playerContinueButton;
    [SerializeField] private GameObject npcContinueButton;

    [Header("Animation Controllers")]
    [SerializeField] private Animator playerSpeechBubbleAnimator;
    [SerializeField] private Animator npcSpeechBubbleAnimator;

    [Header("UI AudioSource")]
    [SerializeField] private AudioSource uiAudioSource;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;
    [TextArea]
    [SerializeField] private string[] npcDialogueSentences;

    private PlayerController playerController;

    private bool dialogueStarted;

    private int playerIndex;
    private int npcIndex;
    public float speechBubbleAnimationDelay = 0.6f;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void TriggerStartDialogue()
    {
        StartCoroutine(StartDialogue());
    }

    private void Update()
    {
        if (playerContinueButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TriggerNPCContinue();
            }
        }

        if (npcContinueButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TriggerPlayerContinue();
            }
        }
    }

    public IEnumerator StartDialogue()
    {
        playerController.ToggleInteraction();

        if (playerSpeakingFirst)
        {
            playerSpeechBubbleAnimator.SetTrigger("Open");

            yield return new WaitForSeconds(speechBubbleAnimationDelay);
            StartCoroutine(TypePlayerDialogue());
        } else
        {
            npcSpeechBubbleAnimator.SetTrigger("Open");

            yield return new WaitForSeconds(speechBubbleAnimationDelay);
            StartCoroutine(TypeNPCDialogue());
        }
    }

    private IEnumerator TypePlayerDialogue()
    {
        foreach (char letter in playerDialogueSentences[playerIndex].ToCharArray())
        {
            playerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        playerContinueButton.SetActive(true);
    }

    private IEnumerator TypeNPCDialogue()
    {
        foreach (char letter in npcDialogueSentences[npcIndex].ToCharArray())
        {
            npcDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        npcContinueButton.SetActive(true);
    }

    private IEnumerator ContinuePlayerDialogue()
    {
        npcDialogueText.text = string.Empty;
        npcSpeechBubbleAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        playerDialogueText.text = string.Empty;
        playerSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        if (dialogueStarted)
            playerIndex++;
        else
            dialogueStarted = true;

        StartCoroutine(TypePlayerDialogue());
        
    }

    private IEnumerator ContinueNPCDialogue()
    {
        playerDialogueText.text = string.Empty;
        playerSpeechBubbleAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        npcDialogueText.text = string.Empty;
        npcSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        
        if (dialogueStarted)
            npcIndex++;
        else
            dialogueStarted = true;

        StartCoroutine(TypeNPCDialogue());
        
    }

    public void TriggerPlayerContinue()
    {
        uiAudioSource.Play();

        npcContinueButton.SetActive(false);

        if (playerIndex >= playerDialogueSentences.Length - 1)
        {
            npcDialogueText.text = string.Empty;

            npcSpeechBubbleAnimator.SetTrigger("Close");

            playerController.ToggleInteraction();
        }
        else
        {
            StartCoroutine(ContinuePlayerDialogue());
        }
    }

    public void TriggerNPCContinue()
    {
        uiAudioSource.Play();

        playerContinueButton.SetActive(false);

        if (npcIndex >= npcDialogueSentences.Length - 1)
        {
            playerDialogueText.text = string.Empty;

            playerSpeechBubbleAnimator.SetTrigger("Close");

            playerController.ToggleInteraction();
        }
        else
        {
            StartCoroutine(ContinueNPCDialogue());
        }
    }
}
