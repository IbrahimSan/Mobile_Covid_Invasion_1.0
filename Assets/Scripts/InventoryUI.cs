using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent; // The parent object of all the items

    public static bool inventoryIsOpen = false;     //Calling the Inventroy to stay closed until button pressed

    public GameObject inventoryUI;  // The entire UI

    Inventory inventory;    // Our current inventory

    InventorySlot[] slots;  // List of all the slots


    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
       

    }


    // Update the inventory UI by:
    //		- Adding items
    //		- Clearing empty slots
    // This is called using a delegate on the Inventory.
    void UpdateUI()
    {

        // Loop through all the slots
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)  // If there is an item to add
            {
                slots[i].AddItem(inventory.items[i]);   // Add it
            }
            else
            {
                // Otherwise clear the slot
                slots[i].ClearSlot();
            }
        }
    }

    //Button Function to open Inventory
    public void BtnInv()
    {
        if (inventoryIsOpen)
        {
            ShowInventory();
        }
        else
        {
            HideInventory();
        }
    }

    //Showing and Hiding the Inventory 
    public void ShowInventory()
    {
        inventoryUI.SetActive(true);
        Time.timeScale = 0f;
        inventoryIsOpen = false;
    }
    void HideInventory()
    {
        inventoryUI.SetActive(false);
        Time.timeScale = 1f;
        inventoryIsOpen = true;
    }
}
