using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSnowman : MonoBehaviour
{
    public GameObject QuestCompletedPrefab;
    public GameObject PlacedBossPrefab;
    Mesh CompletedMesh;
    public QuestItemCheck PairedQuest;
    public BossEnemy bossStatus;
    bool changed = false;
    public GameObject[] ActivatedGameObjects;
    private bool QuestCompleted = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckForCompletion()
    {
        yield return new WaitForSeconds(0.2f);
        if (PairedQuest.GetQuestCompletionState(player) && !changed)
        {
            //PlacedBossPrefab = Instantiate(QuestCompletedPrefab, transform.position, transform.rotation);
            ActivateOtherGameObjects();
            PlacedBossPrefab.GetComponent<BossEnemy>().ActivateBoss(true);
            FindObjectOfType<InventorySlot>().slots.Clear();
            FindObjectOfType<GameplayUI>().ItemGot();
            QuestCompleted = true;
            gameObject.SetActive(false);
        }
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
    public bool GetQuestCompleted()
    {
        return QuestCompleted;
    }
}
