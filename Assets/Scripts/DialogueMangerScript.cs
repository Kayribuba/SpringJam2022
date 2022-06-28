using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueMangerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI SubtitleTextMesh;
    [SerializeField] Animator SubAnimator;
    [Range(0f, 5f)][SerializeField] float cooldownSeconds = 1f;

    public Queue<DialogueObject> dialogueQueue = new Queue<DialogueObject>();

    AudioSource DialogueAudioSource;
    DialogueObject currentDialogue;
    bool dialogueStarted;
    float targetTime = -1;

    void Start()
    {
        DialogueAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (dialogueStarted && !DialogueAudioSource.isPlaying)
            EndDialogue();
        if (dialogueQueue.Count > 0 && !DialogueAudioSource.isPlaying && targetTime < Time.time)
            StartDialogue();
    }

    void EndDialogue()
    {
        dialogueStarted = false;

        if (dialogueQueue.Count == 0)
        {
            SubAnimator.SetBool("SubsOn", false);
            SubtitleTextMesh.text = "";
        }
    }
    void StartDialogue()
    {
        dialogueStarted = true;
        SubAnimator.SetBool("SubsOn", true);
        ShootDialogue();
    }
    void ShootDialogue()
    {
        currentDialogue = dialogueQueue.Dequeue();
        DialogueAudioSource.PlayOneShot(currentDialogue.clip);
        SubtitleTextMesh.color = currentDialogue.TextColor;
        SubtitleTextMesh.text = currentDialogue.dialogueText;
        targetTime = Time.time + cooldownSeconds;
    }
}
