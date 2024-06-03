using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializeableDictionary<TKey, TValue>: Dictionary<TKey, TValue>,ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    public void OnBeforeSerialize()
    {
        this.keys.Clear();
        this.values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            this.keys.Add(pair.Key);
            this.values.Add(pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        this.Clear();
        if(keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary, but the amout of keys(" + keys.Count + ") and values(" + values.Count + ") does not match. (Did you add a new key without adding a value?)");
        }
        for (int i = 0; i < Mathf.Min(this.keys.Count, this.values.Count); i++)
        {
            this.Add(this.keys[i], this.values[i]);
        }
    }
}
