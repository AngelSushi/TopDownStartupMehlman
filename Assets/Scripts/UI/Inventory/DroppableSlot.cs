using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class DroppableSlot : MonoBehaviour,IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {        
            GameObject droppedItem = eventData.pointerDrag;

            ItemSlot itemSlot = droppedItem.GetComponent<ItemSlot>();
            if (transform.childCount == 1)
            {
                Transform pokemonToSwitch = transform.GetChild(0);
                pokemonToSwitch.transform.parent = itemSlot.OriginParent.transform;
            }

            itemSlot.OriginParent = transform;
        }
    }
}
