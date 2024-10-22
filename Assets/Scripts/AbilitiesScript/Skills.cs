using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using static SkillTree;
using Image = UnityEngine.UI.Image;
using static PlayerScript;
public class Skills : MonoBehaviour
{
    //This is to assign a number to a skill
    public int id;
    //THis is the text mesh that will be attached to the image/or skill boxes I made and imported
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    public int[] connectedSkills;
    public static Skills attkSkills;
    
    private void Awake() => attkSkills = this;
    
    public void UpdateUI()
    {
        // '$' string interpolation (Definition: Insert [something of a different nature] into something else) used for making a convenient syntax to create formatted string 
        //alt enter with highlighted script name to import static member which goes right into using ..... up top
        //  \n adds new line 
        //this will tell the system to fill in the level of the skill, the max level the skill can go to and the names of skill 
        titleText.text = $"{attkSkillTree.skillLevels[id]}/{attkSkillTree.skillCaps[id]}\n{attkSkillTree.skillNames[id]}";
        //This will fill in the description of the skill in the text area and then show how many skillpoints we have and how many skillpoints it costs
        descriptionText.text = $"{attkSkillTree.skillDescription[id]}\nCost: {attkSkillTree.skillPoint}/1SP";
        
        //This will change the color of the image that was clicked on and change it simming that it has been selected otherwise not showing the other being selected ro completed
        GetComponent<Image>().color = attkSkillTree.skillLevels[id] >= attkSkillTree.skillCaps[id] ? Color.yellow :
            attkSkillTree.skillPoint >= 1 ? Color.green : Color.white;
        
        //This will show the skills and connectors as the skill progress setting the game objects active as they unlock
        foreach (var connectedSkill in connectedSkills)
        {
            attkSkillTree.SkillList[connectedSkill].gameObject.SetActive(attkSkillTree.skillLevels[id] > 0);
            attkSkillTree.ConnectorList[connectedSkill].SetActive(attkSkillTree.skillLevels[id] > 0);
        }
    }

    public void Buy()
    {
        //this will be checking the cost of the skill locking the skills so the cant be used or bought
        if (attkSkillTree.skillPoint < 1 || attkSkillTree.skillLevels[id] >= attkSkillTree.skillCaps[id]) return;
        attkSkillTree.skillPoint -= 1;
        attkSkillTree.skillLevels[id]++;
        attkSkillTree.UpdateAllSkillUI();
        
    }
    
    public void AttackUnlocked()
    {
        CharacterAction a = CharacterAction.Idle;
        if (id == 0 && attkSkillTree.skillLevels[id] >= 1)
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


    // public void DashUnlcoked()
    // {
    //     if (id == 1 && attkSkillTree.skillLevels[id] >= 1)
    //     {
    //         Vector2 move = player.PlayerRB.velocity;
    //         if (Input.GetKey(KeyCode.LeftShift))
    //         {
    //             if (player.MyDirection == Direction.Left)
    //             {
    //                  move.x = -player.speed * player.dashPower;
    //             }
    //         
    //             if (player.MyDirection == Direction.Right)
    //             {
    //                 move.x = player.speed * player.dashPower;
    //             }
    //         
    //             if (player.MyDirection == Direction.Back)
    //             {
    //                 move.y = player.speed * player.dashPower;
    //             }
    //             if (player.MyDirection == Direction.Front)
    //             {
    //                 move.y = -player.speed * player.dashPower;
    //             }
    //             
    //         }
    //         else
    //         {
    //             return;
    //         }
    //     }
    // }

}