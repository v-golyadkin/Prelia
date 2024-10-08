using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _whiteFlashMaterial;
    [SerializeField] private float _restoreDefaultMaterialTime = .2f;

    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public float GetRestoreMaterialTime()
    {
        return _restoreDefaultMaterialTime;
    }

    public IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = _whiteFlashMaterial;
        yield return new WaitForSeconds(_restoreDefaultMaterialTime);
        _spriteRenderer.material = _defaultMaterial;
    }
}
