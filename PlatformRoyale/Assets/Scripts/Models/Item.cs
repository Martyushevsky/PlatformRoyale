using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isStackable = false;
    public int stackLimit = 0;

    public Item(ItemParams itemParams)
    {

    }
}

public struct ItemParams
{

}