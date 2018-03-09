# DebugMonoBehaviour #

Tiny utillity class that helps to display list of value of fields or properties marked by [DebugDraw] attribute.

## Usage ##

```cs
using Pixigon.OpenSource.Debug;
using UnityEngine;

public class WingController : DebugMonoBehaviour {

    [DebugDraw]
    private float force;

    [DebugDraw]
    private bool isAlive;

    [DebugDraw]
    private float angularVelocity
    {
        get { return rb.angularVelocity; }
    }

    [DebugDraw]
    private Vector2 velocity
    {
        get { return rb.velocity; }
    }
    
    ...
```
