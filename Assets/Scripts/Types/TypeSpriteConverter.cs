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
                case "normal":
                    return typesSprite[0];
                
                case "fighting":
                    return typesSprite[1];
                
                case "flying":
                    return typesSprite[2];
                
                case "poison":
                    return typesSprite[3];
                
                case "ground":
                    return typesSprite[4];
                
                case "rock":
                    return typesSprite[5];
                
                case "ghost":
                    return typesSprite[6];
                
                case "steel":
                    return typesSprite[7];
                
                case "fire":
                    return typesSprite[8];
                
                case "water":
                    return typesSprite[9];
                
                case "grass":
                    return typesSprite[10];
                
                case "electric":
                    return typesSprite[11];
                
                case "psychic":
                    return typesSprite[12];
                
                case "ice":
                    return typesSprite[13];
                
                case "dragon":
                    return typesSprite[14];
                
                case "dark":
                    return typesSprite[15];
                
                case "fairy":
                    return typesSprite[16];

                default:
                    return typesSprite[17];
            }
        }

        public Sprite GetTypeSmallSpriteByName(string name)
        {
            switch (name.ToLower())
            {
                case "normal":
                    return typesSmallSprite[0];

                case "fighting":
                    return typesSmallSprite[1];

                case "flying":
                    return typesSmallSprite[2];

                case "poison":
                    return typesSmallSprite[3];

                case "ground":
                    return typesSmallSprite[4];

                case "rock":
                    return typesSmallSprite[5];

                case "ghost":
                    return typesSmallSprite[6];

                case "steel":
                    return typesSmallSprite[7];

                case "fire":
                    return typesSmallSprite[8];

                case "water":
                    return typesSmallSprite[9];

                case "grass":
                    return typesSmallSprite[10];

                case "electric":
                    return typesSmallSprite[11];

                case "psychic":
                    return typesSmallSprite[12];

                case "ice":
                    return typesSmallSprite[13];

                case "dragon":
                    return typesSmallSprite[14];

                case "dark":
                    return typesSmallSprite[15];

                case "fairy":
                    return typesSmallSprite[16];

                default:
                    return typesSmallSprite[17];
            }
        }
    }
}
