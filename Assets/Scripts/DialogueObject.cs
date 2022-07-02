using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu]
public class DialogueObject : ScriptableObject
{
    [TextArea(1, 10)] public string dialogueText;
    public AudioClip clip;
    public TMP_FontAsset textFont;
    public Color TextColor = Color.white;
    [Header("Trigger Events")]
    public bool TriggerEvent;
    public string[] UseSlotID;
    public InteractionType interactionType;
}
