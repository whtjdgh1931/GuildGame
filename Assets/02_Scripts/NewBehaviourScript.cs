using UnityEngine;
using System.IO;

public class TransparentRenderTextureSaver : MonoBehaviour
{
    public Camera targetCamera;
    public RenderTexture renderTexture;

    public void SaveTransparentPNG(string fileName = "TransparentCapture.png")
    {
        // 카메라 설정: 투명 배경
        targetCamera.clearFlags = CameraClearFlags.SolidColor;
        targetCamera.backgroundColor = new Color(0, 0, 0, 0); // 완전 투명

        // RenderTexture 활성화
        RenderTexture.active = renderTexture;

        // 알파 채널 포함한 Texture2D 생성
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();

        // PNG로 저장
        byte[] bytes = tex.EncodeToPNG();

        // 저장 경로 설정
        string folderPath = @"C:\Users\User\Desktop\guild\Assets\Resources";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string fullPath = Path.Combine(folderPath, fileName);
        File.WriteAllBytes(fullPath, bytes);

        Debug.Log("투명 PNG 저장 완료: " + fullPath);

        // 정리
        RenderTexture.active = null;
        Destroy(tex);
    }
}
