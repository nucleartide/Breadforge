using UnityEngine;

public class ThirdPartyModel : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private ThirdPartyAssets thirdPartyAssets;

    [SerializeField]
    private ThirdPartyAssets.Model model;

    private void Awake()
    {
        if (transform.childCount == 0)
            InstantiateModel();
    }

    public void InstantiateModel()
    {
        var prefab = thirdPartyAssets.GetPrefab(model);
        var instance = Instantiate(prefab);
        instance.transform.parent = transform;
        instance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
}
