using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.ComboBox_addition
{
    public class ComboBoxItem
    {
        public Language Language { get; set; }

        public string Text { get; set; }
        
        public override string ToString()
        {
            return Text;
        }
    }
}
