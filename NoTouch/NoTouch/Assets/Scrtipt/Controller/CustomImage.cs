using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
    [SerializeField]
    public Image mSaveSuccessWindow;

    private Texture2D Drawtex;
    private Color[] colormap;
    static Texture2D Opentex, Resettex;
    [SerializeField]
    public Text pathText;
    // Start is called before the first frame update

    private int tool;
    private Color selectedColor=new Color(0,0,0,1);
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
        mRSlider.value = selectedColor.r;
        mGSlider.value = selectedColor.g;
        mBSlider.value = selectedColor.b;
        mColorImage.color = selectedColor;
    }
    public void Eraser()
    {
        tool = 1;
    }
    public void Pan()
    {
        tool = 0;
    }
    public Sprite CheckCustompath()
    {
        string path = Application.persistentDataPath + "/Assets/Resources/" + Paths.PROFILE_CUSTOM + ".png";
        if (File.Exists(path))
        {
            Opentex = LoadImageFromFile(Application.persistentDataPath + "/Assets/Resources/" + Paths.PROFILE_CUSTOM + ".png");
            Drawtex = new Texture2D(Opentex.width, Opentex.height);
            Drawtex = Opentex;
            Drawtex.filterMode = FilterMode.Point;
            Drawtex.Apply();
            return Sprite.Create(Drawtex, new Rect(0, 0, Opentex.width, Opentex.height), new Vector2(0.5f, 0.5f));
        }
        return null;
    }
    public void OpenImage()
    {
        // Load Texture from file
        //Texture2D tex = LoadImageFromFile(Application.dataPath + Paths.PROFILE_CUSTOM);
        //Opentex = LoadImageFromFile(Application.dataPath + Paths.PROFILE_CUSTOM);
        //Sprite Stex = 
        //Opentex = Resources.Load<Texture2D>(Paths.PROFILE_CUSTOM);
        string path = Application.persistentDataPath + "/Assets/Resources/" + Paths.PROFILE_CUSTOM;
        if (!Directory.Exists(path))
        {
            Opentex = Resources.Load<Texture2D>(Paths.PROFILE_CUSTOM);
            Directory.CreateDirectory((path));

            byte[] bytes = Opentex.EncodeToPNG();

            //string filePath = Application.dataPath + Paths.PROFILE_CUSTOM;
            File.WriteAllBytes((path + ".png"), bytes);
        }
        else
        {
            string path2 = Application.persistentDataPath + "/Assets/Resources/" + Paths.PROFILE_CUSTOM + ".png";
            if (File.Exists(path2))
            {
                Opentex = LoadImageFromFile(Application.persistentDataPath + "/Assets/Resources/" + Paths.PROFILE_CUSTOM + ".png");
            }
            else
            {
                Opentex = Resources.Load<Texture2D>(Paths.PROFILE_CUSTOM);
                Directory.CreateDirectory((path));

                byte[] bytes = Opentex.EncodeToPNG();

                //string filePath = Application.dataPath + Paths.PROFILE_CUSTOM;
                File.WriteAllBytes((path + ".png"), bytes);
            }
        }
        //Sprite Stex = Resources.Load<Sprite>(Paths.PROFILE_CUSTOM);
        //Opentex = Stex.texture;
        // Create a new Image with textures dimensions
        //OpenImg = CreateImage(tex.width, tex.height);
        // Set pixel colors
        Drawtex = new Texture2D(Opentex.width, Opentex.height);
        Drawtex = Opentex;
        Drawtex.filterMode = FilterMode.Point;
        Drawtex.Apply();

        //Colormap = new Color[tex.width * tex.height];

        ////for (int x = 0; x < tex.width; x++)
        ////{
        ////    for (int y = 0; y < tex.height; y++)
        ////    {
        ////        Colormap[x + y * tex.width] = Color.clear;
        ////        Drawtex.SetPixel(x, y, Color.clear);
        ////    }
        ////}

        //for (int x = 0; x < tex.width; x++)
        //{
        //    for (int y = 0; y < tex.height; y++)
        //    {
        //        Colormap[x + y * tex.width] = tex.GetPixel(x, tex.height - 1 - y);
        //    }
        //}
        CustomImage.Instance.mImage.GetComponent<Image>().sprite = Sprite.Create(Drawtex, new Rect(0, 0, Opentex.width, Opentex.height), new Vector2(0.5f,0.5f));
        //CurrentImg = OpenImg;
        //CurrentImg.LoadAllTexsFromMaps();;
    }
    public void ResetImage()
    {
        // Load Texture from file
        //Resettex = LoadImageFromFile(Application.dataPath + Paths.PROFILE_RESET);
        Sprite Stex = Resources.Load<Sprite>(Paths.PROFILE_RESET);
        Resettex = Stex.texture;
        // Create a new Image with textures dimensions
        //RsetImg = CreateImage(tex.width, tex.height);
        // Set pixel colors
        //RsetImg.layers[0].tex = tex;
        //RsetImg.layers[0].tex.filterMode = FilterMode.Point;
        //RsetImg.layers[0].tex.Apply();
        Drawtex = new Texture2D(Resettex.width, Resettex.height);
        Drawtex = Resettex;
        Drawtex.filterMode = FilterMode.Point;
        Drawtex.Apply();

        //Colormap = new Color[tex.width * tex.height];

        //for (int x = 0; x < tex.width; x++)
        //{
        //    for (int y = 0; y < tex.height; y++)
        //    {
        //        Colormap[x + y * tex.width] = tex.GetPixel(x, tex.height - 1 - y);
        //    }
        //}
        CustomImage.Instance.mImage.GetComponent<Image>().sprite = Sprite.Create(Drawtex, new Rect(0, 0, Resettex.width, Resettex.height), new Vector2(0.5f, 0.5f));
        //CurrentImg = RsetImg;
        //CurrentImg.LoadAllTexsFromMaps();;.
    }
    public void SaveImage()
    {


        string path = Application.persistentDataPath + "/Assets/Resources/" + Paths.PROFILE_CUSTOM;
        if (Directory.Exists(path))
        {
            Directory.CreateDirectory((path));

            byte[] bytes = Drawtex.EncodeToPNG();
            //pathText.text = path;
            //pathText.gameObject.SetActive(true);

            //string filePath = Application.dataPath + Paths.PROFILE_CUSTOM;
            File.WriteAllBytes((path + ".png"), bytes);
            mSaveSuccessWindow.gameObject.SetActive(true);
            Sprite spr = Sprite.Create(Drawtex, new Rect(0, 0, Drawtex.width, Drawtex.height), new Vector2(0.5f, 0.5f));
            CustomController.Instance.ChangeCustomImage(spr);
            StageController.Instance.ChangeCustomImage(spr);
            PlayerUpgradeController.Instance.ChangeCustomImage(spr);
            EarthController.Instance.ChangeCustomImage(spr);
        }
        //  byte[] bytes = Drawtex.EncodeToPNG();
        //pathText.text = filePath;
        //pathText.gameObject.SetActive(true);


        ////string filePath = Application.dataPath + Paths.PROFILE_CUSTOM;
        //File.WriteAllBytes(filePath, bytes);

    }
    //public UPAImage Getimg()
    //{
    //    return CurrentImg;
    //}

    
    public static Texture2D LoadImageFromFile(string path)
    {
        Texture2D tex = null;
        byte[] fileData;
        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            tex = new Texture2D(2, 2,TextureFormat.RGB24,false);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
    public void Draw(Vector2 touchPos)
    {
        if (tool == 1)
        {
            SetPixelByPos(Color.clear, touchPos);
        }
        else if (tool == 0)
        {
            SetPixelByPos(selectedColor, touchPos);
        }
    }
    public void ShowColor()
    {
        selectedColor.r = mRSlider.value;
        selectedColor.g = mGSlider.value;
        selectedColor.b = mBSlider.value;
        mColorImage.color = selectedColor;
    }
    public void SetPixelByPos(Color color, Vector2 pos)
    {
        Vector2 pixelCoordinate = GetPixelCoordinate(pos);

        if (pixelCoordinate == new Vector2(-1, -1))
            return;

        //Debug.Log("(int)pixelCoordinate.x :"+ (int)pixelCoordinate.x);
        //Debug.Log("(int)pixelCoordinate.y :"+ (int)pixelCoordinate.y);
        //Debug.Log("color :"+ color);
        //Undo.RecordObject (layers[layer].tex, "ColorPixel");

        SetPixel((int)((pixelCoordinate.x + 8) / 2), (int)(pixelCoordinate.y / 2), color);

        //EditorUtility.SetDirty (this);
        //dirty = true;
    }
    public void SetPixel(int x, int y, Color color)
    {

        Drawtex.SetPixel(x, y, color);
        Drawtex.Apply();
        CustomImage.Instance.mImage.GetComponent<Image>().sprite = Sprite.Create(Drawtex, new Rect(0, 0, Drawtex.width, Drawtex.height), new Vector2(0.5f, 0.5f));
        //CustomImage.Instance.mImage.sprite = Sprite.Create(Drawtex, new Rect(0, 0, Drawtex.width, Drawtex.height), new Vector2(0.5f, 0.5f));

        //map [x + y * - 1 * parentImg.width - parentImg.height] = color;
    }


    public Vector2 GetPixelCoordinate(Vector2 pos)
    {
        Rect texPos = new Rect(-2.5f, -0.5f, Drawtex.width, Drawtex.height);

        if (!texPos.Contains(pos))
        {
            return new Vector2(-1f, -1f);
        }
        float relX = (pos.x + 2f);
        float relY = (pos.y + 0.5f);
        int pixelX = (int)(16 * relX);
        int pixelY = (int)(16 * relY);

        return new Vector2(pixelX, pixelY);
    }
}
