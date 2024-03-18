using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public UnityEngine.UI.RawImage imageOnCanvas;
    public Texture2D myTexture;
    public List<Vector2> hitPoints = new();
    public float scale = 0.1f;
    public void SetupImageOnCanvas()
    {
        //creates the texture and assigns it to the canvas
        myTexture = new Texture2D(128, 128, TextureFormat.ARGB32, false);
        imageOnCanvas.texture = myTexture;
    }

    public void DrawPixelToImage()
    {
        // makes the entire texture clear so it can be drawn on with transparency
        for (int y = 0; y < myTexture.height; y++)
        {
            for (int x = 0; x < myTexture.width; x++)
            {
                myTexture.SetPixel(x, y, color: Color.clear);
            }
        }
        //applies it
        myTexture.Apply();
    }

    private void Start()
    {
        SetupImageOnCanvas();
        DrawPixelToImage();
    }

    private void Update()
    {
        //finds the position of the radar in the world
        Vector2 radarPos = new Vector2(this.transform.position.x, this.transform.position.z);

        //activates when something gets added to the list from radar.cs
        while(hitPoints.Count != 0)
        {
            //draws a pixel on the radar relative to the middle, zoom can be adjusted with scale in the inspector.
            Vector2 lastHit = hitPoints[hitPoints.Count - 1];
            lastHit *= scale;
            myTexture.SetPixel(x: (int)lastHit.x + 64 - (int)radarPos.x, y: (int)lastHit.y + 64 - (int)radarPos.y, color: Color.red);
            hitPoints.RemoveAt(hitPoints.Count - 1);

        }
        //applies it
        myTexture.Apply();
    }

}
