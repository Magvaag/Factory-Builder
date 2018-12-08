
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Inventory
{

    // TODO : Capacity, like the villager only able to carry 10 items at a time, or a weight system
    
    public ItemStack[] ItemStacks;
    public int CarrySize; // Max total weight in inventory
    public bool Persistant = true; // If the inventory should print all its items regardless of if they are 0 or not

    public Inventory(ItemStack[] itemStacks, int carrySize = -1, bool persistant = true)
    {
        ItemStacks = itemStacks;
        CarrySize = carrySize;
        Persistant = persistant;
    }
    
    public Inventory(int size, int carrySize = -1, bool persistant = true)
    {
        ItemStacks = new ItemStack[size];
        CarrySize = carrySize;
        Persistant = persistant;
    }

    public bool IsSpaceForItem(Item item)
    {
        return ItemStacks.Any(itemStack => itemStack == null || (itemStack.item == null || itemStack.item == Item.EMPTY) || (itemStack.item == item && !itemStack.IsFull()));
    }

    public ItemStack AddItemStack(ItemStack itemStack)
    {
        // Try to fill the stacks that are already there before using empty space 
        foreach (var stack in ItemStacks)
        {
            if (itemStack == null || itemStack.amount == 0) return itemStack;
            if (stack != null) itemStack = stack.Merge(itemStack);
        }
        
        // If there are any empty stacks in the inventory, fill them next
        for (var i = 0; i < ItemStacks.Length; i++)
        {
            if (itemStack == null || itemStack.amount == 0) return itemStack;
            if (ItemStacks[i] == null)
            {
                // Since the itemstack doesn't exist from before, we assume that we can fit a lot in there
                // If this is not the case then the developer (that's me) has to create the itemstack first
                ItemStacks[i] = new ItemStack(itemStack.item, itemStack.amount);
                itemStack.amount = 0;
            }
        }

        // Return the remains if there are any
        return itemStack;
    }

    public void RemoveItems(ItemStack itemStack)
    {
        foreach (var stack in ItemStacks)
        {
            if (itemStack.amount <= 0) return;
            if (stack.item == itemStack.item && stack.amount > 0)
            {
                var min = Math.Min(stack.amount, itemStack.amount);
                stack.amount -= min;
                itemStack.amount -= min;
                
                // TODO : Handle stack.LockItem case.
            }
        }
    }

    public ItemStack GetFirstItem()
    {
        // WARNING: This will also remove that item!
        foreach (var stack in ItemStacks)
        {
            if (stack != null && stack.amount > 0)
            {
                stack.amount--;
                return new ItemStack(stack.item, 1);
            }
        }

        return null;
    }
    
    public void RemoveItemStacks(ItemStack[] itemStacks)
    {
        foreach (var stack in itemStacks)
        {
            RemoveItems(stack);
        }
    }

    public void LockItemStack(int itemStack, Item item = null)
    {
        if (ItemStacks[itemStack] == null) ItemStacks[itemStack] = new ItemStack(item, 0);
        else ItemStacks[itemStack].item = item;
        ItemStacks[itemStack].LockItem = true;
    }
    
    public void LockItemStacks(Item item = null)
    {
        for (var i = 0; i < ItemStacks.Length; i++)
        {
            LockItemStack(i, item);
        }
    }

    public void RestrictItemStack(int itemStack, int maxSize)
    {
        if (ItemStacks[itemStack] == null) ItemStacks[itemStack] = new ItemStack(null, 0, maxSize);
        else ItemStacks[itemStack].MaxStackSize = maxSize;
    }
    
    public void RestrictItemStacks(int maxSize)
    {
        for (var i = 0; i < ItemStacks.Length; i++)
        {
            RestrictItemStack(i, maxSize);
        }
    }

    public int GetSize()
    {
        return ItemStacks.Length;
    }
    
    public List<ItemStack> GetItemStacksWithItem(Item item)
    {
        return ItemStacks.Where(itemStack => itemStack != null && itemStack.item == item && itemStack.amount > 0).ToList();
    }

    public void Clear()
    {
        for (var i = 0; i < ItemStacks.Length; i++)
        {
            ItemStacks[i] = null;
        }
    }
    
    public void ClearItem(Item item)
    {
        for (var i = 0; i < ItemStacks.Length; i++)
        {
            var stack = ItemStacks[i];
            if (stack != null && stack.item == item)
            {
                if (ItemStacks[i].LockItem)
                {
                    ItemStacks[i] = ItemStacks[i].Copy();
                    ItemStacks[i].amount = 0;
                }
                else ItemStacks[i] = null;
            }
        }
    }

    public void ClearSlot(int slot)
    {
        if (slot >= 0 && slot < ItemStacks.Length)
        {
            if (ItemStacks[slot].LockItem)
            {
                ItemStacks[slot] = ItemStacks[slot].Copy();
                ItemStacks[slot].amount = 0;
                ItemStacks[slot].item = Item.EMPTY;
            }
            else ItemStacks[slot] = null;
        }
    }
    
    public List<ItemStack> AddItemStacks(List<ItemStack> itemStacks)
    {
        if (itemStacks == null) return null;
        // Try to fill the stacks that are already there before using empty space 
        for (var i = 0; i < itemStacks.Count; i++)
        {
            var stack = itemStacks[i];
            stack = AddItemStack(stack);

            if (stack != null)
            {
                var items = new List<ItemStack> { stack };
                for (var j = i+1; j < itemStacks.Count; j++)
                {
                    items.Add(itemStacks[j]);
                }
                return items;
            }
        }

        return null;
    }

    public bool HasItems(ItemStack[] itemStacks)
    {
        // TODO : This does not work if the input itemStacks has duplicate items!
        // TODO : Run this through some kind of merge method, mergin duplicates into same itemstack
        return itemStacks.All(itemStack => GetItemCount(itemStack.item) >= itemStack.amount);
    }

    public bool IsEmpty()
    {
        return !ItemStacks.Any(stack => stack != null && stack.amount > 0);
    }

    public bool ContainsItem(Item item)
    {
        return ItemStacks.Any(stack => stack != null && stack.item == item && stack.amount > 0);
    }

    public int GetItemCount(Item item)
    {
        return ItemStacks.Where(stack => stack != null && stack.item == item).Sum(stack => stack.amount);
    }

    public bool IsCompletelyFull()
    {
        return !ItemStacks.Any(itemStack => itemStack == null || !itemStack.IsFull());
    }
    
    public void TransferItemToInventory(Inventory inventory, Item item)
    {
        // TODO : Doesn't work with duplicate stacks?
        
        // Separate the items
        var gatheredItems = GetItemStacksWithItem(item);
        Debug.Log("Gathered Items: " + gatheredItems);
        ClearItem(item);
        
        // Move over the items
        var leftoverItems = inventory.AddItemStacks(gatheredItems);
        
        // Put the remaining items back in the inventory
        AddItemStacks(leftoverItems);
    }

}