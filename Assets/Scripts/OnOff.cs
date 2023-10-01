using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class OnOff : MonoBehaviour
    {

        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;

        private SpriteRenderer _sprite;

        public Sprite OnSprite
        {
            get => onSprite;
            set => onSprite = value;
        }

        public Sprite OffSprite
        {
            get => offSprite;
            set => offSprite = value;
        }

        private void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void Actualize(Room currentRoom,Vector2 position,bool value)
        {
            BlocOnOff targetBloc = (BlocOnOff) currentRoom.Blocs.FirstOrDefault(bloc => bloc.LocalPosition == position);
            targetBloc.IsOn = value;
            Debug.Log("hit bloc " + targetBloc);
        }

        public void TurnSprite()
        {
            Debug.Log("turn sprite " + (_sprite.sprite == onSprite ? offSprite.name : onSprite.name));
            _sprite.sprite = _sprite.sprite == onSprite ? offSprite : onSprite;
        }
    }
}
