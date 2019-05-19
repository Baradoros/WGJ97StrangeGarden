using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// So you can drag-and-drop various game effects, to decide how some object works.
/// </summary>
public abstract class GameEffect<TArgs> 
{
    public abstract void Execute(TArgs args);
}
