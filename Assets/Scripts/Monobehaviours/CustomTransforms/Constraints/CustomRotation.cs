﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using TransformTools;
using EditorTools;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

[System.Serializable]
public class CustomRotation : CustomTransformLinks<Quaternion>
{
    public Quaternion rotation
    {
        get
        {
            return GetRotation(Space.World);
        }

        set
        {
            if (!(space == Space.Self && link == Link.Offset))
            {
                if (space == Space.World)
                {
                    this.value = value;
                }
                operationalRotation = SetRotation(value.eulerAngles, Space.World);
            }
            else
            {
                this.value = SetRotationLocal(offset.ReverseRotation(value).eulerAngles, Space.World);
            }
        }
    }
    public Quaternion localRotation
    {
        get
        {
            return GetRotation(Space.Self);
        }
        set
        {
            if (!(space == Space.Self && link == Link.Offset))
            {
                operationalRotation = SetRotation(value.eulerAngles, Space.Self);
            }
            else
            {
                this.value = SetRotationLocal(offset.ReverseRotation(SetRotation(value.eulerAngles, Space.Self)).eulerAngles, Space.World);
            }
        }
    }

    public Vector3 eulerAngles
    {
        get
        {
            return rotation.eulerAngles;
        }
        set
        {
            rotation = Quaternion.Euler(value);
        }
    }
    public Vector3 localEulerAngles
    {
        get
        {
            return localRotation.eulerAngles;
        }
        set
        {
            localRotation = Quaternion.Euler(value);
        }
    }

    public Quaternion rotationRaw
    {
        get
        {
            if (space == Space.Self && link == Link.Offset)
            {
                return GetRotationRaw(Space.World);
            } else
            {
                return GetRotation(Space.World);
            }
        }

        set
        {
            if (space == Space.Self && link == Link.Offset) {
                this.value = SetRotationRawLocal(value.eulerAngles, Space.World);
            }
        }
    }
    public Quaternion localRotationRaw
    {
        get
        {
            if (space == Space.Self && link == Link.Offset)
            {
                return GetRotationRaw(Space.Self);
            } else
            {
                return GetRotation(Space.Self);
            }
        }
        set
        {
            if (space == Space.Self && link == Link.Offset)
            {
                this.value = SetRotationRawLocal(value.eulerAngles, Space.Self);
            }
        }
    }

    public Vector3 eulerAnglesRaw
    {
        get
        {
            return rotationRaw.eulerAngles;
        }
        set
        {
            rotationRaw = Quaternion.Euler(value);
        }
    }
    public Vector3 localEulerAnglesRaw
    {
        get
        {
            return localRotationRaw.eulerAngles;
        }
        set
        {
            localRotationRaw = Quaternion.Euler(value);
        }
    }

    private Quaternion operationalRotation
    {
        get
        {
            return transform.rotation;
        }
        set
        {
            transform.rotation = value;
        }
    }

    public Vector3 up
    {
        get
        {
            return (rotation * Vector3.up).normalized;
        }
        set
        {
            rotation = (Quaternion.LookRotation(value) * Quaternion.Euler(90f, 0f, 0f));
        }
    }
    public Vector3 forward
    {
        get
        {
            return (rotation * Vector3.forward).normalized;
        }
        set
        {
            rotation = Quaternion.LookRotation(value);
        }
    }
    public Vector3 right
    {
        get
        {
            return (rotation * Vector3.right).normalized;
        }
        set
        {
            rotation = (Quaternion.LookRotation(value) * Quaternion.Euler(0f, -90f, 0f));
        }
    }

    private Quaternion parentRot;

    public override void SetToTarget()
    {
        target = GetTarget();

        if (enabled) {
            operationalRotation = target;

            RecordParent();
        }
    }
    public override void MoveToTarget()
    {
        target = GetTarget();

        if (enabled)
        {
            if (space == Space.World)
            {
                operationalRotation = target;
            }
            else if (space == Space.Self)
            {
                if (link == Link.Offset)
                {
                    if (!follow)
                    {
                        operationalRotation = target;
                    } else {
                        operationalRotation = transition.MoveTowards(operationalRotation, target);
                    }
                }
                else if (link == Link.Match)
                {
                    if (_ETERNAL.I.counter)
                    {
                        operationalRotation = target;
                    }
                }
            }

            if (_ETERNAL.I.counter)
            {
                RecordParent();
            }
        }
    }
    public override Quaternion GetTarget()
    {
        Quaternion target = new Quaternion();

        if (space == Space.World)
        {
            target = value;
        }
        else if (space == Space.Self)
        {
            if (link == Link.Offset)
            {
                target = parentRot * value; //++++++++offset
                target = offset.ApplyRotation(this, target);
            } else if (link == Link.Match)
            {
                SetPrevious();

                //target = parentRot * previous; //WORKS!
                target = Linking.TransformEuler(previous, parent.rotation);
            }
        }

        return target;
    }

