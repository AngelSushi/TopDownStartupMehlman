using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TypeSpriteConverter : MonoBehaviour
    {
        [SerializeField] private List<Sprite> typesSprite;
        [SerializeField] private List<Sprite> typesSmallSprite;

        public Sprite GetTypeSpriteByName(string name)
        {
            switch (name.ToLower())
            {
                case "unknown":
                    return typesSprite[0];

                case "normal":
                    return typesSprite[1];
                
                case "fighting":
                    return typesSprite[2];
                
                case "flying":
                    return typesSprite[3];
                
                case "poison":
                    return typesSprite[4];
                
                case "ground":
                    return typesSprite[5];
                
                case "rock":
                    return typesSprite[6];
                
                case "ghost":
                    return typesSprite[7];
                
                case "steel":
                    return typesSprite[8];
                
                case "fire":
                    return typesSprite[9];
                
                case "water":
                    return typesSprite[10];
                
                case "grass":
                    return typesSprite[11];
                
                case "electric":
                    return typesSprite[12];
                
                case "psychic":
                    return typesSprite[13];
                
                case "ice":
                    return typesSprite[14];
                
                case "dragon":
                    return typesSprite[15];
                
                case "dark":
                    return typesSprite[16];
                
                case "fairy":
                    return typesSprite[17];

                default:
                    return typesSprite[18];
            }
        }

        public Sprite GetTypeSmallSpriteByName(string name)
        {
            switch (name.ToLower())
            {
                case "unknown":
                    return typesSmallSprite[0];

                case "normal":
                    return typesSmallSprite[1];

                case "fighting":
                    return typesSmallSprite[2];

                case "flying":
                    return typesSmallSprite[3];

                case "poison":
                    return typesSmallSprite[4];

                case "ground":
                    return typesSmallSprite[5];

                case "rock":
                    return typesSmallSprite[6];

                case "ghost":
                    return typesSmallSprite[7];

                case "steel":
                    return typesSmallSprite[8];

                case "fire":
                    return typesSmallSprite[9];

                case "water":
                    return typesSmallSprite[10];

                case "grass":
                    return typesSmallSprite[11];

                case "electric":
                    return typesSmallSprite[12];

                case "psychic":
                    return typesSmallSprite[13];

                case "ice":
                    return typesSmallSprite[14];

                case "dragon":
                    return typesSmallSprite[15];

                case "dark":
                    return typesSmallSprite[16];

                case "fairy":
                    return typesSmallSprite[17];

                default:
                    return typesSmallSprite[18];
            }
        }
    }
}
