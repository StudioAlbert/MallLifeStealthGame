using UnityEngine;
public class MaterialSwitch : MonoBehaviour
{
    private Material _baseMat;
    private Renderer _renderer;
    
    public void Start()
    {
        if (TryGetComponent(out _renderer))
        {
            _baseMat = _renderer.material;
        }
    }
    
    // Update is called once per frame
    public void SetNewMaterial(Material newMaterial) => _renderer.material = newMaterial;
    public void SetBaseMaterial() => _renderer.material = _baseMat;
    
}
