using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblotekariensGränssnitt.Models.ViewModels
{
    public class UnreturnedItemsViewModel
    {
        public List<UnreturnedItemDTO> Dtos { get; set; }
        public UnreturnedItemDTO Dto { get; set; }
    }
}
