using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableListString : MonoBehaviour
{
    public struct SerialItem
    {
        public string name;
        public int count;
    }

    //This is our seriablizable object
    public List<SerialItem> serialiableList = new List<SerialItem>();
}
