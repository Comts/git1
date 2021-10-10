using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class CustomImage : MonoBehaviour
{
    public static CustomImage Instance;
    [SerializeField]
    public Image mImage;
    [SerializeField]
    public Image mColorImage;
    [SerializeField]
    public Slider mRSlider, mGSlider, mBSlider;
    public static UPAImage CurrentImg;
    [SerializeField]
    public Image mSaveSuccessWindow;
    // Start is called before the first frame update

    private UPATool tool
    {
        get { return CurrentImg.tool; }
        set { CurrentImg.tool = value; }
    }
    private Color32 selectedColor
    {
        get { return CurrentImg.selectedColor; }
        set { CurrentImg.selectedColor = value; }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartCustom()
    {
        mRSlider.value = CurrentImg.selectedColor.r;
        mGSlider.value = CurrentImg.selectedColor.g;
        mBSlider.value = CurrentImg.selectedColor.b;
        mColorImage.color = CurrentImg.selectedColor;
    }
    public void Eraser()
    {
        CurrentImg.tool = UPATool.Eraser;
    }
    public void Pan()
    {
        CurrentImg.tool = UPATool.PaintBrush;
    }
    public void OpenImage()
    {
        // Load Texture from file
        Texture2D tex = LoadImageFromFile(Application.dataPath + Paths.PROFILE_CUSTOM);
        // Create a new Image with textures dimensions
        UPAImage img = CreateImage(tex.width, tex.height);
        // Set pixel colors
        img.layers[0].tex = tex;
        img.layers[0].tex.filterMode = FilterMode.Point;
        img.layers[0].tex.Apply();
        for (int x = 0; x < img.width; x++)
        {
            for (int y = 0; y < img.height; y++)
            {
                img.layers[0].map[x + y * tex.width] = tex.GetPixel(x, tex.height - 1 - y);
            }
        }
        CustomImage.Instance.mImage.sprite = Sprite.Create(tex, new Rect(0, 0, img.width, img.height), new Vector2(0.5f,0.5f));
        CurrentImg = img;
        //CurrentImg.LoadAllTexsFromMaps();;
    }
    public void ResetImage()
    {
        // Load Texture from file
        Texture2D tex = LoadImageFromFile(Application.dataPath + Paths.PROFILE_RESET);
        // Create a new Image with textures dimensions
        UPAImage img = CreateImage(tex.width, tex.height);
        // Set pixel colors
        img.layers[0].tex = tex;
        img.layers[0].tex.filterMode = FilterMode.Point;
        img.layers[0].tex.Apply();
        for (int x = 0; x < img.width; x++)
        {
            for (int y = 0; y < img.height; y++)
            {
                img.layers[0].map[x + y * tex.width] = tex.GetPixel(x, tex.height - 1 - y);
            }
        }
        CustomImage.Instance.mImage.sprite = Sprite.Create(tex, new Rect(0, 0, img.width, img.height), new Vector2(0.5f,0.5f));
        CurrentImg = img;
        //CurrentImg.LoadAllTexsFromMaps();;
    }
    public void SaveImage()
    {

        byte[] bytes = CurrentImg.GetFinalImage(true).EncodeToPNG();

        string filePath = Application.dataPath + Paths.PROFILE_CUSTOM;

        File.WriteAllBytes(filePath, bytes);
        CustomController.Instance.LoadProfile();
        mSaveSuccessWindow.gameObject.SetActive(true);

    }
    public UPAImage Getimg()
    {
        return CurrentImg;
    }

    public static UPAImage CreateImage(int w, int h)
    {
        //string path = Application.dataPath + Paths.PROFILE_ASSET;
        //if (path == "")
        //{
        //    Debug.Log("no");
        //    return null;
        //}
        //Debug.Log("path : " + path);

        //path = FileUtil.GetProjectRelativePath(path);
        //Debug.Log("path : " + path);

        UPAImage img = ScriptableObject.CreateInstance<UPAImage>();
        //AssetDatabase.CreateAsset(img, path+ "UPAImage");

        //AssetDatabase.SaveAssets();

        img.Init(w, h);
        //EditorUtility.SetDirty(img);
        ////UPAEditorWindow.CurrentImg = img;

        //EditorPrefs.SetString("currentImgPath", AssetDatabase.GetAssetPath(img));

        //if (UPAEditorWindow.window != null)
        //    UPAEditorWindow.window.Repaint();
        //else
        //    UPAEditorWindow.Init();

        //img.gridSpacing = 10 - Mathf.Abs(img.width - img.height) / 100f;
        return img;
    }
    public static Texture2D LoadImageFromFile(string path)
    {
        Debug.Log("path :"+ path);
        Texture2D tex = null;
        byte[] fileData;
        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
    public void Draw(Vector2 touchPos)
    {
        if (tool == UPATool.Eraser)
        {
            CurrentImg.SetPixelByPos(Color.clear, touchPos);
        }
        else if (tool == UPATool.PaintBrush)
        {
            CurrentImg.SetPixelByPos(selectedColor, touchPos);
        }
    }
    public void ShowColor()
    {
        CurrentImg.selectedColor.r = mRSlider.value;
        CurrentImg.selectedColor.g = mGSlider.value;
        CurrentImg.selectedColor.b = mBSlider.value;
        mColorImage.color = CurrentImg.selectedColor;
    }
}
