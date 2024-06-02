using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libary.Models
{
    public class Reservation
    {
        // Properties
        public int ReservationId { get; set; }

        // Foreign Key for User
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        // Foreign Key for Book
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }

        public DateTime ReservedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int StatusId { get; set; }
    }
}

