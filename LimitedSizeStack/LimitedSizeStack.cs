using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{   
    LinkedList<T> stackList = new LinkedList<T>();
    private int limit;
    public int Count { get; private set; }

    public LimitedSizeStack(int undoLimit)
    {
        limit = undoLimit;
    }

    public void Push(T item)
    {
        if (limit > 0)
        {
            if (stackList.Count > 0) { }
            if (stackList.Count == limit)
            {
                stackList.RemoveFirst();
                Count -= 1;
            }
            stackList.AddLast(item);
            Count += 1;
        }
    }

    public T Pop()
    {
        var lastStackItem = stackList.Last.Value;
        stackList.RemoveLast();
        Count -= 1;
        return lastStackItem;
    }
}
