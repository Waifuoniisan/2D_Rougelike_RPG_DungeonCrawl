using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerScript;
using static Skills;
public class SkillTree : MonoBehaviour
{

    public static SkillTree attkSkillTree;

    private void Awake() => attkSkillTree = this;

    public int skillPoint;
    public int[] skillLevels;
    public int[] skillCaps;
    public string[] skillNames;
    public string[] skillDescription;

    public List<Skills> SkillList;
    public GameObject skillHolder;
    
    public List<GameObject> ConnectorList;
    public GameObject connectorHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        //used to upgrade skills
        skillPoint = 20;

        //Skill levels gives you how many skill are in the array
        skillLevels = new int[8];
        //How much skill points or cost of the item will be
        skillCaps = new[] { 1, 2, 2, 3, 3, 2, 2, 4 };

        //This is the names of the skills that will be selected
        skillNames = new[]
            { "Attack", "Dash", "Triple Arrow", "Upgrade 4", "Upgrade 4", "Upgrade 4", "Upgrade 4", "Upgrade 4", };
        //Skill Discription or effect of the skill bought
        skillDescription = new[]
        {
            "Press 'Z' to shoot your arrow Base Damage",
            "Press 'Shift' to dash 5 units",
            "Unlock to be able to shoot 3 arrows in quick succession",
            "Does a Thing",
            "Does another thing",
            "Gives you a boost",
            "Who knows what will happen",
            "Does a super big thing"
        };
        //this wil grab all the children (the gameobjects under the main/connect object to be read and she if it has the skill(name of script))
        foreach (var skills in skillHolder.GetComponentsInChildren<Skills>()) SkillList.Add(skills);
        
        //this will look for every connector in the connector holder
        foreach (var connector  in connectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);
        
        //for loop, to loop through all of the skill, assigning the id to a number/skill and this reads it to help define
        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        //This tells what upgrade is unlocked with each node used 0 is the base skill
        SkillList[0].connectedSkills = new[] { 1, 4 };
        SkillList[1].connectedSkills = new[] { 2 };
        SkillList[4].connectedSkills = new[] { 5 };
        SkillList[2].connectedSkills = new[] { 3 };
        SkillList[5].connectedSkills = new[] { 6 };
        SkillList[6].connectedSkills = new[] { 7 };
        
        UpdateAllSkillUI();
    }

    //this function is to update all of the skills UI
    public void UpdateAllSkillUI()
    {
        foreach (var skills in SkillList) skills.UpdateUI();
    }

    #region AttackUnlocked
    
    public void AttackUnlocked()
    { 
        CharacterAction a = CharacterAction.Idle;
        
        // Debug.Log(attkSkillTree.SkillList[0]);
        // Debug.Log(id);
        // Debug.Log(attkSkillTree.skillLevels[id]);
        //id == 0
        if (SkillList[0] && skillLevels[0] >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                a = CharacterAction.Attacking;

                if (player.MyDirection == Direction.Left)
                {
                    Instantiate(player.Arrow, player.ProjectileSpawnPoint.position, Quaternion.Euler(0, 0, 90));
                }
            
                if (player.MyDirection == Direction.Right)
                {
                    Instantiate(player.Arrow, player.ProjectileSpawnPoint.position, Quaternion.Euler(0,0,270));
                }
            
                if (player.MyDirection == Direction.Back)
                {
                    Instantiate(player.Arrow, player.ProjectileSpawnPoint.position, Quaternion.Euler(0,0,0));
                }
                if (player.MyDirection == Direction.Front)
                {
                    Instantiate(player.Arrow, player.ProjectileSpawnPoint.position, Quaternion.Euler(0,0,180));
                }
            }
        }
        else
        {
            return;
        }
        player.SetAction(a);
    }
    #endregion

    #region DashUnlocked

    public void DashUnlcoked()
    {
        if (SkillList[0] && skillLevels[0] >= 1)
        {
            Vector2 move = player.PlayerRB.velocity;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (player.MyDirection == Direction.Left)
                {
                     move.x = -player.speed * player.dashDistance;
                }
            
                if (player.MyDirection == Direction.Right)
                {
                    move.x = player.speed * player.dashDistance;
                }
            
                if (player.MyDirection == Direction.Back)
                {
                    move.y = player.speed * player.dashDistance;
                }
                if (player.MyDirection == Direction.Front)
                {
                    move.y = -player.speed * player.dashDistance;
                }
            }
            else
            {
                return;
            }
        }
    }

    #endregion
}
