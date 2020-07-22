using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TransformTools;

public class ObstructionGroup : MonoBehaviour
{
    public new ObstructionCamera camera;

    public List<MeshRenderer> toHide;
    public Material replacement = null;
    
    public AxisOrder offset;

    public bool hidden
    {
        get
        {
            return _hidden;
        }
        set
        {
            

            if (!_hidden && value) // on "hidden" become true
            {
                previous = new Dictionary<Renderer, Material[]>();
            }

            _hidden = value;

            foreach (MeshRenderer i in toHide)
            {
                if (value) //will be hidden
                {
                    if (replacement == null)
                    {
                        i.enabled = false;
                    } else
                    {
                        //saves previous state

                        previous.Add(i, i.materials.Clone() as Material[]);

                        i.materials = new Material[] { replacement };
                    }
                }
                else
                {
                    if (replacement == null)
                    {
                        i.enabled = true;
                    } else
                    {
                        i.materials = previous[i];
                    }
                }
            }
        }
    }

    private bool _hidden = false;

    private Dictionary<Renderer, Material[]> previous;

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

    private void Awake()
    {
        previous = new Dictionary<Renderer, Material[]>();

        foreach (MeshRenderer i in toHide)
        {
            previous.Add(i, i.materials.Clone() as Material[]);
        }
    }

    private void Update()
    {
        hidden = camera.BetweenSubject(offset.ApplyPosition(transform));
    }
}
