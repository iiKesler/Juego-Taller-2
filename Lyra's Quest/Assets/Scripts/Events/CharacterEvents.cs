using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public abstract class CharacterEvents
    {
        public static UnityAction<GameObject, int> characterDamaged;
        public static UnityAction<GameObject, int> characterHealed;
    }
}