using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBBQ : MonoBehaviour
{
    public GameObject QuestCompletedPrefab;
    private GameObject PairedBossPrefab;
    Mesh CompletedMesh;
    public QuestItemCheck PairedQuest;
    public BossEnemy bossStatus;
    bool changed = false;
    public GameObject[] ActivatedGameObjects, DeactivatedGameObjects;
    public bool clearInventory;
    private bool QuestCompleted = false;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator CheckForCompletion()
    {
        yield return new WaitForSeconds(0.2f);
        if (PairedQuest.GetQuestCompletionState(player) && !changed)
        {
            if (QuestCompletedPrefab != null)
            {
                PairedBossPrefab = Instantiate(QuestCompletedPrefab, transform.position, transform.rotation);
            }
            ActivateOtherGameObjects();
            DeactivateOtherGameObjects();
            if (clearInventory)
            {
                FindObjectOfType<InventorySlot>().slots.Clear();
                FindObjectOfType<GameplayUI>().ItemGot();
            }
            QuestCompleted = true;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            StartCoroutine("CheckForCompletion");
        }
    }
    private void ActivateOtherGameObjects()
    {
        for (int ii = 0; ii < ActivatedGameObjects.Length; ++ii)
        {
            ActivatedGameObjects[ii].SetActive(true);
        }
    }
    private void DeactivateOtherGameObjects()
    {
        for (int ii = 0; ii < DeactivatedGameObjects.Length; ++ii)
        {
            DeactivatedGameObjects[ii].SetActive(false);
        }
    }
    public bool GetQuestCompleted() 
    { 
        return QuestCompleted; 
    }
}
