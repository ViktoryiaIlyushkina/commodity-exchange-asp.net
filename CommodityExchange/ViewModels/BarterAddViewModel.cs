using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CommodityExchange.ViewModels
{
    public class BarterAddViewModel
    {
        [Required]
        [Display(Name = "Предмет аукциона")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фото")]
        public IFormFile Photo { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Начальная Цена")]
        public decimal StartPrice { get; set; }  
    }
}
