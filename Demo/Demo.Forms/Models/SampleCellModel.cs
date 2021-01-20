using System;
using System.Threading.Tasks;
using SquareSix.Core;

namespace Demo.Forms.Models
{
    public class SampleCellModel : BasePropertyChangedModel
    {
        public string Text { get; private set; }
        public Func<Task> OnCellTappedTask { get; private set; }

        public SampleCellModel(string text, Func<Task> onCellTappedTask)
        {
            Text = text;
            OnCellTappedTask = onCellTappedTask;
        }
    }
}
