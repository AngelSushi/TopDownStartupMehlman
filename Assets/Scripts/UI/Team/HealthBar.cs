using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HealthBar : MonoBehaviour
    {

        private Slider _slider;

        private TextMeshProUGUI _healthText;
        private Health _health;

        public Health Health
        {
            get => _health;
            set => _health = value;
        }

        public TextMeshProUGUI HealthText
        {
            get => _healthText;
            set => _healthText = value;
        }

        private void Start() => _slider = GetComponent<Slider>();
        

        private void Update()
        {
            float t = (float)_health.CurrentHealth / _health.MaxHealth;
            _slider.value = t;
            HealthText.text = _health.CurrentHealth + "/" + _health.MaxHealth;
        }
    }
}
