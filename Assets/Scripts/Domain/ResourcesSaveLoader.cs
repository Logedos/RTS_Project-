using System.Collections.Generic;
using Systems.SaveSystem;
using UnityEngine;

namespace Domain
{
    /*public sealed class ResourcesSaveLoader : MonoBehaviour, ISaveLoader
    {
        [SerializeField] 
        private ResourcesManager _resourcesManager;
        
        [SerializeField]
        private IGameRepository _repository;

        public void Save()
        {
            List<int> resourcesPrice = new();
            
            Resources[] resources = _resourcesManager.GetResources();

            for (int i = 0; i < resources.Length; i++) 
                resourcesPrice.Add(resources[i].Price);

            ResourcesData resourcesData = new ResourcesData
            {
                ReourcesPrice = resourcesPrice
            };
            
            _repository.SetData(resourcesData);
        }

        public void Load()
        {
            Resources[] resources = _resourcesManager.GetResources();
            
            ResourcesData resourcesData = _repository.GetData<ResourcesData>();
            List<int> resourcesPrice = resourcesData.ReourcesPrice;

            for (int i = 0; i < resources.Length; i++)
                resources[i].Price = resourcesPrice[i];
        }
    }*/
}