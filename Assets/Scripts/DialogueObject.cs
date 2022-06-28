using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueObject : ScriptableObject
{
    [TextArea(1, 10)] public string dialogueText;
    public AudioClip clip;
    public Color TextColor = Color.white;
}