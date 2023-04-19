using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "NewItem";           //Name OF the Item
    public Sprite icon = null;                   //Item Icon
    public bool isDefaultItem = false;          //Is the Item default wear?

    public GameObject win;

    // Called when the item is pressed in the inventory
    public virtual void Use()
    {
        //Use the item
        SceneManager.LoadScene(3);
        //Something Might Happen

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    //public void WinGame()
    //{
    //    if (GameObject.FindGameObjectWithTag("AccessCard"))
    //    {
    //        win.SetActive(true);
    //    }
    //    else
    //    {
    //        win.SetActive(false);
    //    }
    //}

}
