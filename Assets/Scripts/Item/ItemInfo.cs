using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Item
{
    public class ItemInfo : ScriptableObject
    {
        [Title("Icon")]
        public Texture2D icon;

        [Title("Effect")] 
        public UnityEvent onConsumeEffect;
    }
}
