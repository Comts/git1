using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Paths //Resources 경로를 저장하기 위한 클래스
{
    public const string GEM_PREFAB = "Gem";
    public const string PLAYER_ITEM_ICON = "PlayerItems";

    private const string JSON_FILE_LOCATION = "JsonFiles/"; //찾는 경로를 줄이기 위함. 
    public const string PLAYER_ITEM_TABLE = JSON_FILE_LOCATION+"PlayerItem";
    public const string PLAYER_ITEM_TEXT_TABLE = JSON_FILE_LOCATION+"PlayerItemText";
}

public static class Constants //const 변수들
{
    public const int TOTAL_GEM_COUNT = 3;
}

