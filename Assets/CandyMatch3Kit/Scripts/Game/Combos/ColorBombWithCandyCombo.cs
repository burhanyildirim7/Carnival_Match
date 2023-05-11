// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

using FullSerializer;

using GameVanilla.Core;
using GameVanilla.Game.Popups;
using GameVanilla.Game.Scenes;
using GameVanilla.Game.UI;


namespace GameVanilla.Game.Common
{
    /// <summary>
    /// The class used for the color bomb + candy combo.
    /// </summary>
    public class ColorBombWithCandyCombo : ColorBombCombo
    {
        public override void Resolve(GameBoard board, List<GameObject> tiles, FxPool fxPool)
        {
            GameObject.Find("ColorBombAndCandy").GetComponent<ColorBombAndCandy>().Resolve(board, tileA, tileB);
        }
    }
}
