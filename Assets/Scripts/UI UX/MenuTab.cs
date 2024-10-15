using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTab : MonoBehaviour
{
    //Menu Gameobjects for the menu to switch between the sections
    public GameObject MainMenu;

    public GameObject SkillTree;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(false);
        SkillTree.SetActive(false);
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
        if (SkillTree.active == false)
        {
            SkillTree.SetActive(true);
        }
    }
}
