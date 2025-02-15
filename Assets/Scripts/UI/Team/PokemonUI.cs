using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PokemonUI : MonoBehaviour
    {
        [SerializeField] private Image pokemonSprite;
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private GameObject pokemonTypeParent;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private TextMeshProUGUI healthText;

        public Image PokemonSprite
        {
            get => pokemonSprite;
            set => pokemonSprite = value;
        }

        public TextMeshProUGUI PokemonName
        {
            get => pokemonName;
            set => pokemonName = value;
        }

        public GameObject PokemonTypeParent
        {
            get => pokemonTypeParent;
            set => pokemonTypeParent = value;
        }

        public HealthBar HealthBar
        {
            get => healthBar;
            set => healthBar = value;
        }

        public TextMeshProUGUI HealthText
        {
            get => healthText;
            set => healthText = value;
        }
    }
}