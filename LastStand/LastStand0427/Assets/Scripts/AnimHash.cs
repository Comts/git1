﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash 
{
    public static readonly int Attack = Animator.StringToHash("IsAttack");//수정불가
    public static readonly int Dead = Animator.StringToHash("IsDead");
    public static readonly int Walk = Animator.StringToHash("IsWalk");

}
