using UnityEngine;

namespace Core
{
    internal class MonoBehaviourUtils
    {
        public static bool SafeGetGoWithTag(string tag, out GameObject go)
        {
            try
            {
                go = GameObject.FindWithTag(tag);
                if (go != null) return true;
                throw new UnassignedReferenceException();
            }
            catch
            {
                Debug.LogError($"[{nameof(MonoBehaviourUtils)}] Failed to find Game object with tag {tag}");
                go = null;
                return false;
            }
        }
    }
}