using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApp
{
    public class TestInput
    {
        [Display(Description = "Value 1 Property")]
        public int Value1 { get; set; }

        [Display(Description = "Value 2 Property")]
        public string Value2 { get; set; }

        [Display(Description = "Value 3 Property")]
        [MaxLength(3)]
        public List<string> Value3 { get; set; }
    }
}