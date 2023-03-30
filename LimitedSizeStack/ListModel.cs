using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LimitedSizeStack;

public class ListModel<TItem>
{
    public List<TItem> Items { get; }
    private LimitedSizeStack<TItem> numberHistory;
    private LimitedSizeStack<int> removedIndexHistory;

    public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
    {
    }

    public ListModel(List<TItem> items, int undoLimit)
    {
        Items = items;
        numberHistory = new LimitedSizeStack<TItem>(undoLimit);
        removedIndexHistory = new LimitedSizeStack<int>(undoLimit);
    }

    public void AddItem(TItem item)
    {
        Items.Add(item);
        numberHistory.Push(item);
    }

    public void RemoveItem(int index)
    {
        if (Items.Count != 0)
        {
            numberHistory.Push(Items[index]);
            removedIndexHistory.Push(index);
            Items.RemoveAt(index);
        }
    }

    public bool CanUndo()
    {
        return numberHistory.Count != 0;
    }

    public void Undo()
    {
        if (CanUndo())
        {
            var lastOp = numberHistory.Pop();
            if (Items.Contains(lastOp))
            {
                Items.Remove(lastOp);
            }
            else
            {
                Items.Insert(removedIndexHistory.Pop(), lastOp);
            }
        }
    }
}