using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventroyScript : MonoBehaviour
{
    public static InventroyScript Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    // The list of items in the inventory
    private List<string> items = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance of the inventory exists
        }
    }

    // Add an item to the inventory
    public void AddItem(string item)
    {
        items.Add(item);
        Debug.Log("Added " + item + " to the inventory");
    }

    // Remove an item from the inventory
    public void RemoveItem(string item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Removed " + item + " from the inventory");
        }
        else
        {
            Debug.LogWarning("Item " + item + " not found in the inventory");
        }
    }

    // Print the contents of the inventory
    public void PrintInventory()
    {
        Debug.Log("Inventory Contents:");
        foreach (string item in items)
        {
            Debug.Log("- " + item);
        }
    }
}
