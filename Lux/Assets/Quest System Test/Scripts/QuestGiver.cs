using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
   public Quest quest;
   
   public TestPlayer player;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;

   public void OpenQuestWindow()
   {
       questWindow.SetActive(true);
        titleText.text = quest.title;
       descriptionText.text = quest.description;
   }

   public void AcceptQuest()
   {
       questWindow.SetActive(false);
       quest.isActive = true;
       // give to player
       player.quest = quest;
   }
}
