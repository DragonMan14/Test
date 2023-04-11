using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KillCount : ScriptableObject, ISerializationCallbackReceiver
{
    public int logsKilled;

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        logsKilled = 0;
    }
}
