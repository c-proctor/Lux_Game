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
        if (PairedQuest.GetQuestCompletionState() && !changed)
        {
            Instantiate(QuestCompletedPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
