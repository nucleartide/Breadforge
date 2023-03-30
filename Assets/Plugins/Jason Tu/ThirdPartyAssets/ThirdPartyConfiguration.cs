using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// An exhaustive list of third party assets that are used in this project.
///
/// Use this to fill in game assets when checking out a fresh copy of the project from version control.
/// </summary>
[CreateAssetMenu]
public class ThirdPartyConfiguration : ScriptableObject
{
    public enum Model
    {
        Nothing,
        Pickaxe,
        Axe,
        WaterBottle,
    }

    [System.Serializable]
    public class ModelToPrefabMapping
    {
        public Model Model;

        [NotNull]
        public GameObject Prefab;
    }

    [Tooltip("Uses the pickaxe model from polyperfect's low poly pack.")]
    public ModelToPrefabMapping Pickaxe;

    [Tooltip("Uses the axe model from polyperfect's low poly pack.")]
    public ModelToPrefabMapping Axe;

    [Tooltip("Uses the water bottle model from polyperfect's low poly pack.")]
    public ModelToPrefabMapping WaterBottle;

    private List<ModelToPrefabMapping> AllMappings
    {
        get
        {
            return new List<ModelToPrefabMapping>
            {
                Pickaxe,
                Axe,
                WaterBottle,
            };
        }
    }

    public GameObject GetPrefab(Model model)
    {
        var mappings = AllMappings;
        foreach (var mapping in mappings)
            if (mapping.Model == model)
                return mapping.Prefab;

        throw new System.Exception($"Model {model} does not have a corresponding mapping.");
    }
}
