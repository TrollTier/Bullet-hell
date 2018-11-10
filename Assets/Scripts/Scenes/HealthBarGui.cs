using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class HealthBarGui : MonoBehaviour
    {
        public static List<HealthBar> healthBars = new List<HealthBar>(100);

        private void OnGUI()
        {
            if (healthBars.Count == 0) return;

            foreach(var bar in healthBars)
            {
                bar.Draw();
            }
        }

    }
}
