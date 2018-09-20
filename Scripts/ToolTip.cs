using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Tooltip code modified to use a panel instead of an image background
/// 
/// </summary>
public interface IToolTip
{
    string GetInfo();
}
public class ToolTip : MonoBehaviour {

    GameObject _toolTip;
    public GameObject _panel;//set this 
    Text _text;
    Canvas _canvas;
    Vector2 _baseSize;
    IToolTip _currentHelper;

    public static ToolTip Instance;

	// Use this for initialization
	void Start () {
        _toolTip = transform.GetChild(0).gameObject;
        
        _text = GetComponentInChildren<Text>();
        _canvas = GetComponent<Canvas>();
        Instance = this;

        _baseSize = _text.rectTransform.rect.size;

        Hide();
    }
    //Show tip 
    public void Show(Vector3 position, string msg, IToolTip helper)
    {
        _currentHelper = helper;

        SetText(msg);

        _toolTip.SetActive(true);
        _toolTip.transform.position = position;

        // position it based on quadrant of the screen
        RectTransform rect = _panel.GetComponent<RectTransform>();
        rect.pivot = new Vector2(position.x < _canvas.pixelRect.width / 2 ? 0 : 1, position.y < _canvas.pixelRect.height / 2 ? 0 : 1);
    }
    public void Hide()
    {
        _toolTip.SetActive(false);
    }
    void SetText(string msg)
    {
        // trim whitespace at the end
        while (msg.EndsWith("\n") || msg.EndsWith(" "))
            msg = msg.Remove(msg.Length - 1);

        if (_text.text != msg)
        {
            _text.text = msg;

            // size it to hold the text we've given it
            TextGenerator textGen = new TextGenerator();
            TextGenerationSettings generationSettings = _text.GetGenerationSettings(_baseSize);
            float width = textGen.GetPreferredWidth(msg, generationSettings);
            float height = textGen.GetPreferredHeight(msg, generationSettings);
            RectTransform rect = _panel.GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width + 20);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height + 20);
            _text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            _text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }
    }
}
