using System;
using System.Collections.Generic;
using System.IO;
using Stages;
using UnityEditor;
using UnityEngine;

namespace Utilities
{
   public class MeshCombinator : EditorWindow
   {
      private GameObject m_sourceObject = null;
      private GameObject m_combinedObject = null;
      private int m_maxMeshesToCombineCount = 500;
      
      [MenuItem("Window/Utilities/MeshCombinator")]
      public static void Open() => CreateInstance<MeshCombinator>().Show();

      private void OnGUI()
      {
         EditorGUILayout.BeginHorizontal();
         {
            m_sourceObject =
               EditorGUILayout.ObjectField("Source:", m_sourceObject, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("CombineObject"))
               CombineMeshes();
            EditorGUILayout.ObjectField("Combined object:", m_combinedObject, typeof(GameObject), true);
         }
         EditorGUILayout.EndHorizontal();
         m_maxMeshesToCombineCount = EditorGUILayout.IntField("Max Meshes To Combine", m_maxMeshesToCombineCount);
      }

      private void CombineMeshes()
      {
         if(m_sourceObject == null) return;

         var instance = Instantiate(m_sourceObject, Vector3.zero, Quaternion.identity);
         var meshFilters = instance.GetComponentsInChildren<MeshFilter>();
         
         string path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(m_sourceObject);
         path = Path.GetDirectoryName(path);

         List<List<CombineInstance>> CombineInstancecLists = new List<List<CombineInstance>>();
         List<CombineInstance> list = new List<CombineInstance>();
         int i = 0;
         foreach (var filter in meshFilters)
         {
            var combine = new CombineInstance();

            combine.mesh = filter.sharedMesh;
            combine.transform = filter.transform.localToWorldMatrix;
            
            list.Add(combine);
            if (list.Count == m_maxMeshesToCombineCount)
            {
               CombineInstancecLists.Add(list);
               list = new List<CombineInstance>();
            }
         }


         if (list.Count > 0)
            CombineInstancecLists.Add(list);
         
         m_combinedObject = new GameObject($"{m_sourceObject.name}Combined");
         if (!AssetDatabase.IsValidFolder($"{path}/Models"))
            AssetDatabase.CreateFolder(path, "Models");
         
         m_combinedObject.transform.position = Vector3.zero;
         m_combinedObject.transform.rotation = Quaternion.identity;
         m_combinedObject.transform.localScale = Vector3.one;
         int j = 0;
         foreach (var combineInstancecList in CombineInstancecLists)
         {
            var newMeshGameObject = new GameObject($"{m_sourceObject.name}_{j}_Combined", typeof(MeshFilter), typeof(MeshRenderer));
            newMeshGameObject.transform.SetParent(m_combinedObject.transform);
            Mesh combinedMesh = new Mesh
            {
               name = $"{m_sourceObject.name}_{j++}"
            };
            combinedMesh.CombineMeshes(combineInstancecList.ToArray());
            var meshFilter = newMeshGameObject.GetComponent<MeshFilter>();
            meshFilter.mesh = combinedMesh;
            AssetDatabase.CreateAsset(combinedMesh, $"{path}/Models/{combinedMesh.name}.asset");
         }

         m_combinedObject = PrefabUtility.SaveAsPrefabAsset(m_combinedObject, $"{path}/{m_combinedObject.name}.prefab");
         DestroyImmediate(instance);
         
         AssetDatabase.SaveAssets();
      }
   }
}