using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Game
{

    public enum MecanismType
    {
        BUTTON,
        FIRE,
        BLOC,
        ICE
    }
    
    public class GenerateMecanismRoom : MonoBehaviour
    {

        [SerializeField, BoxGroup("Dependencies")] private RoomManager roomManager;
        
        [SerializeField] private GameObject torch;
        [SerializeField] private GameObject button;
        [SerializeField] private GameObject stele;
        [SerializeField] private GameObject bloc;
        [SerializeField] private GameObject iceBloc;
        
        private void Start() => roomManager.OnGenerateMecanism += OnGenerateMecanism;
        private void OnDestroy() =>roomManager.OnGenerateMecanism -= OnGenerateMecanism;

        private Mecanism OnGenerateMecanism(Room room)
        {
            if (!room.CanHaveMecanism)
            {
                return null;
            }

            MecanismType[] allTypes = room.AllMecanisms.ToArray();
            MecanismType chooseTypeMecanism = (MecanismType)allTypes.GetValue(Random.Range(0, allTypes.Length));
            
            if (chooseTypeMecanism == MecanismType.ICE)
            {
                GenerateIce(room);
            }
            else
            {
                List<BlocOnOff> mecanismBlocs = room.Blocs.OfType<BlocOnOff>().ToList();

                GameObject objectToInstantiate;

                switch (chooseTypeMecanism)
                {
                    case MecanismType.BUTTON:
                        objectToInstantiate = button;
                        break;

                    case MecanismType.FIRE:
                        objectToInstantiate = torch;
                        break;

                    case MecanismType.BLOC:
                        objectToInstantiate = stele;
                        GeneratePushableBlocs(room,mecanismBlocs);
                        break;

                    default:
                        objectToInstantiate = torch;
                        break;
                }
                
                foreach (Bloc bloc in mecanismBlocs)
                {
                    GameObject instance = Instantiate(objectToInstantiate, room.RoomGO.transform);
                    instance.SetActive(true);
                    instance.transform.localPosition = bloc.LocalPosition;
                }

                OnOffMecanism mecanism = new OnOffMecanism(mecanismBlocs);
                return mecanism;
            } 
            
            return null;
        }

        private void GenerateIce(Room room)
        {
            int[,] shape = new int[room.PatternRef.height - 4,room.PatternRef.width - 8]; // - 4 to disable wall part of texture ; // - 8 to disable wall part of texture + let space to player to walk

            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    shape[i, j] = Random.Range(0, 3);
                }
            }
                
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] != 1)
                    {
                        GameObject iceInstance = Instantiate(iceBloc, room.RoomGO.transform);
                        iceInstance.SetActive(true);
                        iceInstance.transform.localPosition = room.GetBlocAt(new Vector2Int(i + 2,j + 4)).LocalPosition;
                    }
                }
            }
        }

        private void GeneratePushableBlocs(Room room,List<BlocOnOff> mecanismBlocs)
        {
            List<Bloc> blocs = new List<Bloc>(room.Blocs);
            blocs.RemoveAll(bloc => bloc is BlocOnOff || bloc is BlocVoid || bloc is BlocPokemon); // AJouter les autres blocs si besoin 
            List<Bloc> spawnedAtBloc = new List<Bloc>();
                        
            for (int i = 0; i < mecanismBlocs.Count; i++)
            {
                int randomBloc = Random.Range(0, blocs.Count);
                Bloc targetBloc = blocs[randomBloc];

                while (spawnedAtBloc.Contains(targetBloc))
                {
                    randomBloc = Random.Range(0, blocs.Count);
                    targetBloc = blocs[randomBloc];
                }

                GameObject blocInstance = Instantiate(bloc, room.RoomGO.transform);
                blocInstance.SetActive(true);

                Vector3 localPosition = targetBloc.LocalPosition;
                localPosition.z = -1;
                
                blocInstance.transform.localPosition = localPosition;
                spawnedAtBloc.Add(targetBloc);
                            

            }
        }
    }
}
