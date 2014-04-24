Simple Unity3D ingame gizmos helper
-----------------------------------

Simple usage:
```cs

using UnityEngine;
using System.Collections;
using Omgnull;

public class MyAwesomeBehaviour : MonoBehaviour
{
    GizmosHelper helper;

    void Start ()
    {
        helper = new GizmosHelper(transform);
        helper.InitilalizeLines(GizmosHelper.GetDefaultLineClasses());
        helper.Draw();
    }
}

```