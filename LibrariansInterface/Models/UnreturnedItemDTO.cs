﻿namespace LibrariansInterface.Models
{
    public class UnreturnedItemDTO //Best Practice är att inte skicka obehandlad information mellan api och mvc. 
    {
        public string BorrowerID { get; set; }
        public string BorrowerFullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string InventoryID { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string BorrowDate { get; set; }
        public string ExpectedReturnDate { get; set; }
    }

}
