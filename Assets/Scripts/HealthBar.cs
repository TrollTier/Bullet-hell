using Assets.Scripts.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public enum HealthBarPositions
    {
        Bottom, 
        Right
    }

    public class HealthBar : MonoBehaviour
    {
        private void Start()
        {
            health = GetComponent<Health>();
            sprite = GetComponent<SpriteRenderer>();
            
            if (healthTexture == null)
            {
                healthTexture = new Texture2D(1, 1);
                healthTexture.SetPixel(0, 0, Color.red);
                healthTexture.Apply();
            }

            HealthBarGui.healthBars.Add(this);
        }

        private void OnDestroy()
        {
            HealthBarGui.healthBars.Remove(this);
        }

        public HealthBarPositions position = HealthBarPositions.Bottom;
        private static Texture2D healthTexture;
        private SpriteRenderer sprite;
        private Health health;

        private Rect healthRect = new Rect();
        private Rect emptyRect = new Rect();
        public void Draw()
        {
            if (!sprite.isVisible) return;

            switch (position)
            {
                case HealthBarPositions.Bottom:
                    SetRectanglesAtBottom();
                    break;
                case HealthBarPositions.Right:
                    SetRectanglesAtRight();
                    break;
            }

            GUI.DrawTexture(healthRect, healthTexture, ScaleMode.StretchToFill);
            GUI.DrawTexture(emptyRect, Texture2D.whiteTexture, ScaleMode.StretchToFill);
        }

        private void SetRectanglesAtBottom()
        {
            var screenPos = Camera.main.WorldToScreenPoint(health.transform.position);
            //Assuming, the pivot point is in the center.
            var x = screenPos.x - sprite.sprite.pivot.x + (sprite.sprite.rect.width * 0.25f);
            var y = Screen.height - screenPos.y - sprite.sprite.pivot.y + sprite.sprite.rect.height + 5;

            var percentFactor = health.GetCurrentHp() / health.hpMax;
            var width = sprite.sprite.rect.width / 2;
            var healthWidth = width * percentFactor;

            healthRect.x = x;
            healthRect.y = y;
            healthRect.width = healthWidth;
            healthRect.height = 5;

            emptyRect.x = x + healthWidth;
            emptyRect.y = y;
            emptyRect.width = width - healthWidth;
            emptyRect.height = 5;
        }

        private void SetRectanglesAtRight()
        {
            var screenPos = Camera.main.WorldToScreenPoint(health.transform.position);
            //Assuming, the pivot point is in the center.
            var x = screenPos.x - sprite.sprite.pivot.x + (sprite.sprite.rect.width + 5);
            var y = Screen.height - screenPos.y - sprite.sprite.pivot.y + (sprite.sprite.rect.height * 0.25f);

            var percentFactor = health.GetCurrentHp() / health.hpMax;
            var height = sprite.sprite.rect.height / 2;
            var healthHeight = height * percentFactor;

            healthRect.x = x;
            healthRect.y = y;
            healthRect.width = 5;
            healthRect.height = healthHeight;

            emptyRect.x = x;
            emptyRect.y = y + healthHeight;
            emptyRect.width = 5;
            emptyRect.height = height - healthHeight;
        }
    }
}
