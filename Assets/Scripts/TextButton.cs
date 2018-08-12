using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextButton : MonoBehaviour, IPointerClickHandler {

    public delegate void TextButtonClicked();

    public TextMeshPro text;

    public TextButtonClicked clickHandler;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Block " + gameObject.GetInstanceID() + " was clicked");
        if (clickHandler != null)
            clickHandler();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
