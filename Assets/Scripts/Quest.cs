using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : ScriptableObject, ISerializationCallbackReceiver
{
    public bool questActive;

    public virtual void startQuest()
    {

    }

    public virtual bool isDone()
    {
       return false;
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        questActive = false;
    }
}
