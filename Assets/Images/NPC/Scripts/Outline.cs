using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    private SpriteRenderer npcRenderer;
    private Shader shaderCUItext;
    private Shader shaderSpritesDefault;

    public GameObject pilot;
    public GameObject otherObject;
    private float Distance;

    // Start is called before the first frame update
    void Start()
    {
        npcRenderer = GetComponent<SpriteRenderer>();
        shaderCUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(pilot.transform.position, otherObject.transform.position);
        if (Distance <= 0.5f)
        {
            whiteSprite();
        }
        else
        {
            normalSprite();
        }
    }

    void whiteSprite()
    {
        npcRenderer.material.shader = shaderCUItext;
        npcRenderer.color = Color.white;
    }

    void normalSprite()
    {
        npcRenderer.material.shader = shaderSpritesDefault;
        npcRenderer.color = Color.black;
    }
}
