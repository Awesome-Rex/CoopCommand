using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TransformTools;

public static class CameraExtensions
{
    public static Vector2 ScaleFromTexture (this Camera camera, Vector2 point)
    {
        //return Vectors.MultiplyVector2(point, new Vector2(camera.scaledPixelWidth / camera.targetTexture.width, camera.scaledPixelHeight / camera.targetTexture.height));
        return Vectors.MultiplyVector2(point, new Vector2(Camera.main.pixelWidth / camera.targetTexture.width, Camera.main.pixelHeight / camera.targetTexture.height));
    }

    public static Vector3 ScaleFromTexture(this Camera camera, Vector3 point)
    {
        //Vector2 temp = Vectors.MultiplyVector2(point, new Vector2(camera.scaledPixelWidth / camera.targetTexture.width, camera.scaledPixelHeight / camera.targetTexture.height));
        Vector2 temp = Vectors.MultiplyVector2(point, new Vector2(Camera.main.pixelWidth / camera.targetTexture.width, Camera.main.pixelHeight / camera.targetTexture.height));
        
        return new Vector3(temp.x, temp.y, point.z);
    }

    public static Vector3 ScreenToScreenPoint (this Camera camera, Vector3 point, Camera to)
    {
        return to.ViewportToScreenPoint(camera.ScreenToViewportPoint(point));
    }
}
