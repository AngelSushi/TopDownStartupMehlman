using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class ItemSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
    {

       private ManageInventoryUI _manager;

       public ManageInventoryUI Manager
       {
           get => _manager;
           set => _manager = value;
       }

        private Transform _originParent;
        private Image _image;

        public Transform OriginParent
        {
            set => _originParent = value;
        }

        private void Start() => _image = GetComponent<Image>();

        public void OnPointerEnter(PointerEventData eventData) => _manager.UpdateValues(_manager.ShowedPokemons.FirstOrDefault(pokemon => pokemon.Sprite == transform.GetComponent<Image>().sprite));
        public void OnPointerExit(PointerEventData eventData) => _manager.UpdateValues(null);
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _originParent = transform.parent;
            transform.SetParent(transform.root);
            _image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData) => transform.position = Input.mousePosition;
        
        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(_originParent);
            _image.raycastTarget = true;
        }
        
    }
}
