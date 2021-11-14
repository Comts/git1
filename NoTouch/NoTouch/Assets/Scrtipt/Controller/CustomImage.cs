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
    [SerializeField]
    public Image[] Backround;
    // Start is called before the first frame update

    private int tool;
    private Color selectedColor;
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
    private void Start()
    {
        selectedColor =  Color.black;
    }
    public void StartCustom()
    {
        SettingSlider();
        SetBackGround(tool);
    }
    public void Pan()
    {
        tool = 0;
        SetBackGround(tool);
    }
    public void Eraser()
    {
        tool = 1;
        SetBackGround(tool);
    }
    public void Spoid()
    {
        tool = 2;
        SetBackGround(tool);
    }
    public void Paint()
    {
        tool = 3;
        SetBackGround(tool);
    }
    public void SettingSlider()
    {
        mRSlider.value = selectedColor.r;
        mGSlider.value = selectedColor.g;
        mBSlider.value = selectedColor.b;
        mColorImage.color = selectedColor;
    }
    public void SetBackGround(int num)
    {
        for (int i = 0; i < Backround.Length; i++)
        {
            if (i == num)
            {
                Backround[i].gameObject.SetActive(true);
                continue;
            }
            Backround[i].gameObject.SetActive(false);
        }
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
        Resettex = Instantiate(Stex.texture);
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
        switch (tool)
        {
            case 0:
                BrushPixelByPos(selectedColor, touchPos);
                break;
            case 1:
                BrushPixelByPos(Color.clear, touchPos);
                break;
            case 2:
                GetPixelColorByPos(touchPos);
                break;
            case 3:
                PaintPixelsFromPos(touchPos);
                break;
            default:
                Debug.LogError("tool : " + tool);
                break;
        }
    }
    public void Drawing(Vector2 touchPos)
    {
        switch (tool)
        {
            case 0:
                BrushPixelByPos(selectedColor, touchPos);
                break;
            case 1:
                BrushPixelByPos(Color.clear, touchPos);
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                Debug.LogError("tool : " + tool);
                break;
        }
    }
    public void SetColor_R()
    {
        selectedColor.r = mRSlider.value;
        mColorImage.color = selectedColor;
    }
    public void SetColor_G()
    {
        selectedColor.g = mGSlider.value;
        mColorImage.color = selectedColor;
    }
    public void SetColor_B()
    {
        selectedColor.b = mBSlider.value;
        mColorImage.color = selectedColor;
    }
    public void BrushPixelByPos(Color color, Vector2 pos)
    {
        Vector2 pixelCoordinate = GetPixelCoordinate(pos);

        if (pixelCoordinate == new Vector2(-1, -1))
            return;

        //Debug.Log("(int)pixelCoordinate.x :"+ (int)pixelCoordinate.x);
        //Debug.Log("(int)pixelCoordinate.y :"+ (int)pixelCoordinate.y);
        //Debug.Log("color :"+ color);
        //Undo.RecordObject (layers[layer].tex, "ColorPixel");

        BrushPixel((int)((pixelCoordinate.x + 8) / 2), (int)(pixelCoordinate.y / 2), color);

        //EditorUtility.SetDirty (this);
        //dirty = true;
    }
    public void BrushPixel(int x, int y, Color color)
    {

        Drawtex.SetPixel(x, y, color);
        Drawtex.Apply();
        CustomImage.Instance.mImage.GetComponent<Image>().sprite = Sprite.Create(Drawtex, new Rect(0, 0, Drawtex.width, Drawtex.height), new Vector2(0.5f, 0.5f));
        
        if (color != Color.clear)
        {
            Color Checkcolor = Drawtex.GetPixel(x, y);
            if (Checkcolor != selectedColor)
            {
                selectedColor = Checkcolor;
                SettingSlider();
            }
        }
        //CustomImage.Instance.mImage.sprite = Sprite.Create(Drawtex, new Rect(0, 0, Drawtex.width, Drawtex.height), new Vector2(0.5f, 0.5f));

        //map [x + y * - 1 * parentImg.width - parentImg.height] = color;
    }
    public void PaintPixelsFromPos(Vector2 pos)
    {
        Vector2 pixelCoordinate = GetPixelCoordinate(pos);

        if (pixelCoordinate == new Vector2(-1, -1))
            return;

        //Debug.Log("(int)pixelCoordinate.x :"+ (int)pixelCoordinate.x);
        //Debug.Log("(int)pixelCoordinate.y :"+ (int)pixelCoordinate.y);
        //Debug.Log("color :"+ color);
        //Undo.RecordObject (layers[layer].tex, "ColorPixel");

        PaintPixels((int)((pixelCoordinate.x + 8) / 2), (int)(pixelCoordinate.y / 2));

        //EditorUtility.SetDirty (this);
        //dirty = true;
    }
    public void PaintPixels(int x, int y)
    {
        Color Checkcolor = Drawtex.GetPixel(x, y);
        if (Checkcolor != selectedColor)
        {
            Painting(x, y, Checkcolor,0);
            Drawtex.Apply();
            CustomImage.Instance.mImage.GetComponent<Image>().sprite = Sprite.Create(Drawtex, new Rect(0, 0, Drawtex.width, Drawtex.height), new Vector2(0.5f, 0.5f));
            selectedColor = Drawtex.GetPixel(x, y);
            SettingSlider();
        }
    }
    public void Painting(int x, int y, Color color, int num)
    {
        if (x < 0 || x > 39 || y < 0 || y > 39)
        {
            return;
        }
        //if (x < Px - 2 || x > Px + 2 || y < Py - 2 || y > Py + 2)
        //{
        //    return;
        //}
        Color Checkcolor = Drawtex.GetPixel(x, y);
        if (color != Checkcolor|| Checkcolor == selectedColor)
        {
            return;
        }
        Drawtex.SetPixel(x, y, selectedColor);

        if( num != 1 )
        {
            Painting(x - 1, y, color, 2);
        }
        if (num != 2)
        {
            Painting(x + 1, y, color, 1);
        }
        if (num != 3 )
        {
            Painting(x, y + 1, color, 4);
        }
        if (num != 4 )
        {
            Painting(x, y - 1, color, 3);
        }
        //if( num != 1 || Px > x)
        //{
        //    if (x <= Px)
        //    {
        //        Painting(x - 1, y, color, Px, Py, 2);
        //    }
        //}
        //if (num != 2)
        //{
        //    if(x >= Px)
        //    {
        //        Painting(x + 1, y, color, Px, Py, 1);
        //    }
        //}
        //if (num != 3 || Py < y)
        //{
        //    if(y >= Py)
        //    {
        //        Painting(x, y + 1, color, Px, Py, 4);
        //    }
        //}
        //if (num != 4 || Py > y )
        //{
        //    if (y <= Py)
        //    {
        //        Painting(x, y - 1, color, Px, Py, 3);
        //    }
        //}
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
    public void GetPixelColorByPos(Vector2 pos)
    {
        Vector2 pixelCoordinate = GetPixelCoordinate(pos);

        if (pixelCoordinate == new Vector2(-1, -1))
            return;

        //Debug.Log("(int)pixelCoordinate.x :"+ (int)pixelCoordinate.x);
        //Debug.Log("(int)pixelCoordinate.y :"+ (int)pixelCoordinate.y);
        //Debug.Log("color :"+ color);
        //Undo.RecordObject (layers[layer].tex, "ColorPixel");

        GetPixelColor((int)((pixelCoordinate.x + 8) / 2), (int)(pixelCoordinate.y / 2));

        //EditorUtility.SetDirty (this);
        //dirty = true;
    }
    public void GetPixelColor(int x, int y)
    {
        Color Checkcolor = Drawtex.GetPixel(x, y);
        if (Checkcolor != Color.clear)
        {
            selectedColor = Checkcolor;
            SettingSlider();
        }
    }
}
