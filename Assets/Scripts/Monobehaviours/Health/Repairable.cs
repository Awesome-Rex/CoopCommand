using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{
    public bool needsRepair = false;

    public float repairProgress;
    public float maxRepairProgress;

    private void Update()
    {
        if (needsRepair && repairProgress >= maxRepairProgress)
        {
            needsRepair = false;
        }
    }
}
