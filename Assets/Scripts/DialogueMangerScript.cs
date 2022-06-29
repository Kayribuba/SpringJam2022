using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueMangerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI SubtitleTextMesh;
    public GameObject[] EffectObjectSlot;
    public string[] ObjectIDs;
    [SerializeField] Animator SubAnimator;
    [SerializeField] InteractionScript IS;
    [Range(0f, 5f)][SerializeField] float cooldownSeconds = 1f;

    public Queue<DialogueObject> dialogueQueue = new Queue<DialogueObject>();

    AudioSource DialogueAudioSource;
    DialogueObject currentDialogue;
    bool dialogueStarted;
    float targetTime = -1;

    Dictionary<string, GameObject> IdToGameobject = new Dictionary<string,GameObject>();

    void Start()
    {
        DialogueAudioSource = GetComponent<AudioSource>();
        for (int i = 0; i < ObjectIDs.Length; i++)
        {
            IdToGameobject.Add(ObjectIDs[i], EffectObjectSlot[i]);
        }
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

        if(currentDialogue.TriggerEvent == true)
        {
            foreach(string slotID in currentDialogue.UseSlotID)
            {
                IS.Interact(currentDialogue.interactionType, IdToGameobject[slotID]);
            }
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
