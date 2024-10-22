using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTab : MonoBehaviour
{
    //Menu Gameobjects for the menu to switch between the sections
    public GameObject MainMenu;
    public GameObject SkillTree;
    public GameObject attkSkills;
    public GameObject DefSkills;
    public GameObject DashSkills;
    public GameObject UnknownSkills;

    enum MenuStates
    {
        MainMenu,
        Stats,
        SkillTree,
        AttackTree,
        DefenseTree,
        DashTree,
        UnknownTree,
        Settings
    }
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(false);
        SkillTree.SetActive(false);
        attkSkills.SetActive(false);
        DefSkills.SetActive(false);
        DashSkills.SetActive(false);
        UnknownSkills.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MenuWindow();
        }
        
    }
    
    private void MenuWindow()
    {
        if (MainMenu.active == true)
        {
            MainMenu.SetActive(false);
        }
        else
        {
            MainMenu.SetActive(true);
        }

    }

    public void SkillWindow()
    {
        if (SkillTree.active == true)
        {
            SkillTree.SetActive(false);
        } 
        else
        {
            SkillTree.SetActive(true);
        }

        if (MainMenu == false)
        {
            SkillTree.SetActive(false);
        }
    }

    public void AttkWindow()
    {
        if (attkSkills.active == true)
        {
            attkSkills.SetActive(false);
        } 
        else
        {
            attkSkills.SetActive(true);
        }
        if (MainMenu == false)
        {
            attkSkills.SetActive(false);
        }
    }
    
    public void DefWindow()
    {
        if (DefSkills.active == true)
        {
            DefSkills.SetActive(false);
        } 
        else
        {
            DefSkills.SetActive(true);
        }
        if (MainMenu == false)
        {
            DefSkills.SetActive(false);
        }
    }
    
    public void DashWindow()
    {
        if (DashSkills.active == true)
        {
            DashSkills.SetActive(false);
        } 
        else
        {
            DashSkills.SetActive(true);
        }
        if (MainMenu == false)
        {
            DashSkills.SetActive(false);
        }
    }
    
    public void UnknownWindow()
    {
        if (UnknownSkills.active == true)
        {
            UnknownSkills.SetActive(false);
        } 
        else
        {
            UnknownSkills.SetActive(true);
        }
        if (MainMenu == false)
        {
            UnknownSkills.SetActive(false);
        }
    }
}
