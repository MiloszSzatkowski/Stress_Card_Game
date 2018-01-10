using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

	public Vector2 dragOffset = new Vector2(0f,0f);
	public Vector2 startPosition = new Vector2(0f,0f);
	public Deck _Deck;
	public bool isDraggable = true;
	public Slot_Watcher __Slot_Watcher;

	void Start (){

		startPosition = this.transform.position;
	}

	public void OnBeginDrag (PointerEventData eventData){
			this.GetComponent<UnityEngine.UI.Image>().color= new Color (255,255,0);
			dragOffset = eventData.position - (Vector2)this.transform.position;
			this.GetComponent<RectTransform>().SetAsLastSibling();
	}

	public void OnDrag (PointerEventData eventData){
			this.transform.position = eventData.position - dragOffset;
	}

	public void OnEndDrag (PointerEventData eventData){
		this.GetComponent<UnityEngine.UI.Image>().color= new Color (1,1,1,1);
			if(this.transform.position.y < 110){
					this.transform.position = startPosition;
			} else if (this.transform.position.x < Screen.width / 2){
				//Card is dropped on LEFT STACK
				this.transform.position =  Vector2.Lerp (
				this.transform.position,
				(Vector2)GameObject.Find("LeftCard").transform.position + new Vector2(Random.Range(0,3),Random.Range(0,3)), 1);
				_Deck.stosLewa.Add(this.GetComponent<UnityEngine.UI.Image>().sprite);
				isDraggable = false;
				//EMPTY slot for other cards
				checkNamesForSlot();
			} else if (this.transform.position.x > Screen.width / 2){
				//Card is dropped on RIGHT STACK
				this.transform.position =  Vector2.Lerp (
				this.transform.position,
				(Vector2)GameObject.Find("RightCard").transform.position + new Vector2(Random.Range(0,3),Random.Range(0,3)), 1);
				_Deck.stosPrawa.Add(this.GetComponent<UnityEngine.UI.Image>().sprite);
				isDraggable = false;
				//EMPTY slot for other cards
				checkNamesForSlot();
			} else {
				this.transform.position = startPosition;
			}
}

	public void checkNamesForSlot(){
		if (this.name=="slot_1"){
			__Slot_Watcher.slot1 = false;
		} else if (this.name=="slot_2"){
			__Slot_Watcher.slot2 = false;
		} else if (this.name=="slot_3"){
			__Slot_Watcher.slot3 = false;
		} else if (this.name=="slot_4"){
			__Slot_Watcher.slot4 = false;
		} else if (this.name=="slot_5"){
			__Slot_Watcher.slot5 = false;
		} else if (this.name=="slot_6"){
			__Slot_Watcher.slot6 = false;
		} else if (this.name=="slot_7"){
			__Slot_Watcher.slot7 = false;
		} else if (this.name=="slot_8"){
			__Slot_Watcher.slot8 = false;
		}
	}

}
