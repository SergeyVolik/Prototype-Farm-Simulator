using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class ResourceUIItem : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI itemText;
        public Image spriteImage;
        public void SetValue(int value)
        {
            itemText.text = value.ToString();
        }
        public void SetSprite(Sprite sprite, Color color)
        {
            spriteImage.color = color;
            spriteImage.sprite = sprite;
        }
    }
}