    public override void TargetToCurrent()
    {
        if (space == Space.Self)
        {
            if (link == Link.Offset)
            {
                rotation = operationalRotation;
            }
            else if (link == Link.Match)
            {
                //already set!!!
                //++++++++++++++++++++++MAKE A DEBUG.LOG or EXCEPTION
            }
        }
        else if (space == Space.World)
        {
            value = operationalRotation;
        }
    }

    public override void RecordParent()
    {
        parentRot = parent.rotation;
    }

    public Quaternion Rotate(Vector3 eulers, Space relativeTo = Space.Self)
    {
        if (relativeTo == Space.Self)
        {
            return operationalRotation * Quaternion.Euler(eulers); //WORKS!
        } else
        {
            return Quaternion.Euler(eulers) * operationalRotation; //WORKS!
        }
    }
    public Quaternion Rotate(Quaternion from, Vector3 eulers, Space relativeTo = Space.Self)
    {
        if (relativeTo == Space.Self)
        {
            return from * Quaternion.Euler(eulers); //WORKS!
        }
        else
        {
            return Quaternion.Euler(eulers) * from; //WORKS!
        }
    }

    public Quaternion SetRotation(Vector3 rotation, Space relativeTo = Space.Self)
    {
        if (relativeTo == Space.Self)
        {
            return parentRot * Quaternion.Euler(rotation); //WORKS!
        }
        else
        {
            return Quaternion.Euler(rotation); //WORKS!
        }
    }
    public Quaternion SetRotationLocal (Vector3 rotation, Space relativeTo = Space.Self) {
        if (relativeTo == Space.Self)
        {
            return Quaternion.Euler(rotation);
        }
        else
        {
            return Linking.InverseTransformEuler(Quaternion.Euler(rotation), parentRot);
        }
    }
    public Quaternion GetRotation(Space relativeTo = Space.Self)
    {
        if (relativeTo == Space.Self) {
            return Quaternion.Inverse(parentRot) * operationalRotation; //WORKS!
        } else
        {
            return operationalRotation; //WORKS!
        }
    }

    public Quaternion SetRotationRaw(Vector3 rotation, Space relativeTo = Space.Self)
    {
        return SetRotation(rotation, relativeTo);
    }
    public Quaternion SetRotationRawLocal(Vector3 rotation, Space relativeTo = Space.Self)
    {
        return SetRotationLocal(SetRotation(SetRotationLocal(rotation, relativeTo).eulerAngles, Space.Self).eulerAngles, Space.World);
    }
    public Quaternion GetRotationRaw(Space relativeTo = Space.Self)
    {
        if (space == Space.Self && link == Link.Offset)
        {
            if (relativeTo == Space.Self) {
                return SetRotation(offset.ReverseRotation(this, /*SetRotation(GetRotation(relativeTo).eulerAngles, relativeTo)*/ target).eulerAngles, Space.Self);
            } else
            {
                return offset.ReverseRotation(this, target);
            }
        }
        else
        {
            if (space == Space.Self)
            {
                //return GetRotation(relativeTo);
                return SetRotationLocal(target.eulerAngles, Space.World);
            }
            else // relative to world
            {
                return SetRotation(target.eulerAngles, Space.World);
            }
        }
    }

    public override void SetPrevious()
    {
        previous = Linking.InverseTransformEuler(operationalRotation, parentRot);
    }

