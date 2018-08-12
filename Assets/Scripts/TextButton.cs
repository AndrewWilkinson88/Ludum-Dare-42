using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextButton : MonoBehaviour, IPointerClickHandler {

    public delegate void TextButtonClicked(string s);

    public TextMeshPro text;

    public TextButtonClicked clickHandler;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickHandler != null)
            clickHandler(text.text);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
