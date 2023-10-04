using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GrassAttack : MonoBehaviour
    {

        [SerializeField] private Sprite ropeSprite;
        [SerializeField] private float maxGrow;
        private bool _startGrowing;

        private float _startGrowX;

        private float _t;

        private GameObject _rope;


        void Start()
        {
            _rope = new GameObject("Rope");
            _rope.transform.parent = transform;
            SpriteRenderer spriteRenderer = _rope.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = ropeSprite;
            _rope.transform.localPosition = Vector3.zero;
            _rope.transform.localScale = new Vector3(1, 0.2f, 1);
            _startGrowX = transform.localScale.x;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _startGrowing = true;
            }            

            if(_startGrowing)
            {
                _rope.transform.localScale = new Vector3(Mathf.Lerp(_startGrowX,maxGrow, _t), _rope.transform.localScale.y, _rope.transform.localScale.z);
                _t += Time.deltaTime;
            }
        }

    }
}
