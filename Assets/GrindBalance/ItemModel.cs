using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public struct ItemModel
{
    [ValueDropdown("GetItemsDropdownList")]
    public string id;
    public float count;

#if UNITY_EDITOR
    // private IEnumerable GetItemsDropdownList => CustomInspectorTools.GetItemsDropdownList();
    private IEnumerable GetItemsDropdownList => new string[] { "Test1", "Test2", "Test3"};
#endif
}
