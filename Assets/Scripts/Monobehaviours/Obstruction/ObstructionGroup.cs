using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TransformTools;

public class ObstructionGroup : MonoBehaviour
{
    public ObstructionCamera camera;

    public List<MeshRenderer> toHide;
    
    public AxisOrder offset;

    public bool hidden
    {
        get
        {
            return _hidden;
        }
        set
        {
            _hidden = value;

            foreach (MeshRenderer i in toHide)
            {
                if (value)
                {
                    i.enabled = false;
                }
                else
                {
                    i.enabled = true;
                }
            }
        }
    }

    private bool _hidden = false;

    [ContextMenu("Get Meshes")]
    public void GetMeshes()
    {
        toHide = new List<MeshRenderer>();

        foreach (MeshRenderer i in GetComponentsInChildren<MeshRenderer>())
        {
            toHide.Add(i);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(offset.ApplyPosition(transform), 0.5f);
    }

    private void Update()
    {
        //bool visible = false;

        //foreach (Renderer i in toHide)
        //{
        //    if (i.isVisible)
        //    {
        //        visible = true;
        //    }
        //}

        //if (visible)
        //{
        //    hidden = camera.BetweenSubject(offset.ApplyPosition(transform));
        //}
        //else
        //{
        //    hidden = false;
        //}

        hidden = camera.BetweenSubject(offset.ApplyPosition(transform));
        Debug.Log(gameObject.name + hidden.ToString());
    }
}
