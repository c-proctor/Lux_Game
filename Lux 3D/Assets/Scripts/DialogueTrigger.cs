using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(3,10)]
    public string dialogue;
    public Text dialogueTextBox;
    private bool activateDialogue = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if(other.gameObject.GetComponent<ThirdPersonPlayer>() != null)
        {
            activateDialogue = true;
            dialogueTextBox.text = dialogue;
            dialogueTextBox.enabled = activateDialogue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<ThirdPersonPlayer>() != null)
        {
            activateDialogue = false;
            dialogueTextBox.text = "";
            dialogueTextBox.enabled = activateDialogue;
        }
    }
}
