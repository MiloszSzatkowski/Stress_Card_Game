using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card_Class : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

	//suits: of Hearts, Diamonds, Clubs, Spades
	public List<string> suits;
	public string card_suit;

	//values : 2,3,4,5,6,7,8,9,10,Jack,Queen,King,Ace,Joker
	public List<string> values;
	public string card_value;

	//containers - 1_Deck 2_Deck 1_Table 2_Table Left_Stack Right_Stack
	public List<GameObject> containers;
	public GameObject container;

	public Vector2 dragOffset = new Vector2(0f,0f);
	public Vector2 startPosition = new Vector2(0f,0f);

	public void OnBeginDrag (PointerEventData eventData){
			dragOffset = eventData.position - (Vector2)this.transform.position;
	}

	public void OnDrag (PointerEventData eventData){
			this.transform.position = eventData.position - dragOffset;
	}

	public void OnEndDrag (PointerEventData eventData) {

	}

}
