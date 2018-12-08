using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStack {

    public Item item;
    public int amount;
    public int MaxStackSize;
    public bool LockItem;
    
    public ItemStack(Item item, int amount = 0, int maxStackSize = int.MaxValue, bool lockItem = false)
    {
        this.item = item;
        this.amount = amount;
        MaxStackSize = maxStackSize;
        LockItem = lockItem;
    }

    public ItemStack Merge(ItemStack itemStack)
    {
        if (itemStack != null && (item == null || item == Item.EMPTY || item == itemStack.item) && !IsFull())
        {
            item = itemStack.item;
            amount += itemStack.amount;
            if (amount > MaxStackSize)
            {
                itemStack.amount = amount - MaxStackSize;
                amount = MaxStackSize;
            }
            else
            {
                if (itemStack.LockItem) itemStack.amount = 0;
                else itemStack = null;
            }
        }
        
        return itemStack;
    }
    
    public bool IsOverflowing()
    {
        return amount > MaxStackSize;
    }

    public bool IsFull()
    {
        return amount >= MaxStackSize;
    }

    public ItemStack RemoveOverflow()
    {
        if (!IsOverflowing()) return null;
        var stack = new ItemStack(item, amount - MaxStackSize);
        amount = MaxStackSize;
        return stack;
    }
    
    public bool IsEmpty()
    {
        return amount <= 0;
    }

    public ItemStack Copy()
    {
        return new ItemStack(item, amount, MaxStackSize, LockItem);
    }
}
