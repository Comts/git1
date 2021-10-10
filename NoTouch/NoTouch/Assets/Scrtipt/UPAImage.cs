//-----------------------------------------------------------------
// This class stores all information about the image.
// It has a full pixel map, width & height properties and some private project data.
// It also hosts functions for calculating how the pixels should be visualized in the editor.
//-----------------------------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class UPAImage : ScriptableObject
{
	// HELPER GETTERS


	// IMAGE DATA

	public int width;
	public int height;

	public UPALayer[] layers;
	public int layerCount
	{
		get { return layers.Length; }
	}

	public Texture2D finalImg;

	// VIEW & NAVIGATION SETTINGS

	[SerializeField]
	private float _gridSpacing = 20f;
	public float gridSpacing 
	{
		get { return _gridSpacing + 1f; }
		set { _gridSpacing = Mathf.Clamp (value, 0, 140f); }
	}

	public float gridOffsetY = 0;
	public float gridOffsetX = 0;

	//Make sure we always get a valid layer
	private int _selectedLayer = 0;
	public int selectedLayer {
		get {
			return _selectedLayer;
		}
		set { _selectedLayer = value; }
	}
	
	
	// PAINTING SETTINGS

	public Color selectedColor = new Color (0,0,0,1);
	public UPATool tool = UPATool.PaintBrush;
	public int gridBGIndex = 0;


	//MISC VARIABLES

	public bool dirty = false;		// Used for determining if data has changed
	
	
	// Class constructor
	public UPAImage () {
		// do nothing so far
	}

	// This is not called in constructor to have more control
	public void Init (int w, int h) 
	{
		width = w;
		height = h;

		layers = new UPALayer[2];
		layers[0] = new UPALayer(this);
		//layers.Add(newLayer);

		EditorUtility.SetDirty (this);
		dirty = true;
	}

	// Color a certain pixel by position in window in a certain layer
	public void SetPixelByPos (Color color, Vector2 pos)
	{
		Vector2 pixelCoordinate = GetPixelCoordinate (pos);

		if (pixelCoordinate == new Vector2 (-1, -1))
			return;

		//Debug.Log("(int)pixelCoordinate.x :"+ (int)pixelCoordinate.x);
		//Debug.Log("(int)pixelCoordinate.y :"+ (int)pixelCoordinate.y);
		//Debug.Log("color :"+ color);
		//Undo.RecordObject (layers[layer].tex, "ColorPixel");

		layers[0].SetPixel ((int)(pixelCoordinate.x+8)/2, (int)pixelCoordinate.y/2, color);
		
		EditorUtility.SetDirty (this);
		dirty = true;
	}

	//// Return a certain pixel by position in window
	//public Color GetPixelByPos (Vector2 pos, int layer) 
	//{
	//	Vector2 pixelCoordinate = GetPixelCoordinate (pos);

	//	if (pixelCoordinate == new Vector2 (-1, -1)) {
	//		return Color.clear;
	//	} else {
	//		return layers[layer].GetPixel ((int)pixelCoordinate.x, (int)pixelCoordinate.y);
	//	}
	//}

	//public Color GetBlendedPixel (int x, int y) {
	//	Color color = Color.clear;

	//	for (int i = 0; i < layers.Length; i++) {
	//		if (!layers[i].enabled)
	//			continue;

	//		Color pixel = layers[i].tex.GetPixel(x,y);

	//		// This is a blend between two methods of calculating color blending; Alpha blending and premultiplied alpha blending
	//		// I have no clue why this actually works but it's very accurate :D
	//		float newR = Mathf.Lerp (1f * pixel.r + (1f - pixel.a) * color.r, pixel.a * pixel.r + (1f - pixel.a) * color.r, color.a);
	//		float newG = Mathf.Lerp (1f * pixel.g + (1f - pixel.a) * color.g, pixel.a * pixel.g + (1f - pixel.a) * color.g, color.a);
	//		float newB = Mathf.Lerp (1f * pixel.b + (1f - pixel.a) * color.b, pixel.a * pixel.b + (1f - pixel.a) * color.b, color.a);

	//		float newA = pixel.a + color.a * (1 - pixel.a);

	//		color = new Color (newR, newG, newB, newA);
	//	}

	//	return color;
	//}
	
	//public void ChangeLayerPosition (int from, int to) {
	//	if (from >= layers.Count || to >= layers.Count || from < 0 || to < 0) {
	//		Debug.LogError ("Cannot ChangeLayerPosition, out of range.");
	//		return;
	//	}
		
	//	UPALayer layer = layers[from];
	//	layers.RemoveAt(from);
	//	layers.Insert(to, layer);

	//	dirty = true;
	//}

	// Get the rect of the image as displayed in the editor
	public Rect GetImgRect () 
	{
		float w = width;
		float h = height;
		
		float xPos = -2.5f;
		float yPos = -0.5f;

		return new Rect (xPos,yPos, w, h);
	}

	public Vector2 GetPixelCoordinate (Vector2 pos)
	{
		Rect texPos = GetImgRect();
			
		if (!texPos.Contains (pos)) {
			return new Vector2(-1f,-1f);
		}

		//float relX = (pos.x - texPos.x) / texPos.width;
		//float relY = (texPos.y - pos.y) / texPos.height;
		//float relX=1;// texPos.width;
		//float relY=1;// / texPos.height;

		//if (pos.x < 2.5)
		//{
		//	relX = (pos.x - texPos.x);
		//}
		//else
		//{
		//	relX = (pos.x + texPos.x);
		//}
		float relX = (pos.x +2f);
		float relY = (pos.y + 0.5f);
		//if (pos.y < 0.5)
		//{
		//}
		//else
		//{
		//	relY = (pos.y + texPos.y);
		//}
		int pixelX = (int)(16 * relX);
		int pixelY = (int)(16 * relY);

		return new Vector2(pixelX, pixelY);
	}
	
	//public Vector2 GetReadablePixelCoordinate (Vector2 pos) {
	//	Vector2 coord = GetPixelCoordinate (pos);
		
	//	if (coord.x == -1)
	//		return coord;
		
	//	coord.x += 1;
	//	coord.y *= -1;
	//	return coord;
	//}

	public Texture2D GetFinalImage (bool update) 
	{


		finalImg = UPADrawer.CalculateBlendedTex(layers);
		//finalImg.filterMode = FilterMode.Point;
		//finalImg.Apply();

		dirty = false;
		return finalImg;
	}

	//public void LoadAllTexsFromMaps () 
	//{
	//	if (layers[0].tex == null)
	//	{
	//		layers[0].LoadTexFromMap();
	//	}
	//}

	//public void AddLayer () {
	//	Undo.RecordObject (this, "AddLayer");
	//	EditorUtility.SetDirty (this);
	//	this.dirty = true;

	//	UPALayer newLayer = new UPALayer (this);
	//	layers.Add(newLayer);
	//}

	//public void RemoveLayerAt (int index) {
	//	Undo.RecordObject (this, "RemoveLayer");
	//	EditorUtility.SetDirty (this);
	//	this.dirty = true;

	//	layers.RemoveAt (index);
	//	if (selectedLayer == index) {
	//		selectedLayer = index - 1;
	//	}
	//}
}
public enum UPATool
{
	PaintBrush,
	Eraser
}
