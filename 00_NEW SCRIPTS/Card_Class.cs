using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card_Class : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

	public Initialize _In;

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

	//starting sprites for switching
	public Sprite onTopSprite;
	public Sprite onBackSprite;

	public void OnBeginDrag (PointerEventData eventData){
		dragOffset = eventData.position - (Vector2)this.transform.position;
	}

	public void OnDrag (PointerEventData eventData){
		if (checkIfDraggable()) {
			this.transform.position = eventData.position - dragOffset;
			iTween.PunchRotation (gameObject, new Vector3 (80f,1,1), 2);
		} else {
			Debug.Log("You can't drag this card.");
		}
	}

	public void OnEndDrag (PointerEventData eventData) {
		if (gameObject.transform.position.y > Screen.height/3 && gameObject.transform.position.x > Screen.width/2) {
			_In.moveThis(gameObject, GameObject.Find("Left_Stack"),_In.rightStack);
		} else if (gameObject.transform.position.y > Screen.height/3 && gameObject.transform.position.x < Screen.width/2) {
			_In.moveThis(gameObject, GameObject.Find("Left_Stack"),_In.leftStack);
		} else {
			iTween.MoveTo(gameObject,startPosition,1);
		}
	}

	public bool checkIfDraggable () {
			if (gameObject.transform.parent.gameObject == GameObject.Find("1_Table") ||
					gameObject.transform.parent.gameObject == GameObject.Find("2_Table")) {
				return true;
			} else {
				return false;
			}
		//bool end
	}

}
