using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVisualPurplePassive : MonoBehaviour
{
    void Update()
    {
        this.transform.Rotate(0, 0, 40 * Time.deltaTime);
    }
}
