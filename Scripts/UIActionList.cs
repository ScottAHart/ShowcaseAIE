using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Sets up multiple UIActions to be displayed dynamically depending on how many are there
/// </summary>
public class UIActionList : MonoBehaviour {

    public List<Action> actions = new List<Action>();
    public UIAction uiPrefab;

    Dictionary<Action, UIAction> uiActions = new Dictionary<Action, UIAction>();

	// Use this for initialization
	void Start () {
        uiPrefab.gameObject.SetActive(false);
        UpdateActions();
	}

    public void SetActions()
    {
        if(PlayerControl.Attached == null || PlayerControl.Attached.GetComponent<IActions>() == null)
        {
            actions = new List<Action>();

            ToolTip.Instance.Hide();
            UpdateActions();
        }
        else
        {
            actions = PlayerControl.Attached.GetComponent<IActions>().GetActions();
            UpdateActions();
        }
    }

    void UpdateActions()
    {    
        // go through the list and remove any actions no longer in our data
        List<Action> deathRow = new List<Action>();
        foreach (KeyValuePair<Action, UIAction> pair in uiActions)
        {
            if (actions.Contains(pair.Key) == false)
            {
                deathRow.Add(pair.Key);
            }
        }
        // remove them from the list
        foreach (Action a in deathRow)
        {
            Destroy(uiActions[a].gameObject);
            uiActions.Remove(a);
        }


        // go through our actions and create instances from the prefab
        foreach (Action a in actions)
        {
            if (uiActions.ContainsKey(a) == false)
            {
                GameObject go = Instantiate(uiPrefab.gameObject, transform);
                go.SetActive(true);
                uiActions[a] = go.GetComponent<UIAction>();
                uiActions[a].SetAction(a);
            }
        }

        StartCoroutine(UpdateSize());
    }

    IEnumerator UpdateSize()
    {
        yield return new WaitForEndOfFrame();        
        RectTransform rect = GetComponent<RectTransform>();
        LayoutGroup layout = GetComponent<LayoutGroup>();
        if (layout)
        {
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, layout.preferredWidth);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, layout.preferredHeight);
        }

        yield return null;
	}
}
