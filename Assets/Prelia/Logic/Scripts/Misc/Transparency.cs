using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Transparency : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _transparencyAmount = .8f;
    [SerializeField] private float _fadeTime = .4f;

    private SpriteRenderer _spriteRenderer;
    private Tilemap _tilemap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, _fadeTime, _spriteRenderer.color.a, _transparencyAmount));
            } else if(_tilemap)
            {
                StartCoroutine(FadeRoutine(_tilemap, _fadeTime, _tilemap.color.a, _transparencyAmount));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, _fadeTime, _spriteRenderer.color.a, 1f));
            }
            else if (_tilemap)
            {
                StartCoroutine(FadeRoutine(_tilemap, _fadeTime, _tilemap.color.a, 1f));
            }
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
