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
            itemSlot.OriginParent = transform;
        }
    }
}
