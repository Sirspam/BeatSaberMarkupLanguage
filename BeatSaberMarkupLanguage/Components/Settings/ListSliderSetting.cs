﻿using BeatSaberMarkupLanguage.Parser;
using HMUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BeatSaberMarkupLanguage.Components.Settings
{
    public class ListSliderSetting : GenericSliderSetting
    {
        public BSMLAction formatter;
        public BSMLAction onChange;
        public BSMLValue associatedValue;
        public bool updateOnChange = false;
        public List<object> values;

        public object Value
        {
            get
            {
                return values[(int)(slider.value*(values.Count-1))];
            }
            set
            {
                slider.value = values.IndexOf(value) * 1f / values.Count;
                text.text = TextForValue(Value);
            }
        }

        public void Setup()
        {
            slider.minValue = 0;
            slider.maxValue = 1;
            text = slider.GetComponentInChildren<TextMeshProUGUI>();
            slider.numberOfSteps = values.Count;
            ReceiveValue();
            slider.valueDidChangeEvent += OnChange;
            StartCoroutine(SetInitialText());
        }
        private void OnEnable()
        {
            StartCoroutine(SetInitialText());
        }
        IEnumerator SetInitialText()//I don't really like this but for some reason I can't get the inital starting text any other quick way and this works perfectly fine
        {
            yield return new WaitForFixedUpdate();
            text.text = TextForValue(Value);
            yield return new WaitForSeconds(0.1f);//if the first one is too fast, don't yell at me pls
            text.text = TextForValue(Value);
        }

        private void OnChange(TextSlider _, float val)
        {
            text.text = TextForValue(Value);
            onChange?.Invoke(Value);
            if (updateOnChange)
            {
                ApplyValue();
            }
        }
        public void ApplyValue()
        {
            if (associatedValue != null)
                    associatedValue.SetValue(Value);
        }
        public void ReceiveValue()
        {
            if (associatedValue != null)
                Value = associatedValue.GetValue();
        }

        protected string TextForValue(object value)
        {
            return formatter == null ? value.ToString() : (formatter.Invoke(value) as string);
        }
    }
}