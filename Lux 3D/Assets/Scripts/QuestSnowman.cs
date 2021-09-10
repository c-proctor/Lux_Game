using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSnowman : MonoBehaviour
{
    public GameObject QuestCompletedPrefab;
    Mesh CompletedMesh;
    public QuestItemCheck PairedQuest;
    bool changed = false;
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
        if (PairedQuest.GetQuestCompletionState() && !changed)
        {
            Instantiate(QuestCompletedPrefab, transform.position, transform.rotation);
            FindObjectOfType<InventorySlot>().slots.Clear();
            FindObjectOfType<GameplayUI>().ItemGot();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("CheckForCompletion");
        }
    }
}
