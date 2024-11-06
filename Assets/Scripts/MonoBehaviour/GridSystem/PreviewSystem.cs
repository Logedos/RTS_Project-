using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
   [SerializeField] private float previewOffset = 0.06f;
   [SerializeField] private GameObject cellIndicator;
   private GameObject previewObject;
   [SerializeField] private Material previewMaterialsPrefab;
   private Material previewMaterialsInstance;

   private Renderer cellIndicatorRenderer;

   
   private void Start()
   {
      previewMaterialsInstance = new Material(previewMaterialsPrefab);
      cellIndicator.SetActive(false);
      cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
   }

   public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
   {
      prefab.transform.GetChild(0).gameObject.SetActive(true);
      
      previewObject = Instantiate(prefab);
      PreparePreview(previewObject);
      PrepareCursor(size);
      cellIndicator.SetActive(true);
   }

   private void PrepareCursor(Vector2Int size)
   {
      if (size.x > 0 || size.y > 0)
      {
         cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
         cellIndicatorRenderer.material.mainTextureScale = size;
      }
   }

   private void PreparePreview(GameObject previewObject)
   {
      Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
      foreach (Renderer renderer in renderers)
      {
         Material[] materials = renderer.materials;
         for (int i = 0; i < materials.Length; i++)
         {
            materials[i] = previewMaterialsInstance;
         }

         renderer.materials = materials;
      }
   }

   public void StopShowingPreview(GameObject prefab)
   {
      if (prefab != null)
         prefab.transform.GetChild(0).gameObject.SetActive(false);

      cellIndicator.SetActive(false);
      Destroy(previewObject);
   }

   public void UpdatePosition(Vector3 position,Vector2Int size, bool validity)
   {
      MovePreview(position);
      MoveCoursor(position, size);
      ApplyFeedBack(validity);
   }

   private void ApplyFeedBack(bool validity)
   {
      Color colourForPreview = validity ? Color.green : Color.red;
      Color colourForIndicator = Color.white;
      colourForPreview.a = 0.5f;
      colourForIndicator.a = 0.2f;
      cellIndicatorRenderer.material.color = colourForIndicator;
      previewMaterialsInstance.color = colourForPreview;
   }

   private void MoveCoursor(Vector3 position, Vector2Int size)
   {
      Vector3 offset = new Vector3();
      if (size == new Vector2Int(3,3))
      {
         offset = new Vector3(2, 0, 2);
      }
      else if (size == new Vector2Int(2,2))
      {
         offset = new Vector3(1, 0, 1);
      }
      else if (size == new Vector2Int(3,2))
      {
         offset = new Vector3(2, 0, 1);
      }
      cellIndicator.transform.position = position + offset;
   }

   private void MovePreview(Vector3 position)
   { 
      previewObject.transform.position = new Vector3(position.x, position.y + previewOffset, position.z);
   }
}