    public override void Switch(Space newSpace, Link newLink)
    {
        Quaternion originalRotation = rotation;
        Quaternion originalLocalRotation = localRotation;

        if (space == Space.World)
        {
            if (newSpace == Space.Self)
            {
                if (newLink == Link.Offset) //world > offset
                {
                    space = Space.Self;
                    link = Link.Offset;
                    
                    //auto keep offset
                    SetToTarget();
                    value = offset.ReverseRotation(this, Linking.InverseTransformEuler(originalRotation, parent.rotation));
                }
                else if (newLink == Link.Match) //world > match
                {
                    space = Space.Self;
                    link = Link.Match;
                }
            }
        }
        else if (space == Space.Self)
        {
            if (link == Link.Offset)
            {
                if (newSpace == Space.World) //offset > world
                {
                    space = Space.World;
                    rotation = originalRotation;
                }
                else
                {
                    if (newLink == Link.Match) //offset > match
                    {
                        link = Link.Match;
                    }
                }
            }
            else if (link == Link.Match)
            {
                if (newSpace == Space.World) //match > world
                {
                    space = Space.World;
                    rotation = originalRotation;
                }
                else
                {
                    if (newLink == Link.Offset) //match > offset
                    {
                        link = Link.Offset;
                        
                        //auto keep offset
                        SetToTarget();

                        value = offset.ReverseRotation(this, Linking.InverseTransformEuler(originalRotation, parent.rotation));
                    }
                }
            }
        }
    }
    public override void SwitchParent(Transform newParent)
    {
        if (space == Space.Self)
        {
            Quaternion originalRotation = rotation;
            Quaternion originalLocalRotation = localRotation;

            if (link == Link.Offset)
            {
                parent = newParent;

                rotation = offset.ReverseRotation(originalRotation);

            }
            else if (link == Link.Match)
            {
                parent = newParent;

                rotation = originalRotation;
            }
        }
    }
    public override void RemoveOffset()
    {
        if (space == Space.Self && link == Link.Offset)
        {
            rotation = offset.ApplyRotation(this, rotation);
        }

        offset = new AxisOrder(null, offset.variety, offset.space);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start() { }

#if UNITY_EDITOR
    [CustomEditor(typeof(CustomRotation))]
    public class E : EditorPRO<CustomRotation>
    {
        private bool showContextInfo = false;
        private bool showMethods = false;

        //method parameters
        private Space P_Switch_Space;
        private Link P_Switch_Link;

        private Transform P_SwitchParent_Parent;

        protected override void DeclareProperties ()
        {
            AddProperty("transition");
            AddProperty("offset");
            AddProperty("link");

            AddProperty("space");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            target.RecordParent();
        }

        public override void OnInspectorGUI()
        {
            OnInspectorGUIPRO(() =>
            {
            target.expanded = EditorGUILayout.Foldout(target.expanded, "Expanded".bold(), true, EditorStyles.foldout.clone().richText());

                //<-----------ACTUAL FIELDS------------>
                if (target.expanded)
                {
                    EditorGUILayout.PropertyField(FindProperty("space"));

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Rotation", EditorStyles.boldLabel);
                    if (target.space == Space.Self)
                    {
                        target.parent = (Transform)EditorGUILayout.ObjectField("Parent", target.parent, typeof(Transform), true);
                    }
                    if (!(target.space == Space.Self && target.link == Link.Match))
                    {

                        target.value = Quaternion.Euler(EditorGUILayout.Vector3Field("Value", target.value.eulerAngles));
                    }

                    EditorGUILayout.Space();

                    if (target.space == Space.Self)
                    {
                        EditorGUILayout.LabelField("Local", EditorStyles.boldLabel);

                        EditorGUILayout.PropertyField(FindProperty("link"));

                        if (target.link == Link.Offset)
                        {
                            EditorGUILayout.PropertyField(FindProperty("offset"));
                        }
                    }

                    EditorGUILayout.Space();

                    if (target.space == Space.Self && target.link == Link.Offset)
                    {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("Transition", EditorStyles.boldLabel);

                        target.follow = EditorGUILayout.Toggle(string.Empty, target.follow);

                        EditorGUILayout.EndHorizontal();
                        if (target.follow)
                        {
                            EditorGUILayout.PropertyField(FindProperty("transition"));
                        }
                    }

                    EditorGUILayout.Space();

                    showContextInfo = EditorGUILayout.Foldout(showContextInfo, "Context Info".bold(), EditorStyles.foldout.clone().richText());
                    if (showContextInfo)
                    {
                        target.eulerAngles = EditorGUILayout.Vector3Field("Euler Angles", target.eulerAngles);
                        target.localEulerAngles = EditorGUILayout.Vector3Field("Local Euler Angles", target.localEulerAngles);

                        if (target.space == Space.Self && target.link == Link.Offset)
                        {
                            EditorGUILayout.Space();

                            target.eulerAnglesRaw = EditorGUILayout.Vector3Field("Euler Angles Raw", target.eulerAnglesRaw);
                            target.localEulerAnglesRaw = EditorGUILayout.Vector3Field("Local Euler Angles Raw", target.localEulerAnglesRaw);
                        }
                    }

                    Line();

                    if (EditorApplication.isPaused || !EditorApplication.isPlaying)
                    {
                        //EditorGUILayout.Space();

                        showMethods = EditorGUILayout.Foldout(showMethods, "Show Methods".bold(), EditorStyles.foldout.clone().richText());
                        if (target.applyInEditor)
                        {
                            GUI.enabled = false;
                        }
                        if (showMethods)
                        {
                            if (GUILayout.Button("Target to Current"))
                            {
                                Undo.RecordObject(target.gameObject, "Re-Oriented CustomRotation");

                                target.TargetToCurrent();
                            }

                            EditorGUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button("Switch", GUILayout.Width(EditorGUIUtility.labelWidth), GUILayout.ExpandHeight(true), GUILayout.Height(EditorGUIUtility.singleLineHeight * 2f)))
                                {
                                    Undo.RecordObject(target.gameObject, "Switched CustomRotation Space and/or Link");

                                    target.Switch(P_Switch_Space, P_Switch_Link);
                                }
                                EditorGUILayout.BeginVertical();
                                {
                                    P_Switch_Space = (Space)EditorGUILayout.EnumPopup("New Space", P_Switch_Space);
                                    P_Switch_Link = (Link)EditorGUILayout.EnumPopup("New Link", P_Switch_Link);
                                }
                                EditorGUILayout.EndVertical();
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button("Switch Parent", GUILayout.Width(EditorGUIUtility.labelWidth)))
                                {
                                    Undo.RecordObject(target.gameObject, "Switched CustomRotation Parent");

                                    target.SwitchParent(P_SwitchParent_Parent);
                                }
                                EditorGUILayout.BeginVertical();
                                {
                                    P_SwitchParent_Parent = (Transform)EditorGUILayout.ObjectField("New Parent", P_SwitchParent_Parent, typeof(Transform), true);
                                }
                                EditorGUILayout.EndVertical();
                            }
                            EditorGUILayout.EndHorizontal();

                            if (GUILayout.Button("Remove Offset", GUILayout.Width(EditorGUIUtility.labelWidth)))
                            {
                                if (EditorUtility.DisplayDialog(
                                    "Remove Offset?",
                                    "Are you sure you want to remove the offset of \"CustomRotation?\"",
                                    "Yes", "Cancel"))
                                {
                                    Undo.RecordObject(target.gameObject, "Removed CustomRotation Offset");

                                    target.RemoveOffset();
                                }
                            }
                        }
                        GUI.enabled = true;

                        EditorGUILayout.Space();

                        //Apply button
                        if (!target.applyInEditor)
                        {
                            if (EditorApplication.isPaused)
                            {
                                target.EditorApplyCheck();
                            }

                            if (GUILayout.Button(
                                "Apply in Editor".bold(),
                                EditorStyles.miniButton.clone().richText().fixedHeight(EditorGUIUtility.singleLineHeight * 1.5f)
                                ))
                            {
                                Undo.RecordObject(target.gameObject, "Applied CustomRotation Values in Editor");

                                target.SetPrevious();
                                target.RecordParent();

                                target.applyInEditor = true;
                            }
                        }
                        else
                        {
                            if (GUILayout.Button(
                                "Don't Apply in Editor".colour(Color.red).bold(),
                                EditorStyles.miniButton.clone().richText().fixedHeight(EditorGUIUtility.singleLineHeight * 1.5f)
                                ))
                            {
                                target.applyInEditor = false;
                            }

                            if (EditorApplication.isPaused)
                            {
                                target.EditorApplyCheck();
                            }
                        }
                    }
                }
            });
        }
    }
#endif
}
