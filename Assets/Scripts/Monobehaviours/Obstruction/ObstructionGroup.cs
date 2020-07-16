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
            _hidden = value;

            if (value)
            {
                previous = new Dictionary<Renderer, Material[]>();
            }

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

                        Material[] newMats = new Material[i.materials.Length];

                        for (int j = 0; j < i.materials.Length; j++)
                        {
                            newMats[j] = i.materials[j];
                        }

                        previous.Add(i, newMats);

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
            Material[] newMats = new Material[i.materials.Length];

            for (int j = 0; j < i.materials.Length; j++)
            {
                newMats[j] = i.materials[j];
            }

            previous.Add(i, newMats);
        }
    }

    private void Update()
    {
        hidden = camera.BetweenSubject(offset.ApplyPosition(transform));
    }
}
