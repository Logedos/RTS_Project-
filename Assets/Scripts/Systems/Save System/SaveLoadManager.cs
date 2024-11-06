using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Systems.SaveSystem
{
    public sealed class SaveLoadManager : MonoBehaviour
    {
        [SerializeReference]
        private IGameRepository _repository;
        
        [SerializeReference]
        private List<ISaveLoader> _saveLoaders;
        
        [Button]
        public void Save()
        {
            for (int i = 0; i < _saveLoaders.Count; i++) 
                _saveLoaders[i].Save();
            
            _repository.SaveState();
        }
        
        [Button]
        public void Load()
        {
            _repository.LoadState();

            for (int i = 0; i < _saveLoaders.Count; i++)
                _saveLoaders[i].Load();
        }
    }
}