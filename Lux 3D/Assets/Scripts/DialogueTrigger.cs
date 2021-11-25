using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(3,10)]
    public string[] dialogue;
    private int dialogueNum = 1;
    public Text dialogueTextBox;
    private bool activateDialogue = false;
    public GameObject[] ActivatedGameObjects;
    // Start is called before the first frame update
    void Start()
    {
        dialogueNum = 0;
        dialogueTextBox = GameObject.FindGameObjectWithTag("Dialogue Text Box").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            dialogueTextBox.gameObject.SetActive(true);
        }
        else
        {
            dialogueTextBox.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if(other.gameObject.GetComponent<ThirdPersonPlayer>() != null)
        {
            activateDialogue = true;
            dialogueTextBox.text = dialogue[dialogueNum];
            dialogueTextBox.enabled = activateDialogue;
            other.gameObject.GetComponent<ThirdPersonPlayer>().SetDialogueSpoken(gameObject);
        }
    }

    public void NextOption()
    {
        if(dialogueNum + 1 >= dialogue.Length)
        {
            dialogueNum = 1;
            
        }
        else
        {
            dialogueNum++;
        }
        if(dialogueNum >= dialogue.Length - 2)
        {
            ActivateOtherGameObjects();
        }
        dialogueTextBox.text = dialogue[dialogueNum];
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<ThirdPersonPlayer>() != null)
        {
            activateDialogue = false;
            dialogueTextBox.text = "";
            dialogueTextBox.enabled = activateDialogue;
            dialogueNum = 0;
            other.gameObject.GetComponent<ThirdPersonPlayer>().SetDialogueSpoken(null);
        }
    }
    
    private void ActivateOtherGameObjects()
    {
        for (int ii = 0; ii < ActivatedGameObjects.Length; ++ii)
        {
            ActivatedGameObjects[ii].SetActive(true);
        }
    }

    public int GetDialogueCount()
    {
        return (dialogueNum);
    }
    public bool DialogueCompleted()
    {
        return (dialogueNum >= dialogue.Length - 1);
    }
}
