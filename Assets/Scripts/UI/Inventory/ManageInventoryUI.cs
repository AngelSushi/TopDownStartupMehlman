using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ManageInventoryUI : MonoBehaviour
    {

        [SerializeField] private GameObject inventory;
        [SerializeField] private Transform slotParent;
        [SerializeField] private Transform slotTeamParent;
        [SerializeField] private PlayerReference playerReference;
        private List<GameObject> _inventorySlots = new List<GameObject>();

        private List<PokemonObject> _showedPokemons;

        public List<PokemonObject> ShowedPokemons
        {
            get => _showedPokemons;
        }

        [Header("Data Fields")]
        
        [SerializeField]private Image pokemonSprite;
        [SerializeField] private TextMeshProUGUI pokemonName;
        [SerializeField] private TextMeshProUGUI pokemonIndex;

        [SerializeField] private TextMeshProUGUI hp;
        [SerializeField] private TextMeshProUGUI attack;
        [SerializeField] private TextMeshProUGUI defense;
        [SerializeField] private TextMeshProUGUI spAttack;
        [SerializeField] private TextMeshProUGUI spDef;
        [SerializeField] private TextMeshProUGUI speed;
        [SerializeField] private TextMeshProUGUI ability;

        private Transform[] _teamSlots;
        
        private void Awake()
        {
            if (playerReference.Instance != null)
            {
                SpawnPlayer(playerReference.Instance, playerReference.Instance);
            }
            else
            {
                playerReference.OnValueChanged += SpawnPlayer;
                
            }
            
            inventory.SetActive(false);
            
            for (int i = 0; i < slotParent.childCount; i++)
            {
                _inventorySlots.Add(slotParent.GetChild(i).gameObject);
            }

            _teamSlots = new Transform[slotTeamParent.childCount];
            
            for (int i = 0; i < slotTeamParent.childCount; i++)
            {
                _teamSlots[i] = slotTeamParent.transform.GetChild(i);
            }
            
        }
        private void SpawnPlayer(Entity sEntity,Entity oEntity)
        {
            if (sEntity != null && sEntity is Player)
            {
                Player p = (Player)sEntity;
                p.OnOpenInventory += OnOpenInventory;
                p.OnCloseInventory += OnCloseInventory;
            }
            else
            {
                if (oEntity != null && oEntity is Player)
                {
                    Player p = (Player)oEntity;
                    p.OnOpenInventory -= OnOpenInventory;
                    p.OnCloseInventory -= OnCloseInventory;
                }
            }
        }

        private void Update()
        {
            UpdateTeamUI();
        }


        private void OnOpenInventory(List<PokemonObject> ownedPokemons,List<PokemonObject> seenPokemons,Player player)
        {
            _showedPokemons = new List<PokemonObject>(ownedPokemons);
            
            // Faire en sorte que les 3 premiers de la liste ownedPokemons soit oujours les followers 

            for (int i = 0; i < player.Followers.Count; i++)
            {
                if (player.Followers[i] is PokemonEntity)
                {
                    PokemonEntity enemy = (PokemonEntity)player.Followers[i];
                    _showedPokemons.Remove(enemy.AttachedPokemon);
                    _showedPokemons.Insert(i,enemy.AttachedPokemon);
                }
            }
            
            

            inventory.SetActive(true);
            player.IsInInventory = true;
            for (int i = 0; i < _showedPokemons.Count; i++)
            {
                GameObject slot = new GameObject();
                Image image = slot.AddComponent<Image>();
                image.sprite = _showedPokemons[i].Sprite;
                slot.name = _showedPokemons[i].Name;
                ItemSlot itemSlot = slot.AddComponent<ItemSlot>();
                itemSlot.Manager = this;
                image.transform.parent = _inventorySlots[i].transform;
            }
        }
        
        
        private List<PokemonObject> OnCloseInventory(Player player)
        {
            inventory.SetActive(false);
            player.IsInInventory = false;

            foreach (GameObject slot in _inventorySlots)
            {
                for (int i = 0; i < slot.transform.childCount; i++)
                {
                    Destroy(slot.transform.GetChild(i).gameObject);
                }
            }

            List<PokemonObject> newFollowers = new List<PokemonObject>();

            for (int i = 0; i < _teamSlots.Length; i++)
            {
                PokemonObject pokemonObject = _showedPokemons.FirstOrDefault(pokemon => pokemon.Sprite == _teamSlots[i].GetChild(0).GetComponent<Image>().sprite);

                if (pokemonObject != null)
                {
                    newFollowers.Add(pokemonObject);
                }
            }
            
            return newFollowers;
        }
        
        private void UpdateTeamUI()
        {
            for (int i = 0; i < _teamSlots.Length; i++)
            {

                if (_inventorySlots[i].transform.childCount == 0)
                {
                    _teamSlots[i].GetChild(0).GetComponent<Image>().sprite = null;
                    _teamSlots[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = ""; 
                    continue;
                }
                
                PokemonObject targetPokemon = _showedPokemons.First(pokemon => pokemon.Name == _inventorySlots[i].transform.GetChild(0).name);

                if (targetPokemon != null)
                {
                    _teamSlots[i].GetChild(0).GetComponent<Image>().sprite = targetPokemon.Sprite;
                    _teamSlots[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = targetPokemon.Name;   
                }
            }    
        }
        public void UpdateValues(PokemonObject pokemonObject)
        {
            if (pokemonObject != null)
            {
                pokemonSprite.sprite = pokemonObject.Sprite;
                pokemonName.text = pokemonObject.Name;
                pokemonIndex.text = "No. " + pokemonObject.Data.id;
                hp.text = pokemonObject.Data.statbase.HP.ToString();
                attack.text = pokemonObject.Data.statbase.Attack.ToString();
                defense.text = pokemonObject.Data.statbase.Defense.ToString();
                spAttack.text = pokemonObject.Data.statbase.SpAttack.ToString();
                spDef.text = pokemonObject.Data.statbase.SpDefense.ToString();
                speed.text = pokemonObject.Data.statbase.Speed.ToString();
                ability.text = "";
            }
            else
            {
                pokemonSprite.sprite = null;
                pokemonName.text = "";
                pokemonIndex.text = "";
                hp.text = "";
                attack.text = "";
                defense.text = "";
                spAttack.text = "";
                spDef.text = "";
                speed.text = "";
                ability.text = "";
            }
        }

    }
}
