using System.Collections.Generic;

namespace LibrariansInterface.Models.ViewModels
{
    public class UnreturnedItemsViewModel
    {
        public List<UnreturnedItemDTO> Dtos { get; set; }
        public UnreturnedItemDTO Dto { get; set; }
    }
}
