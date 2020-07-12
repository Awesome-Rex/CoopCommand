using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private GameObject instance;

    protected virtual void Awake()
    {
        transform.SetAsFirstSibling();

        if (_ETERNAL.I == null)
        {
            instance = Instantiate(Resources.Load("_ETERNAL") as GameObject);
            instance.transform.SetAsFirstSibling();
        }
    }
}
