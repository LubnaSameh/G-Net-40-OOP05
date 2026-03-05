using System;

namespace OOP05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part 01 : Theoretical Questions

            #region Q1 : What is an interface in C#? Why do we use it? Benefits?
            /*
             * 1. Definition: 
             * An interface is a "Contract." It tells a class what methods and properties it must have, but it does not write the code for them.
             * * 2. Why use it: 
             * It allows us to write flexible code that doesn't depend on one specific class. We can swap different classes easily as long as they follow the same interface.
             * * 3. Three Benefits:
             * - Loose Coupling: Changes in one part of the code don't break everything else
             * - Multiple Implementation: A class can follow many interfaces at the same time
             * - Standardizing Code: It ensures that different classes provide the same functionality like "Print" or "Clone"
             */
            #endregion

            #region Q2 : Interface Naming Conflicts 
            /*
             * a) The Problem: 
             * Currently, there is ambiguity. The class has one Greet() method that tries to satisfy both interfaces at once
             * * b) The Fix: 
             * Use "Explicit Interface Implementation"
             * Example: 
             * void IEnglishSpeaker.Greet() { Console.WriteLine("Hello"); }
             * void IArabicSpeaker.Greet() { Console.WriteLine("Ahlan"); }
             * * c) Calling the Method: 
             * No, you cannot call it directly from the object (e.g., translator.Greet() will not work)
             * To call it, you must cast the object to the interface type first:
             * ((IEnglishSpeaker)translator).Greet();
             */
            #endregion

            #region Q3 : Shallow Copy vs. Deep Copy
            /*
             * 1. Shallow Copy: 
             * Copies the object’s values. If the object contains another object reference type, it only copies the memory address the "pointer"
             * * 2. Deep Copy: 
             * Creates a completely new and independent copy of the object and all other objects inside it
             * * 3. The Risk: 
             * In a shallow copy, if you change a reference-type field in the copy, it will also change in the original object because they share the same memory
             */
            #endregion

            #region Q4 : Code Snippet Output Analysis
            /*
             * Output:
             * Dev - Testing
             * QA - Testing
             * * Why:
             * - The Title ("Dev","QA") changed only for e2 because it is a value-like type string.
             * - The Dept.Name "Testing" changed for BOTH e1 and e2. 
             * - This happened because ShallowCopy used MemberwiseClone(), 
             * which copies the reference to the Department object instead of creating a new one
             */
            #endregion

            #endregion

            #region Part 02 : Practical (Extending the Movie Ticket Booking System)

            // a. Create a Cinema and open it.
            Cinema cinema = new Cinema();
            cinema.OpenCinema();

            // b. Create one of each ticket type with hardcoded data. Book all three and add them to the Cinema.
            StandardTicket st = new StandardTicket("Inception", 80m, "A5");
            VIPTicket vip = new VIPTicket("Avengers", 200m, true);
            IMAXTicket imax = new IMAXTicket("Dune", 130m, true);

            st.Book();
            vip.Book();
            imax.Book();

            cinema.AddTicket(st);
            cinema.AddTicket(vip);
            cinema.AddTicket(imax);

            // c. Print all tickets through the Cinema.
            cinema.PrintAllTickets();

            // d. Clone a VIP ticket, change the clone's movie name, and print both to prove independence.
            Console.WriteLine("--- Clone Test ---");
            VIPTicket clonedVip = (VIPTicket)vip.Clone();
            clonedVip.MovieName = "Interstellar";

            Console.Write("Original : ");
            vip.Print();
            Console.Write("Clone    : ");
            clonedVip.Print();

            // e. Cancel one ticket and reprint it to show the updated status.
            Console.WriteLine("--- After Cancellation ---");
            st.Cancel();
            st.Print();

            // f. Use the utility method to print an array of printable tickets.
            Console.WriteLine("--- BookingHelper.PrintAll ---");
            IPrintable[] printables = new IPrintable[] { st, vip, imax };
            BookingHelper.PrintAll(printables);

            // g. Close the Cinema.
            cinema.CloseCinema();

            #endregion
        }
    }

    #region Part 02 Interfaces & Classes

    // --- Interfaces ---
    public interface IPrintable
    {
        void Print();
    }

    public interface IBookable
    {
        bool IsBooked { get; }
        void Book();
        void Cancel();
    }

    // --- Base Ticket ---
    public abstract class Ticket : IPrintable, IBookable, ICloneable
    {
        private static int _ticketCounter = 0;
        public int TicketId { get; set; }
        public string MovieName { get; set; }
        public decimal Price { get; set; }
        public decimal PriceAfterTax => Price * 1.14m;
        public bool IsBooked { get; private set; }

        public Ticket(string movieName, decimal price)
        {
            MovieName = movieName;
            Price = price;
            TicketId = ++_ticketCounter;
            IsBooked = false;
        }

        public void Book()
        {
            if (!IsBooked) IsBooked = true;
        }

        public void Cancel()
        {
            if (IsBooked) IsBooked = false;
        }

        public abstract void Print();

        public object Clone()
        {
            Ticket copy = (Ticket)this.MemberwiseClone();
            copy.TicketId = ++_ticketCounter;
            copy.IsBooked = false;
            return copy;
        }
    }

    // --- Child Classes ---
    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }

        public StandardTicket(string movieName, decimal price, string seatNumber) : base(movieName, price)
        {
            SeatNumber = seatNumber;
        }

        public override void Print()
        {
            string status = IsBooked ? "Yes" : "No";
            Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | Standard | Seat: {SeatNumber} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {status}");
        }
    }

    public class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; set; }
        public decimal ServiceFee { get; set; } = 50m;

        public VIPTicket(string movieName, decimal price, bool loungeAccess) : base(movieName, price)
        {
            LoungeAccess = loungeAccess;
        }

        public override void Print()
        {
            string status = IsBooked ? "Yes" : "No";
            string lounge = LoungeAccess ? "Yes" : "No";
            Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | VIP | Lounge: {lounge} | Fee: {ServiceFee} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {status}");
        }
    }

    public class IMAXTicket : Ticket
    {
        public bool Is3D { get; set; }

        public IMAXTicket(string movieName, decimal price, bool is3D) : base(movieName, price)
        {
            Is3D = is3D;
        }

        public override void Print()
        {
            string status = IsBooked ? "Yes" : "No";
            string threeD = Is3D ? "Yes" : "No";
            Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | IMAX | 3D: {threeD} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {status}");
        }
    }

    // --- Cinema & Utility ---
    public class Cinema
    {
        private Ticket[] _tickets = new Ticket[20];
        private int _count = 0;

        public void OpenCinema() => Console.WriteLine("=== Cinema Opened ===");
        public void CloseCinema() => Console.WriteLine("=== Cinema Closed ===");

        public void AddTicket(Ticket t)
        {
            if (_count < 20)
            {
                _tickets[_count] = t;
                _count++;
            }
        }

        public void PrintAllTickets()
        {
            Console.WriteLine("--- All Tickets ---");
            for (int i = 0; i < _count; i++)
            {
                _tickets[i].Print();
            }
        }
    }

    public static class BookingHelper
    {
        public static void PrintAll(IPrintable[] items)
        {
            foreach (var item in items)
            {
                item?.Print();
            }
        }
    }

    #endregion
}