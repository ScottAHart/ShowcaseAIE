using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI action sets up UI element to display an action
/// Set relevant Ui elements with data
/// </summary>
public class UIAction : MonoBehaviour, IToolTip {

    public Action action;
    public Image icon;
    public Text nameTag;
    public Text descTag;
    public Button button;

    void Start()
    {
        // hook up button
        button.onClick.AddListener(OnClicked);
    }

    public void OnClicked()
    {
        action.Apply(PlayerControl.Attached);
    }

    public void SetAction(Action a)
    {
        action = a;
        if (icon)
            icon.sprite = action._sprite;
        if (nameTag)
            nameTag.text = action._name;
        if (descTag)
            descTag.text = action._description;
    }

    string IToolTip.GetInfo()
    {
        return "<b>" + action._name + "</b>\n" + action._description;
    }
}
