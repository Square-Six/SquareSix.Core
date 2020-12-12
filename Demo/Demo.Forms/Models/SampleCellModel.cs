using System;
using Demo.Forms.Enums;
using SquareSix.Core.Models;

namespace Demo.Forms.Models
{
    public class SampleCellModel : BasePropertyChangedModel
    {
        public string Text { get; private set; }
        public SampleType SampleType { get; private set; }

        public SampleCellModel(string text, SampleType type)
        {
            Text = text;
            SampleType = type;
        }
    }
}
