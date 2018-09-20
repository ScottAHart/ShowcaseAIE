using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Score holder contains a variable and a list of fields that will update when variable is changed.
/// </summary>
public class ScoreHolder : MonoBehaviour {
    private int _score;//Score itself
    
    public CUI.CUINumber[] _fieldsToUpdate; //Fields that this score is tied to. 

    /// <summary>
    /// Add score to current score. 
    /// Updates all UI assigned. 
    /// </summary>
    /// <param name="amount">Change amount.</param>
    public void AddScore(int amount)
    {
        _score += amount;
        foreach (CUI.CUINumber num in _fieldsToUpdate)
            num.SetValue(_score);
    }

}
