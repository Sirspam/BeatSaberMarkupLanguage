﻿using BeatSaberMarkupLanguage.Components.Settings;
using Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeatSaberMarkupLanguage.Tags.Settings
{
    public class SliderSettingTag : GenericSliderSettingTag<SliderSetting>
    {
        public override string[] Aliases => new[] { "slider-setting" };
    }
}
