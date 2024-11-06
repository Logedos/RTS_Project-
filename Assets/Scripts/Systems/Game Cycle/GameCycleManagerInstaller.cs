using System.Collections.Generic;
using UnityEngine;

namespace Systems.GameCycle
{
    [DefaultExecutionOrder(-5000)]
    internal sealed class GameCycleManagerInstaller : MonoBehaviour
    {
        [SerializeField]
        private GameCycleManager _gameCycleManager;

        private void Awake()
        {
            MonoBehaviour[] components = FindObjectsOfType<MonoBehaviour>(true);
            
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] is IGameListener gameListener)
                    _gameCycleManager.AddListener(gameListener);
            }
        }
    }
}