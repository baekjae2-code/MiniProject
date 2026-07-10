using UnityEngine;

public class Base : Unit
{
    private SpriteRenderer spriteRenderer;

    public Color targetColor = Color.white;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        spriteRenderer.color = targetColor;
        rb.linearVelocity = Vector3.zero;
    }

    private void OnDisable()
    {
        UIManager.instance.GameOverUI(name);
    }
}
