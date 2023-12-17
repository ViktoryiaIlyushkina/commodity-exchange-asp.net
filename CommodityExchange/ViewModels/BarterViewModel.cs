using CommodityExchange.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CommodityExchange.ViewModels
{
    public class BarterViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public StatusType Status { get; set; }

        [Required]
        [Display(Name = "Организатор")]
        public string Owner { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Начальная Цена")]
        public decimal StartPrice { get; set; }

        [Required]
        [Display(Name = "Дата открытия")]
        public DateTime CreationTime { get; set; }

        [Required]
        [Display(Name = "Дата закрытия")]
        public DateTime? ClosedTime { get; set; }
    }
}
