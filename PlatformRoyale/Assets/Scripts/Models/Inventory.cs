using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<InventoryCell> inventory = new List<InventoryCell>();

    public void AddItems(Item item, int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddItem(item);
        }
    }

    public void RemoveItems(Item item, int count)
    {
        for (int i = 0; i < count; i++)
        {
            RemoveItem(item);
        }
    }

    public void AddItems(InventoryCell inventoryCell)
    {
        Item item = inventoryCell.item;
        int count = inventoryCell.count;
        for (int i = 0; i < count; i++)
        {
            AddItem(item);
        }
    }

    public void RemoveItems(InventoryCell inventoryCell)
    {
        Item item = inventoryCell.item;
        int count = inventoryCell.count;
        for (int i = 0; i < count; i++)
        {
            RemoveItem(item);
        }
    }

    private void AddItem(Item item)
    {
        if (item.isStackable)
        {
            // Если стэкуется
            foreach (InventoryCell cell in inventory)
            {
                // Проверка на наличея придмета в инвенторе
                if (cell.item.Equals(item))
                {
                    // Проверка на максимальный стэк
                    if (cell.count >= item.stackLimit)
                    {
                        // Добавляем предмет в новую ячейку
                        InventoryCell newCell = new InventoryCell();
                        newCell.count = 1;
                        newCell.item = item;
                        inventory.Add(newCell);
                    }
                    else
                    {
                        // Если стэк не полный добавляем предмет в стэк
                        cell.count++;
                    }
                }
            }
        }
        else
        {
            // Если не стэкуется
            // Добавляем предмет в новую ячейку
            InventoryCell newCell = new InventoryCell
            {
                count = 1,
                item = item
            };
            inventory.Add(newCell);
        }
    }
    private void RemoveItem(Item item)
    {
        foreach (InventoryCell cell in inventory)
        {
            // Ищем совпадения в инвенторе
            if (cell.item.Equals(item))
            {
                // Если можно удалить из стэка
                if (cell.count >= 2)
                {
                    cell.count--;
                }
                else
                {
                    // Удаляем ячейку в инвентори
                    inventory.Remove(cell);
                }
            }
        }
    }
}