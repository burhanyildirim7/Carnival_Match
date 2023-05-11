// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;

using UnityEngine;

using GameVanilla.Core;

namespace GameVanilla.Game.Common
{
    /// <summary>
    /// The class used for the striped candy + striped candy combo.
    /// </summary>
    public class TwoStripedCandyCombo : Combo
    {
        public override void Resolve(GameBoard board, List<GameObject> tiles, FxPool fxPool)
        {
            GameObject.Find("StripedComboRoketOlustur").GetComponent<StripedComboRoketOlustur>().Resolve(board, tileA, tileB);
        }
    }
}
