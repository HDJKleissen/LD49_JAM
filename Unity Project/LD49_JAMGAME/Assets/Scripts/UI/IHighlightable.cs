using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

interface IHighlightable
{
    Color OriginalColor { get; set; }
    Color highlightColor { get; }

    void ToggleHighlight(bool highlighting);
}
