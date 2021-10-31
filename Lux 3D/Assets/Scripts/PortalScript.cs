using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public string LevelName;
    public bool IsActive;
    public GameObject[] PortalPlanes;
    public QuestSnowman PairedQuestSnowman;
    public QuestBBQ PairedQuestBBQ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int ii = 0; ii < PortalPlanes.Length; ++ii)
        {
            PortalPlanes[ii].SetActive(IsActive);
        }
        if(PairedQuestBBQ != null)
        {
            IsActive = PairedQuestBBQ.GetQuestCompleted();
        }
        if(PairedQuestSnowman != null)
        {
            IsActive = PairedQuestSnowman.GetQuestCompleted();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetSceneByName(LevelName) != null && IsActive)
        {
            SceneManager.LoadScene(LevelName);
        }
        
    }
}
