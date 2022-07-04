using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindVehicle
{
    public static GameObject FindGameObjectInChildWithTag(GameObject _parent, string _tag)
    {
        Transform t = _parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == _tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }
}
